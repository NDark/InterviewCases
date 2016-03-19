/**
@file ChangeTexture.cs
@author NDark
@date 20160319 file started.
*/
using UnityEngine;

public class ChangeTexture : MonoBehaviour 
{
	public Material m_ObjMaterial = null ;
	public string m_ResourceName = "Red128" ;
	
	// Use this for initialization
	void Start () 
	{
		Texture2D tex2D = Resources.Load(this.m_ResourceName) as Texture2D;
		
		m_ObjMaterial.mainTexture = tex2D ;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
