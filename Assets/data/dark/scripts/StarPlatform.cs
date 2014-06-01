using UnityEngine;
using System.Collections;

public class StarPlatform : MonoBehaviour 
{
/** Unity Configuration *********************************************************************************************/

	public float fadeInSpeed = 0.1f;
	public float fadeOutSpeed = 0.1f;

	public float minAlpha = 0;
	public float maxAlpha = 1;

/** Initialise ******************************************************************************************************/

	public StarPlatform()
	{
		currentBaseColor = Color.white;
		currentBaseColor.a = 0;
	}

/** Publics Methods *************************************************************************************************/

	public void FadeIn()
	{
		currentSpeed = fadeInSpeed;
	}

	public void FadeOut()
	{
		currentSpeed = -fadeOutSpeed;
	}

	public void InfuseElderStarPower(Color elderStarColor)
	{
		currentBaseColor = elderStarColor;
		currentBaseColor.a = 0;
	}

	public void ReleaseElderStarPower()
	{
		currentBaseColor = Color.white;
		currentBaseColor.a = 0;
	}

/** Callbacks Unity *************************************************************************************************/

	/* Unity */ void Update()
	{
		currentAlpha += currentSpeed * Time.deltaTime;

		currentAlpha = (currentAlpha < minAlpha) ? minAlpha : currentAlpha;
		currentAlpha = (currentAlpha > maxAlpha) ? maxAlpha : currentAlpha;

		particleSystem.startColor = new Color (currentBaseColor.r, currentBaseColor.g, currentBaseColor.b, Mathf.Max(currentBaseColor.a, currentAlpha));
	}

/** Attributes ******************************************************************************************************/

	private float currentSpeed = 0;
	private float currentAlpha = 0;

	private Color currentBaseColor;
}
