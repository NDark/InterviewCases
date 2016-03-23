/**
@file Virtual.cs
@author NDark
@date 20160323 file started.
*/
using UnityEngine;

public class Virtual : Parent 
{

	public virtual void RestoreHP()
	{
		m_HP = (int)( m_HPMax * 0.3f );
	}
}
