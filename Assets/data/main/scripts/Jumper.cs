using UnityEngine;
using System.Collections;

/** Player controle on the rectangle character.
 */
public class Jumper : MonoBehaviour 
{
/** Unity Configuration ******************************************/

	public float JumpSpeed = 650;

	public float MoveAcceleration = 25;
	public float MaxMoveVelocity = 25;

/** Interface methods ********************************************/

	public void Jump()
	{
		rigidbody2D.AddForce(new Vector2(0, JumpSpeed));
	}

	public void GoLeft()
	{
		if(rigidbody2D.velocity.x > -MaxMoveVelocity) 
		{
			rigidbody2D.AddForce (new Vector2(-MoveAcceleration * Time.deltaTime, 0));
		}
	}

	public void GoRight()
	{
		if(rigidbody2D.velocity.x < MaxMoveVelocity) 
		{
			rigidbody2D.AddForce(new Vector2 (MoveAcceleration * Time.deltaTime, 0));
		}
	}

/** Protected attribute ******************************************/

/** Callback Unity ***********************************************/

	private void Update()
	{
		this.GetComponent<Animator>().SetBool("CanJump", this.rigidbody2D.velocity.y == 0);
	}
}
