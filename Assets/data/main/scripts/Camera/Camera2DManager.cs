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

/** Callback Unity *******************************/

	/* Unity */ void Update () {
		if(target != null) 
		{
			float x = target.transform.localPosition.x - this.transform.position.x + this.deltaPosition.x;
			float y = target.transform.localPosition.y - this.transform.position.y + this.deltaPosition.y;

			this.transform.Translate(x, y, 0);
		}
	}
}
