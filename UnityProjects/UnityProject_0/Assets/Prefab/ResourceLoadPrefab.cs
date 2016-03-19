using UnityEngine;
using System.Collections;

public class ResourceLoadPrefab : MonoBehaviour 
{
	public GameObject m_Prefab = null ;
	public string m_PrefabPath = "" ;

	// Use this for initialization
	void Start () 
	{
		m_Prefab = Resources.Load ( m_PrefabPath ) as GameObject;
		GameObject obj = GameObject.Instantiate( m_Prefab ) as GameObject ;
		obj.transform.position = Vector3.zero ;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
