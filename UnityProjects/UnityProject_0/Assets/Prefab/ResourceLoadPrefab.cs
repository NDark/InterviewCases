using UnityEngine;

public class ResourceLoadPrefab : MonoBehaviour 
{
	GameObject m_Prefab = null ;
	public string m_PrefabPath = "" ;

	// Use this for initialization
	void Start () 
	{
		/**
		Please use Resources.Load to load a prefab and instantiate to a GameObject.
		*/
		m_Prefab = Resources.Load ( m_PrefabPath ) as GameObject;
		
		GameObject obj = GameObject.Instantiate( m_Prefab ) as GameObject ;
		if( null != obj )
		{
			obj.transform.position = Vector3.zero ;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
