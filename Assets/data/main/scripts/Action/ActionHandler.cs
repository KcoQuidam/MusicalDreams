using UnityEngine;
using System.Collections.Generic;

public class ActionHandler : MonoBehaviour 
{
/** Statical Configurations ******************************************************************************/

	private static Dictionary<string, Action> globalActions = new Dictionary<string, Action>();

	public static readonly string GLOBAL_ACTION_MARKER = "global:";
	
/** Publics Methods **************************************************************************************/

	/** Perform a globally registered action.
	 */
	public bool PerformGlobalAction(string name)
	{
		return ActionHandler.PerformAction(this.gameObject, name);
	}
	
	/** Perform a locally registered action.
	 */
	public bool PerformLocalAction(string name)
	{
		if(string.IsNullOrEmpty(name)) { Debug.LogWarning("Cannot perform local action, searched name is null or empty"); return false; }

		foreach(var actionRegister in this.GetComponents<ActionRegister>())
		{
			if(name.Equals(actionRegister.action.name))
			{
				actionRegister.action.Perform(this.gameObject);
				
				return true;
			}
		}
		
		Debug.LogWarning ("Cannot perform local action with id " + name + " not registered"); return false;
	}

	/** Register an action has global.
	 */
	public static bool RegisterAction(Action action)
	{
		if(!action.name.StartsWith(GLOBAL_ACTION_MARKER)) 
		{
			action.name = GLOBAL_ACTION_MARKER + action.name;
		}

		globalActions.Add(action.name, action); return true;
	}

	/** Staticaly Perform a globally registered action.
	 */
	public static bool PerformAction(GameObject sender, string name)
	{
		if(string.IsNullOrEmpty(name)) { Debug.LogWarning("Cannot perform global action, searched name is null or empty"); return false; }

		name = GLOBAL_ACTION_MARKER + name;

		foreach(var action in globalActions)
		{
			if(name.Equals(action.Value.name))
			{
				action.Value.Perform(sender); 
				
				return true;
			}
		}
		
		Debug.LogWarning ("Cannot perform global action with id " + name + " not registered"); return false;
	}

/** Callbacks Unity **************************************************************************************/
}
