using UnityEngine;
using System.Collections;

public class CharacterApparence : MonoBehaviour 
{
/** Publics Methods **********************************************************************************************/

	public void SetLightResponsive()
	{
		this.gameObject.layer = LayerMask.NameToLayer("LightedCharacter");
		this.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Diffuse");
	}

	public void SetLightIndependant()
	{
		this.gameObject.layer = LayerMask.NameToLayer("Character");
		this.gameObject.GetComponent<MeshRenderer>().material.shader = Shader.Find("Self-Illumin/Diffuse");
	}
}
