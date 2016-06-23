using UnityEngine;
using System.Collections;

public class CameraMoveTo : MonoBehaviour 
{
	public Vector3 m_Destination = Vector3.zero ;
	public Vector3 m_LookTarget = Vector3.zero ;
	public float m_Threashold = 0.01f ;
	public float m_Speed = 3.0f ;
	
	public Quaternion m_FinalRotation = Quaternion.identity ;
	
	// Use this for initialization
	void Start () 
	{
		Vector3 lookForward = m_LookTarget - m_Destination ;
		m_FinalRotation = Quaternion.LookRotation( lookForward ) ;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float dist = Vector3.Distance( this.transform.position , m_Destination ) ;
		if( dist < m_Threashold )
		{
			this.transform.LookAt( m_LookTarget , Vector3.up ) ;
			
			Component.Destroy( this );
			return ;
		}
	
		this.transform.position = 
			Vector3.Lerp( this.transform.position 
				, m_Destination 
				, m_Speed * Time.deltaTime ) ;
		Vector3 lookVec = m_LookTarget - this.transform.position ;
		this.transform.rotation = Quaternion.Slerp( this.transform.rotation
		                                          , m_FinalRotation 
		                                          , m_Speed * Time.deltaTime ) ;
		
	}
}
