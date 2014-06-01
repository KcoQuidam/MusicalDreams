using UnityEngine;
using System.Collections;

public class ElderStar : MonoBehaviour
{
/** Publics Methods *************************************************************************************************/

	public void Loot()
	{
		this.InfusePower();
		this.FadeOut();
	}

	public void FadeIn()
	{
		this.collider.enabled = true;
	}
	
	public void FadeOut()
	{
		this.collider.enabled = false;
	}

/** Internals Methods ***********************************************************************************************/

	protected void InfusePower()
	{
		foreach(var starplatform in Component.FindObjectsOfType<StarPlatform>())
		{
			starplatform.InfuseElderStarPower(this.particleSystem.startColor);
		}
	}
}
