using UnityEngine;
using System.Collections;

public class DreamDoorFading : Fader 
{
/** Callbacks Unity *************************************************************************************************/

	public override void FadeIn()
	{
		this.GetComponent<Animator>().SetBool("Show", true);
	}

	public override void FadeOut()
	{
		this.GetComponent<Animator>().SetBool("Show", false);
	}

/** Attributes ******************************************************************************************************/
}
