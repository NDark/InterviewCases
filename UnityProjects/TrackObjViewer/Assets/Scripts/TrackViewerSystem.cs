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
	public Vector3 m_LastPos = Vector3.zero ;

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
		Debug.Log("TryZoomIn");
		m_DetailMode = true ;
		UpdateCamera() ;
	}
	
	public void TryZoomOut()
	{
		Debug.Log("TryZoomOut");
		m_DetailMode = false ;
		UpdateCamera() ;
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
			Component.DestroyImmediate( moveTo ) ;
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
			if( distVec.y > 50 && m_DetailMode )
			{
				TryZoomOut() ;
			}
			else if( distVec.x > 30 && false == m_DetailMode )
			{
				TrySlideRight() ;
			}
			else if( distVec.x < -30 && false == m_DetailMode )
			{
				TrySlideLeft() ;
			}
			else if( Mathf.Abs( distVec.x ) < 30 
				&& Mathf.Abs( distVec.y ) < 30 
			        && false == m_DetailMode )
			{
				TryZoomIn() ;
			}
		}
		else if( Input.GetMouseButton( 0 ) 
			&& m_DetailMode )
		{
			Vector3 moveVec = Input.mousePosition - m_LastPos ;
			TrackBallObject( moveVec ) ;
		}
		
		m_LastPos = Input.mousePosition ;
	}
	
	void TrackBallObject( Vector3 _InputVec )
	{
		GameObject obj = m_Objs[ m_CameraLookIndex ] ;
		obj.transform.Rotate( m_Camera.transform.up , -1 * _InputVec.x , Space.World ) ;
		obj.transform.Rotate( m_Camera.transform.right ,  _InputVec.y , Space.World ) ;
		
	}
}
