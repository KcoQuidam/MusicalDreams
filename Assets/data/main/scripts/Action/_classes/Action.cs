using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

[System.Serializable]
public class Action
{
	public string name = "";

	public GameObject fixedTarget;
	public string componentName;
	public string methodName;

	public List<Arg> args;

	public void Perform(GameObject target)
	{
		if(fixedTarget != null) { target = fixedTarget; }

		Component c = target.GetComponent(componentName);

		if(c != null)
		{
			MethodInfo method = c.GetType().GetMethod(methodName, ArgsTypes);

			if(method != null) 
			{
				method.Invoke(c, Args);
			}
			else
			{
				Debug.LogWarning("Action cannot be perform method " + methodName + " not found on " + target.name + "(" + componentName + ")");
			}
		}
		else
		{
			Debug.LogWarning("Action cannot be perform component " + componentName + " not found on " + target.name);
		}
	}
	
	[System.Serializable]
	public class Arg
	{
		public enum Type
		{
			String,
			Integer,
			Float,
			Bool
		}
		
		public Type type = Type.String;
		
		public string strValue;
		public int intValue;
		public float fltValue;
		public bool boolValue;
	}
	
	public object[] Args
	{
		get
		{
			List<object> values = new List<object>();
			
			foreach(Arg arg in args)
			{
				switch(arg.type)
				{
				case Arg.Type.String : values.Add(arg.strValue); break;
				case Arg.Type.Integer : values.Add(arg.intValue); break;
				case Arg.Type.Float : values.Add(arg.fltValue); break;
				case Arg.Type.Bool : values.Add(arg.boolValue); break;
				}
			}
			
			return values.ToArray();
		}
	}
	
	public System.Type[] ArgsTypes
	{
		get
		{
			List<System.Type> types = new List<System.Type>();
			
			foreach(Arg arg in args)
			{
				switch(arg.type)
				{
				case Arg.Type.String : types.Add(typeof(string)); break;
				case Arg.Type.Integer : types.Add(typeof(int)); break;
				case Arg.Type.Float : types.Add(typeof(float)); break;
				case Arg.Type.Bool : types.Add(typeof(bool)); break;
				}
			}
			
			return types.ToArray();
		}
	}
}
