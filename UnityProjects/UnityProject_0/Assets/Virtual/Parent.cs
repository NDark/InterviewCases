/**
@file Virtual.cs
@author NDark
@date 20160323 file started.
*/
using UnityEngine;

public class Parent : MonoBehaviour 
{
	public GameObject m_TargetObject = null ;
	
	public virtual void RestoreHP()
	{
		m_HP = m_HPMax ;
	}

	// Use this for initialization
	void Start () 
	{
		RestoreHP() ;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		SetScale() ;
		SetColor() ;
	}
	
	private void SetScale()
	{
		if( null != m_TargetObject )
		{
			Vector3 vec3 = m_TargetObject.transform.localScale ; 
			vec3.y = GetRatio() * 3 ;
			m_TargetObject.transform.localScale = vec3 ;
		}
	}
	private void SetColor()
	{
		if( null != m_TargetObject )
		{
			Renderer r = m_TargetObject.GetComponent<Renderer>() ;
			if( null != r )
			{
				float ratio = GetRatio() ;
				if( ratio > 0.5f )
				{
					r.material.color = new Color( ratio / 2.0f , ratio , ratio / 2.0f ) ;
				}
				else
				{
					r.material.color = new Color( ratio , ratio / 2.0f  , ratio / 2.0f ) ;
				}
			}
		}
	}
	
	public float GetRatio()
	{
		return (float)m_HP / (float)m_HPMax ;
	}
	
	protected int m_HP = 0 ;
	protected int m_HPMax = 10 ;
}
