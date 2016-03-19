/**
@file TryMoveTo.cs
@author NDark
@date 20160319 file started.
*/
using UnityEngine;
using System.Collections.Generic;

public class TryMoveTo : MonoBehaviour 
{
	const float BOX_SIZE = 5.0f ;
	Vector3 m_Goal = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		DoUpdate()
	}
	
	public void DoUpdate()
	{
		GameObject thisObject = this.gameObject ;
		
		if( Vector3.Distance( m_Goal , thisObject.transform.position ) < 1.0f )
		{
			Vector3 newPos = Random.insideUnitSphere ;
			newPos *= BOX_SIZE ;
			m_Goal = newPos ;
		}
		else
		{
			
			thisObject.transform.position = 
				Vector3.Lerp( thisObject.transform.position 
				             , m_Goal , Time.deltaTime ) ;
			
			
		}
	}
}
