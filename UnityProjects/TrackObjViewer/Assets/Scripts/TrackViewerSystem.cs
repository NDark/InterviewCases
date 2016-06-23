﻿using UnityEngine;
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
	public int m_CameraLookIndex = 0 ;
	public bool m_DetailMode = false ;

	public void TrySlideRight()
	{
		
	}
	
	public void TrySlideLeft()
	{
		
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
	
}