using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour 
{
	public void ZoomIn()
	{
		m_ScaleValue += 0.1f;
		UpdateScale ();
	}

	public void ZoomOut()
	{
		Debug.Log("ZoomIn");
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
		UpdateScale ();
		// mapRect.position = Vector3.zero;
		mapRect.anchoredPosition= Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnDragDelegate(PointerEventData data)
	{
		Debug.Log("Dragging." + string.Format("data.delta" + data.delta ) );

		mapRect.transform.Translate (data.delta.x, data.delta.y, 0.0f);
	}

	void UpdateScale()
	{
		mapRect.sizeDelta = new Vector2 (orgSizeOfTexture.width *m_ScaleValue , orgSizeOfTexture.height * m_ScaleValue );		
	}

	RectTransform mapRect ;
	Sprite m_Sprite ;
	Rect orgSizeOfTexture;
	float m_ScaleValue = 1;
}
