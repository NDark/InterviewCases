using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour 
{
	public RectTransform m_DummyMap ;

	public void ZoomIn()
	{
		m_ScaleValue += 0.1f;
		UpdateScale ();
	}

	public void ZoomOut()
	{
		m_ScaleValue -= 0.1f;
		UpdateScale ();
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
		UpdateScaneReal (mapRect);
		m_DummyMap.position = mapRect.position;
		m_DummyMap.localScale = mapRect.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDragDelegate(PointerEventData data)
	{
		Vector3 currentPos = mapRect.position;

		m_DummyMap.transform.Translate (data.delta.x, data.delta.y, 0.0f);
		Vector3 suggestingMove = Vector3.zero;
		float suggestingScale = m_ScaleValue;
		bool valid = CheckIfMapIsInsideScreen (ref suggestingMove , ref suggestingScale);
		if (false == valid) 
		{
			m_DummyMap.anchoredPosition = mapRect.anchoredPosition;
		} 
		else
		{
			mapRect.anchoredPosition = m_DummyMap.anchoredPosition;
		}
	}

	void UpdateScaneReal( RectTransform rect )
	{
		rect.sizeDelta = new Vector2 (orgSizeOfTexture.width *m_ScaleValue , orgSizeOfTexture.height * m_ScaleValue );		
	}

	void UpdateScale()
	{
		UpdateScaneReal ( m_DummyMap );

		Vector3 suggestingMove = Vector3.zero;
		float suggestingScale = m_ScaleValue;
		Vector3 currentPos = mapRect.position;
		if (false == CheckIfMapIsInsideScreen (ref suggestingMove, ref suggestingScale)) 
		{
			UpdateScaneReal ( m_DummyMap );
			UpdateScaneReal ( mapRect );

			m_DummyMap.transform.Translate (suggestingMove.x, suggestingMove.y, 0.0f);
			mapRect.anchoredPosition = m_DummyMap.anchoredPosition;
		} 
		else 
		{
			UpdateScaneReal (mapRect);
		}


	}

	bool CheckIfMapIsInsideScreen( ref Vector3 suggestingMove , ref float suggestingScale )
	{
		bool ret = true ;

		if (m_ScaleValue < 1.0f) 
		{
			m_ScaleValue = 1.0F;
			UpdateScaneReal (m_DummyMap);
			return true ;
		}

		// for 4 corners of mapRect
		// check they are invalid or not.
		// and return the suggestion scale and shift position.

		// 0 left top, 1 left bottom, 2 right bottom, 3 right top.
		m_DummyMap.GetWorldCorners (m_MapCorners);

		if (m_MapCorners [0].x > 0.0f) 
		{
			ret = false;
			suggestingMove.x = 0.0f - m_MapCorners [0].x;
		}

		if (m_MapCorners [0].y > 0.0f ) 
		{
			ret = false;
			suggestingMove.y = 0.0f - m_MapCorners [0].y;
		}

		if (m_MapCorners [2].x < Screen.width ) 
		{
			ret = false;
			suggestingMove.x = Screen.width - m_MapCorners [2].x;
		}

		if (m_MapCorners [2].y < Screen.height ) 
		{
			ret = false;
			suggestingMove.y = Screen.height - m_MapCorners [2].y;
		}

		return ret;
	}

	Vector3[] m_MapCorners = new Vector3[4];
	RectTransform mapRect ;
	Sprite m_Sprite ;
	Rect orgSizeOfTexture;
	float m_ScaleValue = 1;
}
