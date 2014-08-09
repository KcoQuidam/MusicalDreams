using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

/** Reserved character : "/", ":", "[", "]" "\n".
 */
[System.Serializable]
public class Action
{
/** Unity Configuration ******************************************************************/

	private GameObject fixedTarget;
	private string componentName;
	private string methodName;

	private List<Arg> args = new List<Arg>();

	/** For composed actions.
	 */
	private Action subAction = null;

/** Initialise ****************************************************************************/

	public Action()
	{
	}

	public Action(string description)
	{
		description = description.Replace("\n", "");

		if (description.Contains ("/"))
		{
			string[] descriptions = description.Split(new char[]{ '/' } , 2);
			this.subAction = new Action(descriptions[1]);

			description = descriptions[0];
		}

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
		else if(values.Length <= 2)
		{
			string actionName = (values.Length == 2) ? values[1] : values[0];

			Action based;
			if(ActionHandler.TryGetGlobalAction(actionName, out based))
			{
				this.FillFromGlobalAction(based, (values.Length == 2) ? GameObject.Find(values[0]) : based.fixedTarget, this.subAction);
			}
			else
			{
				Debug.LogWarning("Cannot found " + actionName + " global action");
			}
		}
	}

	private void FillFromGlobalAction(Action based, GameObject fixedTarget, Action mySubAction)
	{
		componentName = based.componentName;
		methodName = based.methodName;
		
		args = based.args;
		
		this.fixedTarget = fixedTarget;

		if(based.subAction != null)
		{
			this.subAction = new Action();
			this.subAction.FillFromGlobalAction(based.subAction, fixedTarget, mySubAction);
		}
		else
		{
			this.subAction = mySubAction;
		}
	}

/** Public Methods *************************************************************************/

	public void Perform(GameObject target)
	{
		if(fixedTarget != null) { target = fixedTarget; }

		Component c = target.GetComponent(componentName);

		if(c != null)
		{
			MethodInfo method = null;

			method = c.GetType().GetMethod(methodName, ArgsTypesWithSubAction);

			if(method != null) 
			{
				method.Invoke(c, ArgsWithSubAction);
				return;
			}

			method = c.GetType().GetMethod(methodName, ArgsTypes);

			if(method != null) 
			{
				method.Invoke(c, Args);

				if(subAction != null)
				{
					subAction.Perform(target);
				}
			}
			else
			{
				Debug.LogWarning("Action cannot be perform method " + methodName + " with " + Args.Length + " args not found on " + target.name + " (" + componentName + ")");
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

	public object[] ArgsWithSubAction
	{
		get
		{
			List<object> values = new List<object>();
			
			foreach(Arg arg in args)
			{
				values.Add(arg.Val);
			}

			values.Add(subAction);
			
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

	public System.Type[] ArgsTypesWithSubAction
	{
		get
		{
			List<System.Type> types = new List<System.Type>();
			
			foreach(Arg arg in args)
			{
				types.Add(TypeToSystemType(arg.type));
			}

			types.Add(typeof(Action));
			
			return types.ToArray();
		}
	}
}
