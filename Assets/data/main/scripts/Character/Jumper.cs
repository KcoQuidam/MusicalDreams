using UnityEngine;
using System.Collections;

/** Player controle on the rectangle character.
 */
public class Jumper : MonoBehaviour 
{
/** Unity Configuration ******************************************/

	public float JumpSpeed = 650;

	public float SwimAcceleration = 25;
	public float MaxSwimVelocity = 25;
	
	public float MoveAcceleration = 25;
	public float MaxMoveVelocity = 25;

	public float onEarthDrag = 0.5f;
	public float onWaterDrag = 5f;

/** Privates Attributes ******************************************/

	private bool canJump = false;
	private bool canSwim = false;

/** State Controle Methods ***************************************/

	/*
	public void GoIntoWater()
	{
		this.rigidbody2D.drag = onWaterDrag;
	}

	public void GoOutWater()
	{
		this.rigidbody2D.drag = onEarthDrag;
	}
	*/
	public void SetSwimming(bool canSwim)
	{
		this.canSwim = canSwim;
	}

/** Interface methods ********************************************/

	public void Jump()
	{
		if(canJump)
		{
			this.GetComponent<Animator>().SetTrigger("Jump");
		}
	}

	/* Unity */ void _Jump()
	{
		rigidbody2D.AddForce(new Vector2(0, JumpSpeed));
	}


	public void Swim()
	{
		if(canSwim && rigidbody2D.velocity.y < MaxSwimVelocity) 
		{
			Debug.Log ("Swimming");

			rigidbody2D.AddForce(new Vector2(0, SwimAcceleration * Time.deltaTime));
		}
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

/** Callback Unity ***********************************************/

	/* Unity */ void Update()
	{
		canJump = (this.rigidbody2D.velocity.y == 0 || canSwim);
	}
}
