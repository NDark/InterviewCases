using UnityEngine;
using System.Collections;

public class OnTriggerEnterCode : MonoBehaviour {

	
	void OnTriggerEnter( Collider _Other )
	{
		Debug.Log("OnTriggerEnter" + _Other.name );
		// die here.
		GameObject.Destroy( this ) ;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
