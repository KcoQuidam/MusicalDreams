using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

[System.Serializable]
public class Action
{
/** Unity Configuration ******************************************************************/

	private GameObject fixedTarget;
	private string componentName;
	private string methodName;

	private List<Arg> args = new List<Arg>();

/** Initialise ****************************************************************************/

	public Action()
	{
	}

	public Action(string description)
	{
		string[] values = description.Split(':');

		if(values.Length > 2)
		{
			componentName = values[1];
			methodName = values[2];

			if(values[0].Length > 0)
			{
				fixedTarget = GameObject.Find(values[0]);
			}

			for(int i = 3; i < values.Length; i++)
			{
				args.Add(new Arg(values[i]));
			}
		}
		else if(values.Length == 2)
		{
			Action based;
			if(ActionHandler.TryGetGlobalAction(values[1], out based))
			{
				componentName = based.componentName;
				methodName = based.methodName;

				args = based.args;

				fixedTarget = GameObject.Find(values[0]);
			}
		}
	}


/** Public Methods *************************************************************************/

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

		public Arg(string desc)
		{
			desc = desc.Replace("[", "");
			string[] values = desc.Split(']');

			if(values.Length > 1)
			{
				this.type = (Arg.Type) System.Enum.Parse(typeof(Arg.Type), values[0]);
				this.val = values[1];
			}
			else
			{
				this.type = Type.String;
				this.val = values[0];
			}
		}

		public Type type = Type.String;
		public string val;

		public object Val
		{
			get
			{
				switch(type)
				{
					case Arg.Type.String : return val;
					case Arg.Type.Integer : return int.Parse(val);
					case Arg.Type.Float : return float.Parse(val);
					case Arg.Type.Bool : return bool.Parse(val);
				}

				return null;
			}
		}
	}

	public object[] Args
	{
		get
		{
			List<object> values = new List<object>();
			
			foreach(Arg arg in args)
			{
				values.Add(arg.Val);
			}
			
			return values.ToArray();
		}
	}

	private static System.Type TypeToSystemType(Arg.Type type)
	{
		switch(type)
		{
			case Arg.Type.String : return typeof(string);
			case Arg.Type.Integer : return typeof(int);
			case Arg.Type.Float : return typeof(float);
			case Arg.Type.Bool : return typeof(bool);
		}

		return typeof(object);
	}
	
	public System.Type[] ArgsTypes
	{
		get
		{
			List<System.Type> types = new List<System.Type>();
			
			foreach(Arg arg in args)
			{
				types.Add(TypeToSystemType(arg.type));
			}
			
			return types.ToArray();
		}
	}
}
