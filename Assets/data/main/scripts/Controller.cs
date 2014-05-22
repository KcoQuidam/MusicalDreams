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
			 if(Input.GetKey(KeyCode.LeftArrow))  { this.GetComponent<Jumper>().GoLeft()  ; }
		else if(Input.GetKey(KeyCode.RightArrow)) { this.GetComponent<Jumper>().GoRight() ; }

		if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.GetComponent<Jumper>().Jump();
		}
	}
}
