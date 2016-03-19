/**

*/
using UnityEngine;

public class ResourcesLoad : MonoBehaviour 
{
	public Material m_ObjMaterial = null ;
	
	// Use this for initialization
	void Start () 
	{
		Texture2D tex2D = Resources.Load("WhileSliced") as Texture2D;
		
		m_ObjMaterial.mainTexture = tex2D ;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
