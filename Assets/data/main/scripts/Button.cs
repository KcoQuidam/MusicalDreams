using UnityEngine;

public class Button : MonoBehaviour 
{
	/*Unity*/ void OnTriggerEnter2D(Collider2D collider) 
	{
		this.GetComponent<Animator>().SetTrigger("Engage");
	}
}
