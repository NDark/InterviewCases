using UnityEngine;
using System.Collections;

public class MoveVec : MonoBehaviour 
{
	public Vector3 m_Vec = Vector3.zero ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate( m_Vec ) ;
	}
}
