using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour 
{

	public void ZoomIn()
	{
		TryUpdateScale (m_ScaleValue, m_ScaleValue + 0.1f);
		TryMovePosition ();
	}

	public void ZoomOut()
	{
		TryUpdateScale (m_ScaleValue, m_ScaleValue - 0.1f);
		TryMovePosition ();
	}

	// Use this for initialization
	void Start () 
	{
		EventTrigger trigger = GetComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.Drag;
		entry.callback.AddListener((data) => { OnDragDelegate((PointerEventData)data); });
		trigger.triggers.Add(entry);

		mapRect = this.GetComponent<RectTransform> ();
		Image image = this.GetComponent<Image> ();
		m_Sprite = image.sprite;
		orgSizeOfTexture = m_Sprite.textureRect ;

		// mapRect.position = Vector3.zero;
		mapRect.anchoredPosition= Vector3.zero;
		UpdateScaneReal (mapRect , m_ScaleValue);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TryMovePosition()
	{
		// 0 left top, 1 left bottom, 2 right bottom, 3 right top.
		mapRect.GetWorldCorners (m_MapCorners);

		Vector3 suggestingMove = Vector3.zero;

		bool valid = CheckIfCornersIsInsideScreen ( m_MapCorners , ref suggestingMove );

		if (false == valid) 
		{
			mapRect.transform.Translate (suggestingMove);
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

		bool valid = CheckIfCornersIsInsideScreen ( m_MapCorners , ref suggestingMove );

		if (true == valid) 
		{
			mapRect.transform.Translate (delta);

		}
	}

	public void OnDragDelegate(PointerEventData data)
	{
		Vector3 delta = new Vector3 (data.delta.x, data.delta.y, 0.0f);
		TryMovePosition (delta);
	}

	void UpdateScaneReal( RectTransform rect , float scaleValue )
	{
		m_ScaleValue = scaleValue;
		rect.sizeDelta = new Vector2 (orgSizeOfTexture.width *m_ScaleValue , orgSizeOfTexture.height * m_ScaleValue );		
	}

	void TryUpdateScale( float oldScale , float newScale )
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

		if ( true == CheckScaleIfMapIsSmallerThanScreen (m_MapCorners)) 
		{
			Debug.LogWarning ("TryUpdateScale() true == CheckScaleIfMapIsSmallerThanScreen" );
			return;
		}

		UpdateScaneReal ( mapRect , newScale );

	}

	bool CheckScaleIfMapIsSmallerThanScreen( Vector3 []corners )
	{
		Vector3 distance = corners [2] - corners [0];
		return (distance.x <= (float)Screen.width - 0.001f || distance.y <= (float)Screen.height - 0.001f);
	}

	bool CheckIfCornersIsInsideScreen( Vector3 []corners, ref Vector3 suggestingMove )
	{
		bool ret = true ;

		// for 4 corners of mapRect
		// check they are invalid or not.
		// and return the suggestion scale and shift position.

		// 0 left top, 1 left bottom, 2 right bottom, 3 right top.

		if (corners [0].x > 0.0f) 
		{
			ret = false;
			suggestingMove.x = 0.0f - corners [0].x;
		}

		if (corners [0].y > 0.0f ) 
		{
			ret = false;
			suggestingMove.y = 0.0f - corners [0].y;
		}

		if (corners [2].x < Screen.width ) 
		{
			ret = false;
			suggestingMove.x = Screen.width - corners [2].x;
		}

		if (corners [2].y < Screen.height ) 
		{
			ret = false;
			suggestingMove.y = Screen.height - corners [2].y;
		}

		return ret;
	}

	Vector3[] m_MapToCornerVec = new Vector3[4];
	Vector3[] m_MapCorners = new Vector3[4];
	RectTransform mapRect ;
	Sprite m_Sprite ;
	Rect orgSizeOfTexture;
	float m_ScaleValue = 1;
}
