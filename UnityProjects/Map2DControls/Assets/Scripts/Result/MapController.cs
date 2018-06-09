using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour 
{
	public Text m_DebugText;
	float m_ScaleStep = 0.3f ;


	public void ZoomIn()
	{
		
		m_SuggestMovePosition += TryCompensateScalePosition (m_ScaleValue, m_ScaleValue + m_ScaleStep);
		TryCheckScale (m_ScaleValue, m_ScaleValue + m_ScaleStep);
	}

	public void ZoomOut()
	{
		m_SuggestMovePosition += TryCompensateScalePosition (m_ScaleValue, m_ScaleValue - m_ScaleStep);
		TryCheckScale (m_ScaleValue, m_ScaleValue - m_ScaleStep);
	}

	// Use this for initialization
	void Start () 
	{
		EventTrigger trigger = GetComponent<EventTrigger>();
		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.Drag;
			entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
			trigger.triggers.Add(entry);
		}

		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.BeginDrag;
			entry.callback.AddListener((data) => { OnBeginDrag((PointerEventData)data); });
			trigger.triggers.Add(entry);
		}

		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.EndDrag;
			entry.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
			trigger.triggers.Add(entry);
		}


		mapRect = this.GetComponent<RectTransform> ();
		Image image = this.GetComponent<Image> ();
		m_Sprite = image.sprite;
		Debug.LogWarning ("Screen.width" + Screen.width);
		Debug.LogWarning ("Screen.height" + Screen.height);

		float textureRatio = m_Sprite.textureRect.width / m_Sprite.textureRect.height;
		orgSizeOfTexture.Set( 0 , 0 
			, Screen.height * textureRatio, Screen.height ) ;

		Debug.LogWarning ("orgSizeOfTexture" + orgSizeOfTexture);

		// mapRect.position = Vector3.zero;
		mapRect.anchoredPosition= Vector3.zero;
		UpdateScaleReal (mapRect , m_ScaleValue);

		m_TargetValue = m_ScaleValue;
		m_SuggestMovePosition = mapRect.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		// Debug.LogWarning ("Input.touchCount" + Input.touchCount );
		if (Input.touchCount >= 2) 
		{
			TryCheckZoomByPinch ();

			float value = Mathf.Lerp (m_ScaleValue, m_TargetValue, Time.deltaTime * m_ZoomSpeed );
			Vector3 diff = TryCompensateScalePosition (m_ScaleValue, value);
			UpdateScaleBy (value);

			mapRect.transform.position = mapRect.transform.position + diff;
			m_SuggestMovePosition = mapRect.transform.position;

		} 
		else 
		{
			m_PriviousTouchesDistance = Vector3.zero;

		}


		if ( Input.touchCount <= 0 && false == m_IsUnderDrag) 
		{
			UpdateScaleAndCheckPositionValid ();

			if (m_TargetValue >= 1.0f &&m_SuggestMovePosition != mapRect.transform.position) 
			{
				mapRect.transform.position = Vector3.Lerp (mapRect.transform.position, m_SuggestMovePosition, Time.deltaTime * m_MovingSpeed );
			}

		}


		if (Input.touchCount <= 0 && m_TargetValue < 1.0f) 
		{
			if (Time.time > m_CheckNextTime) // recover
			{
				m_TargetValue = 1.0f;
				UpdateScaleAndCheckPositionValid ();
			}
		}
		else 
		{
			m_CheckNextTime = Time.time + 0.3f;
		}

	}

	void UpdateScaleAndCheckPositionValid()
	{
		if (m_TargetValue != m_ScaleValue) 
		{
			float value = Mathf.Lerp (m_ScaleValue, m_TargetValue, Time.deltaTime * m_ZoomSpeed );
			UpdateScaleReal (mapRect, value);
			TryMovePosition ();
		}
	}

	void UpdateScaleBy( float newScaleValue )
	{
		UpdateScaleReal (mapRect, newScaleValue);
	}

	void UpdateScale()
	{
		if (m_TargetValue != m_ScaleValue) 
		{
			float value = Mathf.Lerp (m_ScaleValue, m_TargetValue, Time.deltaTime * m_ZoomSpeed );
			UpdateScaleBy (value);
		}
	}

	void TryMovePosition()
	{
		// 0 left top, 1 left bottom, 2 right bottom, 3 right top.
		mapRect.GetWorldCorners (m_MapCorners);

		Vector3 suggestingMove = Vector3.zero;

		bool valid = CheckIfCornersIsInsideScreen ( m_MapCorners , ref suggestingMove );
		if (true == valid) 
		{
			m_SuggestMovePosition = mapRect.transform.position + suggestingMove;
		} 
	}

	void TryMovePosition( Vector3 delta )
	{
		// 0 left top, 1 left bottom, 2 right bottom, 3 right top.
		mapRect.GetWorldCorners (m_MapCorners);
		for (int i = 0; i < m_MapCorners.Length; ++i) 
		{
			m_MapCorners [i] += delta;
		}

		Vector3 suggestingMove = Vector3.zero;

		bool invalid = CheckIfCornersIsInsideScreen ( m_MapCorners , ref suggestingMove );
		if (true == invalid) 
		{
			m_SuggestMovePosition = mapRect.transform.position + suggestingMove;
		} 
		else 
		{
			m_SuggestMovePosition = mapRect.transform.position + delta;
		}

		mapRect.transform.Translate (delta);
	}


	public void OnDragDelegate(PointerEventData data)
	{
		Vector3 delta = new Vector3 (data.delta.x, data.delta.y, 0.0f);

		TryMovePosition (delta);
	}

	public void OnBeginDrag(PointerEventData data)
	{
		m_IsUnderDrag = true;
	}

	public void OnEndDrag(PointerEventData data)
	{
		m_IsUnderDrag = false;
	}

	void UpdateScaleReal( RectTransform rect , float scaleValue )
	{
		m_ScaleValue = scaleValue;
		rect.sizeDelta = new Vector2 (orgSizeOfTexture.width *m_ScaleValue , orgSizeOfTexture.height * m_ScaleValue );		
	}

	void TryCheckZoomByPinch()
	{
		if (Input.touchCount != 2) 
		{
			return;
		}

		Touch touch0 = Input.GetTouch(0);
		Touch touch1 = Input.GetTouch(1);

		Vector3 currentDistance = touch0.position - touch1.position;

		if (Vector3.zero == m_PriviousTouchesDistance) {
			m_PriviousTouchesDistance = currentDistance;
		} 

		float scaleRatio = currentDistance.magnitude / m_PriviousTouchesDistance.magnitude;

		TryCheckScale ( m_ScaleValue , m_TargetValue * scaleRatio);

		m_PriviousTouchesDistance = currentDistance;

	}

	Vector3 TryCompensateScalePosition(float oldScale , float newScale)
	{
		Vector3 newPos = mapRect.transform.localPosition / oldScale * newScale;
		Vector3 diffVec = newPos - mapRect.transform.localPosition ;
		// m_DebugText.text = string.Format ("{0}->{1},{2}" , oldScale , newScale , diffVec);
		return diffVec;

	}

	void TryCheckScale( float oldScale , float newScale )
	{
		
		// 0 left top, 1 left bottom, 2 right bottom, 3 right top.
		mapRect.GetWorldCorners (m_MapCorners);

		Vector3 center = m_MapCorners [0] + m_MapCorners [2];
		center *= 0.5f;
		for( int i = 0 ; i < m_MapCorners.Length ; ++i )
		{
			m_MapToCornerVec [i] = m_MapCorners [i] - center;
			m_MapToCornerVec [i] = m_MapToCornerVec [i] / oldScale * newScale;
			m_MapCorners [i] = center + m_MapToCornerVec [i];
		}


		if (true == CheckScaleIfMapIsSmallerThanScreen (m_MapCorners)) 
		{
			// Debug.LogWarning ("TryCheckScale() true == CheckScaleIfMapIsSmallerThanScreen");
		} 

		m_TargetValue = newScale;

	}

	bool CheckScaleIfMapIsSmallerThanScreen( Vector3 []corners )
	{
		Vector3 distance = corners [2] - corners [0];
		bool ret = (distance.x <= (float)Screen.width - 0.01f || distance.y <= (float)Screen.height - 0.01f);
		if (true == ret) 
		{
			// Debug.Log ("distance=" + distance );
		}
		return ret ;
	}

	bool CheckIfCornersIsInsideScreen( Vector3 []corners, ref Vector3 suggestingMove )
	{
		bool ret = false  ;

		// for 4 corners of mapRect
		// check they are invalid or not.
		// and return the suggestion scale and shift position.

		// 0 left top, 1 left bottom, 2 right bottom, 3 right top.

		if (corners [0].x > 0.0f) 
		{
			ret = true;
			suggestingMove.x = 0.0f - corners [0].x;
		}

		if (corners [0].y > 0.0f ) 
		{
			ret = true;
			suggestingMove.y = 0.0f - corners [0].y;
		}

		if (corners [2].x < Screen.width ) 
		{
			ret = true;
			suggestingMove.x = Screen.width - corners [2].x;
		}

		if (corners [2].y < Screen.height ) 
		{
			ret = true;
			suggestingMove.y = Screen.height - corners [2].y;
		}

		return ret;
	}

	float m_ZoomSpeed = 10.0f ;
	float m_MovingSpeed = 10.0f ;

	bool m_IsUnderDrag = false ;
	Vector3[] m_MapToCornerVec = new Vector3[4];
	Vector3[] m_MapCorners = new Vector3[4];
	RectTransform mapRect ;
	Sprite m_Sprite ;
	Rect orgSizeOfTexture;
	float m_ScaleValue = 1;

	float m_TargetValue = 1 ;
	Vector3 m_SuggestMovePosition = Vector3.zero ;
	Vector3 m_PriviousTouchesDistance = Vector3.zero ;

	float m_CheckNextTime = 0.0f ;

}
