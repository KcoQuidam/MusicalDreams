using UnityEngine;
using System.Collections;

public class KeyHandler : MonoBehaviour 
{
/** Unity Configuration **********************************************************/

	/** Key Configuration
	 */
	public KeyCode key;
	public KeyState state;

	public enum KeyState { Down, Pressed, Released }


	/** Action Configuration
	 */
	public string action;

/** Callbacks Unity **************************************************************/

	/* Unity */ void Update()
	{
		switch(state)
		{
			case KeyState.Down :
				
				if(Input.GetKey(key)) { ActionHandler.S_PerformAction(this.gameObject, action); }

			break;

			case KeyState.Pressed :
			
				if(Input.GetKeyDown(key)) { ActionHandler.S_PerformAction(this.gameObject, action); }
			
			break;

			case KeyState.Released :
			
				if(Input.GetKeyUp(key)) { ActionHandler.S_PerformAction(this.gameObject, action); }
			
			break;
		}
	}
}
