using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackViewerSystem : MonoBehaviour 
{
	public float m_Radious = 20.0f ;
	public GameObject m_ObjectParent = null ;
	public Camera m_Camera = null ;
	public List<GameObject> m_Objs = new List<GameObject>() ;
	
	public float m_CameraMaxDistance = 15.0f ;
	public float m_CameraMinDistance = 11.0f ;
	public float m_CameraHeight = 4.0f ;
	public int m_CameraLookIndex = 0 ;
	public bool m_DetailMode = false ;
	
	public Vector3 m_PressPos = Vector3.zero ;

	public void TrySlideRight()
	{
		if( m_Objs.Count <= 0 )
		{
			return ;
		}
		
		
		m_CameraLookIndex += 1 ;
		if( m_CameraLookIndex >= m_Objs.Count )
		{
			m_CameraLookIndex = 0 ;
		}
		
		UpdateCamera() ;
	}
	
	public void TrySlideLeft()
	{
		if( m_Objs.Count <= 0 )
		{
			return ;
		}
		
		m_CameraLookIndex -= 1 ;
		if( m_CameraLookIndex < 0 )
		{
			m_CameraLookIndex = m_Objs.Count - 1  ;
		}
		
		UpdateCamera() ;
	}
	
	public void TryZoomIn()
	{
		
	}
	
	public void TryZoomOut()
	{
		
	}
	
	// Use this for initialization
	void Start () 
	{
		CollectObjects() ;
		ArrangeObjects() ;
		
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		DetectInput() ;
		
	}
	
	void CollectObjects()
	{
		if( null == m_ObjectParent )
		{
			return ;
		}
		
		Transform child = null ;
		int size = m_ObjectParent.transform.childCount ;
		for( int i = 0 ; i < size ; ++i )
		{
			child = m_ObjectParent.transform.GetChild( i ) ;
			m_Objs.Add( child.gameObject ) ;
		}
	}
	
	void ArrangeObjects()
	{
		Vector3 center = m_ObjectParent.transform.position ;
		
		float radian = 0.0f ;
		float d2r = 360 * Mathf.Deg2Rad ;
		int size = this.m_Objs.Count ; 
		for( int i = 0 ; i < size ; ++i )
		{
			radian = (float)i/( float)size * d2r ;
			Vector3 setPos = new Vector3( m_Radious * Mathf.Sin( radian ) 
				, 0.0f 
			                             , m_Radious * Mathf.Cos( radian ) ) ;
			                             
			m_Objs[i].transform.position = setPos ;
		}
	}

	private void UpdateCamera() 
	{
		if( null == m_Camera )
		{
			return ;
		}
		CameraMoveTo moveTo = m_Camera.GetComponent<CameraMoveTo>() ;
		bool isInAnim = ( null != moveTo ) ;
		if( true == isInAnim )
		{
			return ;
		}
		if( m_CameraLookIndex >= m_Objs.Count )
		{
			return ;
		}
		
		float assumDist = (m_DetailMode) ? 
			m_Radious + m_CameraMinDistance : 
				m_Radious + m_CameraMaxDistance ; 
		Vector3 targetObjPos = m_Objs[ m_CameraLookIndex ].transform.position ;
		Vector3 toTargetObjVec = targetObjPos - this.gameObject.transform.position ;
		toTargetObjVec.Normalize() ;
		
		Vector3 assumePos = toTargetObjVec * assumDist ;
		assumePos.y = m_CameraHeight ;
		
		moveTo = m_Camera.gameObject.AddComponent<CameraMoveTo>() ;
		moveTo.m_Destination = assumePos ;
		moveTo.m_LookTarget = targetObjPos ;
		
	}	
	
	private void DetectInput()
	{
		if( Input.GetMouseButtonDown( 0 ) )
		{
			m_PressPos = Input.mousePosition ;		
		}
		else if( Input.GetMouseButtonUp( 0 ) )
		{
			Vector3 distVec = Input.mousePosition - m_PressPos ;
			if( distVec.x > 0 )
			{
				TrySlideRight() ;
			}
			else
			{
				TrySlideLeft() ;
			}
		}
		
	}
	
	
}
