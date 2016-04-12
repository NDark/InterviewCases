using UnityEngine;
using System.Collections;

public class OnCollisionEnterCode : MonoBehaviour 
{
	void OnCollisionEnter(Collision _Collision )
	{
		Debug.Log("OnCollisionEnter " + _Collision.gameObject.name );
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
