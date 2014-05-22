using UnityEngine;
using System.Collections;

public class StarPlatformFading : Fader 
{
/** Unity Configuration *********************************************************************************************/

	public float stepValue = 0;

/** Callbacks Unity *************************************************************************************************/

	public override void FadeIn()
	{
		step = 1f;
	}

	public override void FadeOut()
	{
		step = -1f;
	}

	/* Unity */ void Update()
	{
		currentAlpha += step * stepValue * Time.deltaTime;

		currentAlpha = (currentAlpha < 0) ? 0 : currentAlpha;
		currentAlpha = (currentAlpha > 1) ? 1 : currentAlpha;

		particleSystem.startColor = new Color(1, 1, 1, currentAlpha);
	}

/** Attributes ******************************************************************************************************/

	private float currentAlpha = 0;
	private float step = -1f;
}
