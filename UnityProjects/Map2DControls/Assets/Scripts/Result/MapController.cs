using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour 
{
	const float m_ZoomSpeed = 10.0f ;
	const float m_MovingSpeed = 10.0f ;
	const float m_ScaleStep = 0.3f ;

	public RectTransform m_TargetUnderMap = null ;


	public void ZoomIn()
	{
		m_SuggestMovePosition += CalculateCompansatePositionFromScaleDiff (m_ScaleValue, m_ScaleValue + m_ScaleStep);
		SetScaleGoal ( m_ScaleValue + m_ScaleStep);
	}

	public void ZoomOut()
	{
		m_SuggestMovePosition += CalculateCompansatePositionFromScaleDiff (m_ScaleValue, m_ScaleValue - m_ScaleStep);
		SetScaleGoal ( m_ScaleValue - m_ScaleStep);
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

		{
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener((data) => { OnClick((PointerEventData)data); });
			trigger.triggers.Add(entry);
		}


		mapRect = this.GetComponent<RectTransform> ();
		Image image = this.GetComponent<Image> ();
		Sprite sprite = image.sprite;

		if (Screen.width < Screen.height) 
		{
			// portrait
			float textureRatio = sprite.textureRect.width / sprite.textureRect.height;
			orgSizeOfTexture.Set (0, 0 
				, Screen.height * textureRatio, Screen.height);
		} 
		else 
		{
			// landscale
			float textureRatio = sprite.textureRect.height / sprite.textureRect.width;
			orgSizeOfTexture.Set (0, 0 
				, Screen.width , textureRatio * Screen.width);
		}


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
			Vector3 diff = CalculateCompansatePositionFromScaleDiff (m_ScaleValue, value);
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

	// delegate
	void OnClick(PointerEventData data)
	{
		Vector3 localPos = this.mapRect.InverseTransformPoint (data.position);

		m_TargetLocalUnderScale1 = localPos * (1.0f/ m_ScaleValue);// revert to scale 1

		UpdateTargetPos (localPos);
		m_TargetUnderMap.gameObject.SetActive (true);
	}

	void OnDragDelegate(PointerEventData data)
	{
		Vector3 delta = new Vector3 (data.delta.x, data.delta.y, 0.0f);
		TryMovePosition (delta);
	}

	void OnBeginDrag(PointerEventData data)
	{
		m_IsUnderDrag = true;
	}

	void OnEndDrag(PointerEventData data)
	{
		m_IsUnderDrag = false;
	}

	/*
	 * When input is release, scale value will try approach from m_ScaleValue to m_TargetValue.
	 * And update m_ScaleValue to by UpdateScaleReal()
	 * Finally test m_SuggestMovePosition by CheckScaleAndAssignSuggestPosition().
	*/
	void UpdateScaleAndCheckPositionValid()
	{
		if (m_TargetValue != m_ScaleValue) 
		{
			float value = Mathf.Lerp (m_ScaleValue, m_TargetValue, Time.deltaTime * m_ZoomSpeed );
			UpdateScaleReal (mapRect, value);
			CheckScaleAndAssignSuggestPosition ();
		}
	}

	void UpdateScaleBy( float newScaleValue )
	{
		UpdateScaleReal (mapRect, newScaleValue);
	}

	void UpdateScaleReal( RectTransform rect , float scaleValue )
	{
		m_ScaleValue = scaleValue;
		rect.sizeDelta = new Vector2 (orgSizeOfTexture.width *m_ScaleValue , orgSizeOfTexture.height * m_ScaleValue );		

		UpdateTargetPos (m_TargetLocalUnderScale1);
	}

	void CheckScaleAndAssignSuggestPosition()
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

	/**
	 * Move by detal and set m_SuggestMovePosition by calling CheckIfCornersIsInsideScreen().
	*/
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


	/**
	 * Update m_TargetUnderMap.transform.localPosition
	*/
	void UpdateTargetPos( Vector3 localPosUnderScale1 )
	{
		if (null != m_TargetUnderMap) 
		{
			m_TargetUnderMap.transform.localPosition = localPosUnderScale1 * m_ScaleValue ;
		}
	}

	/**
	 * Zoom by Input.GetTouch()
	*/
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

		SetScaleGoal ( m_TargetValue * scaleRatio);

		m_PriviousTouchesDistance = currentDistance;
	}

	/**
	 * Calculate the localPosition position base on theration of oldScale and new Scale.
	*/
	Vector3 CalculateCompansatePositionFromScaleDiff(float oldScale , float newScale)
	{
		Vector3 newPos = mapRect.transform.localPosition / oldScale * newScale;
		Vector3 diffVec = newPos - mapRect.transform.localPosition ;
		return diffVec;
	}

	/**
	 * Set m_TargetValue by newScale
	*/
	void SetScaleGoal( float newScale )
	{
		m_TargetValue = newScale;
	}

	/**
	 * Check and calculate suggestingMove
	*/
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

	bool m_IsUnderDrag = false ;
	Vector3[] m_MapCorners = new Vector3[4];
	RectTransform mapRect ;
	Rect orgSizeOfTexture;
	float m_ScaleValue = 1;
	float m_TargetValue = 1 ;// target scale value

	Vector3 m_SuggestMovePosition = Vector3.zero ;
	Vector3 m_PriviousTouchesDistance = Vector3.zero ;

	float m_CheckNextTime = 0.0f ;

	Vector3 m_TargetLocalUnderScale1 ;
}
