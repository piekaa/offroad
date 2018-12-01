using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = true)]
public class OnEvent : Attribute	
{
	public String Name;

	public OnEvent (string Name)
	{
		this.Name = Name;
	}
	
}
