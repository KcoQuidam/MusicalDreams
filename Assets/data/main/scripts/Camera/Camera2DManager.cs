using UnityEngine;
using System.Collections;

/** Make an object follow another.
 */
public class Camera2DManager : MonoBehaviour {
/** Unity Configuration **************************/

	/** Delta from target position.
	 */ 
	public Vector2 deltaPosition = new Vector2(0, 0);

	/** Target to follow.
	 */
	public GameObject target;

/** Actions Method *******************************/

	public void SetDeltaPosition(float x, float y) {
		deltaPosition.x = x;
		deltaPosition.y = y;
	}

	public void SwitchTo(string gameObjectName) {
		this.SwitchTo(gameObjectName, null);
	}

	public void SwitchTo(string gameObjectName, Action callbackAction) {
		this.SwitchTo(gameObjectName, 1f, callbackAction);
	}

	public void SwitchTo(string gameObjectName, float timeToTravel, Action callbackAction) {
		this.SwitchTo(gameObjectName, timeToTravel, 0.001f, callbackAction);
	}

	public void SwitchTo(string gameObjectName, float timeToTravel, float travelingPrecision, Action callbackAction) {
		if (nextTarget != null) { return; }

		nextTarget = GameObject.Find(gameObjectName);

		if (nextTarget == null) { Debug.LogWarning("target " + gameObjectName + " Not found. Camera stay here."); return; }

		actionToPerform = callbackAction;

		if (timeToTravel <= 0) {
			target = nextTarget;
			nextTarget = null;

			callbackAction.Perform(target);
		}
		else
		{
			this.travelingPrecision = travelingPrecision;
			xTarget = nextTarget.transform.position.x + this.deltaPosition.x;
			yTarget = nextTarget.transform.position.y + this.deltaPosition.y;
			travelingTime = timeToTravel;
		}
	}

/** Callback Unity *******************************/

	/* Unity */ void Update () {

		float x = 0;
		float y = 0;

		if(nextTarget != null) {
			x = (xTarget - this.transform.position.x)*Time.deltaTime/travelingTime;
			y = (yTarget - this.transform.position.y)*Time.deltaTime/travelingTime;

			if(x  < this.travelingPrecision && x > -this.travelingPrecision) {
				if(actionToPerform != null) {
					actionToPerform.Perform(nextTarget);
				}

				target = nextTarget;
				this.deltaPosition.x += x;
				this.deltaPosition.y += y;

				actionToPerform = null;
				nextTarget = null;
				travelingTime = 0; 
			}
		}
		else if(target != null)
		{
			x = target.transform.position.x - this.transform.position.x + this.deltaPosition.x;
			y = target.transform.position.y - this.transform.position.y + this.deltaPosition.y;
		}

		this.transform.Translate(x, y, 0);
	}

/** Internal Attributes *************************/

	/** Target where the camera is going.
	 */
	private GameObject nextTarget;
	private float xTarget;
	private float yTarget;

	/** Time for the current target traveling.
	 */
	private float travelingTime = 0;

	/** Action to perfom when traveling is done.
	 */
	private Action actionToPerform;

	/** Current traveling precision.
	 */
	private float travelingPrecision = 0.005f;
}
