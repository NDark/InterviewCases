/**
@file TryMoveTo.cs
@author NDark
@date 20160319 file started.
*/
using UnityEngine;
using System.Collections.Generic;

public class TryMoveTo : MonoBehaviour 
{
	static List<TryMoveTo> s_List = new List<TryMoveTo>() ;
	
	const float BOX_SIZE = 5.0f ;
	Vector3 m_Goal = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		s_List.Add( this ) ;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( Vector3.Distance( m_Goal , this.transform.position ) < 1.0f )
		{
			Vector3 newPos = Random.insideUnitSphere ;
			newPos *= BOX_SIZE ;
			m_Goal = newPos ;
		}
		else
		{
			CalculateDistanceBetween();
			
			this.transform.position = 
			Vector3.Lerp( this.transform.position , m_Goal , Time.deltaTime ) ;
			
			
		}
	}
	
	private void CalculateDistanceBetween()	
	{
		float sum = 0.0f ;
		for( int i = 0 ; i < 10000 ; ++i )
		{
			sum += Mathf.Sqrt( i * i / 2.0f ) ;
		}
	}
}
