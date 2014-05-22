using UnityEngine;

public class ColliderHandler : MonoBehaviour 
{
/** Internals Classes & Attributes ***********************************************************************/

	[System.Serializable]
	public class CollisionHandlingInfo
	{
		public string otherName;

		public string onSelf;
		public string onOther;
	}

	public CollisionHandlingInfo[] enterCollisions;
	public CollisionHandlingInfo[] stayCollisions;
	public CollisionHandlingInfo[] exitCollisions;

	public CollisionHandlingInfo[] enterTriggers;
	public CollisionHandlingInfo[] stayTriggers;
	public CollisionHandlingInfo[] exitTriggers;

/** Internal Method **************************************************************************************/

	private void Verify(CollisionHandlingInfo chi, GameObject other)
	{
		if(chi.otherName == other.gameObject.name)
		{
			Handle(this.gameObject, chi.onSelf);
			Handle(other, chi.onOther);
		}
	}
	
	private void Handle(GameObject target, string actionName)
	{
		if(string.IsNullOrEmpty(actionName)) { return; }

		ActionHandler ah = target.GetComponent<ActionHandler>();

		if(ah == null || !ah.PerformLocalAction(actionName))
		{
			ActionHandler.PerformAction(target, actionName);
		}
	}

/** Callbacks Unity **************************************************************************************/

	/* Unity*/ void OnCollisionEnter2D(Collision2D coll)
	{
		foreach(CollisionHandlingInfo chi in enterCollisions)
		{
			if(chi.otherName == coll.gameObject.name)
			{
				Verify(chi, coll.gameObject);
			}
		}
	}

	/* Unity*/ void OnCollisionStay2D(Collision2D coll)
	{
		foreach(CollisionHandlingInfo chi in stayCollisions)
		{
			if(chi.otherName == coll.gameObject.name)
			{
				Verify(chi, coll.gameObject);
			}
		}
	}

	/*Unity*/ void OnCollisionExit2D(Collision2D coll)
	{
		foreach(CollisionHandlingInfo chi in exitCollisions)
		{
			if(chi.otherName == coll.gameObject.name)
			{
				Verify(chi, coll.gameObject);
			}
		}
	}

	/* Unity*/ void OnTriggerEnter2D(Collider2D coll) 
	{
		foreach(CollisionHandlingInfo chi in enterTriggers)
		{
			if(chi.otherName == coll.gameObject.name)
			{
				Verify(chi, coll.gameObject);
			}
		}
	}

	/* Unity*/ void OnTriggerStay2D(Collider2D coll) 
	{
		foreach(CollisionHandlingInfo chi in stayTriggers)
		{
			if(chi.otherName == coll.gameObject.name)
			{
				Verify(chi, coll.gameObject);
			}
		}
	}

	/* Unity*/ void OnTriggerExit2D(Collider2D coll) 
	{
		foreach(CollisionHandlingInfo chi in exitTriggers)
		{
			if(chi.otherName == coll.gameObject.name)
			{
				Verify(chi, coll.gameObject);
			}
		}
	}
}
