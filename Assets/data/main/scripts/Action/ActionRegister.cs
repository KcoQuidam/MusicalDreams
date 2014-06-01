using UnityEngine;
using System.Collections;

public class ActionRegister : MonoBehaviour 
{
/** Initialisation ************************************************************************/

	public ActionRegister()
	{
		actionName = ActionHandler.GLOBAL_ACTION_MARKER;
	}

/** Unity Configurations ******************************************************************/

	public string actionName = "";
	public string description = "";

/** Public Method *************************************************************************/

	public Action Action
	{
		get
		{
			if(action == null) { action = new Action(description); } return action;
		}
	}
	private Action action = null;

	public void Perform()
	{
		this.Action.Perform(this.gameObject);
	}

/** Unity Callbacks ***********************************************************************/

	/* Unity */ void Start()
	{
		if(actionName.StartsWith(ActionHandler.GLOBAL_ACTION_MARKER) && !ActionHandler.RegisterAction(actionName, this.Action))
		{
			Debug.LogWarning("Cannot register action " + actionName + ", something goes wrong");
		}
	}
}
