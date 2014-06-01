using UnityEngine;
using System.Collections.Generic;

public class ActionHandler : MonoBehaviour 
{
/** Statical Configurations ******************************************************************************/

	private static Dictionary<string, Action> globalActions = new Dictionary<string, Action>();
	private static Dictionary<string, Action> alreadyRegisteredActions = new Dictionary<string, Action>();

	public static readonly string GLOBAL_ACTION_MARKER = "global:";
	public static readonly string LOCAL_ACTION_MARKER  = "local:";
	
/** Performing Methods **************************************************************************************/

	public void PerformLocalAction(string nameOrDescription)
	{
		ActionHandler.S_PerformAction(this.gameObject, nameOrDescription);
	}

	public void PerformAction(GameObject target, string nameOrDescription)
	{
		ActionHandler.S_PerformAction(target, nameOrDescription);
	}

	/** Perform an action.
	 */
	public static void S_PerformAction(GameObject target, string nameOrDescription)
	{
		Action action = null;

		if(   !TryGetGlobalAction(nameOrDescription, out action) 
		   && !TryGetLocalAction(target, nameOrDescription, out action)
		   && !alreadyRegisteredActions.TryGetValue(nameOrDescription, out action))
		{
			action = new Action(nameOrDescription); 
			alreadyRegisteredActions.Add(nameOrDescription, action);
		}

		action.Perform(target);
	}
	


	public static bool TryGetLocalAction(GameObject target, string name, out Action action)
	{
		foreach(var actionRegister in target.GetComponents<ActionRegister>())
		{
			if(name.Equals(actionRegister.actionName))
			{
				action = actionRegister.Action; return true;
			}
		}

		action = null; return false;
	}

	public static bool TryGetGlobalAction(string name, out Action action)
	{
		return globalActions.TryGetValue(GLOBAL_ACTION_MARKER + name, out action);
	}

/** Register action *************************************************************************************/

	/** Register an action has global.
	 */
	public static bool RegisterAction(string actionName, Action action)
	{
		if(!globalActions.ContainsKey(actionName)) 
		{
			globalActions.Add(actionName, action);
			return true;
		}

		Debug.LogWarning("Warning the global action " + actionName + " already was register");
		return false;

	}

/** Callbacks Unity **************************************************************************************/
}
