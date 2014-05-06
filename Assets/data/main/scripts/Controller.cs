using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour 
{
/** Unity Configuration ********************************************/

/** Protected attribute ******************************************/

/** Unity Callback ***********************************************/

	// Update is called once per frame
	void Update() 
	{
		int motion = 0;

			 if(Input.GetKey(KeyCode.LeftArrow))  { this.GetComponent<Jumper>().GoLeft()  ; motion = -1; }
		else if(Input.GetKey(KeyCode.RightArrow)) { this.GetComponent<Jumper>().GoRight() ; motion = 1 ; }

		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.GetComponent<Animator>().SetTrigger("Jump");
		}

		this.GetComponent<Animator>().SetInteger("Motion", motion);
	}
}
