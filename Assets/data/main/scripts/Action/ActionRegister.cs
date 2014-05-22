using UnityEngine;
using System.Collections;

public class ActionRegister : MonoBehaviour 
{
/** Initialisation ************************************************************************/

	public ActionRegister()
	{
		action.name = ActionHandler.GLOBAL_ACTION_MARKER;
	}

/** Unity Configurations ******************************************************************/

	public Action action = new Action();

/** Unity Callbacks ***********************************************************************/

	/* Unity */ void Start()
	{
		if(action.name.StartsWith(ActionHandler.GLOBAL_ACTION_MARKER) && !ActionHandler.RegisterAction(action))
		{
			Debug.LogWarning("Cannot register action " + action.name + ", something goes wrong");
		}
	}
}
