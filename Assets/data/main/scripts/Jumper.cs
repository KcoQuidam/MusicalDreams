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

/** Privates Attributes ******************************************/

	private bool canJump = false;

/** Interface methods ********************************************/

	public void Jump()
	{
		if(canJump)
		{
			this.GetComponent<Animator>().SetTrigger ("Jump");
		}
	}

	/* Unity */ void _Jump()
	{
		lastJumpPos = transform.position;
		rigidbody2D.AddForce(new Vector2(0, JumpSpeed));
	}

	public void BackToLastJump()
	{
		transform.position = lastJumpPos;
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

/** Privates Attributes ******************************************/

	private Vector3 lastJumpPos;

/** Callback Unity ***********************************************/

	/* Unity */ void Start()
	{
		lastJumpPos = transform.position;
	}

	/* Unity */ void Update()
	{
		canJump = (this.rigidbody2D.velocity.y == 0);
	}
}
