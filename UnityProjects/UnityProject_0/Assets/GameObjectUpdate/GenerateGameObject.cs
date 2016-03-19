/**
@file GenerateGameObject.cs
@author NDark
@date 20160319 file started.
*/
using UnityEngine;
using System.Collections;

public class GenerateGameObject : MonoBehaviour 
{
	public int m_GenerateNum = 10 ;

	// Use this for initialization
	void Start () 
	{
		GameObject prefab = Resources.Load("Character") as GameObject;
		
		GameObject addObj = null ;
		for( int i = 0 ; i < m_GenerateNum ; ++i )
		{
			addObj = GameObject.Instantiate( prefab ) as GameObject;
			addObj.name = i.ToString() ;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
