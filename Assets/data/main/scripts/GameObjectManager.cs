using UnityEngine;
using System.Collections;

public class GameObjectManager : MonoBehaviour {

/** Action Methods **********************/

	public void Disable(string gameObjectName)
	{
		GameObject target = GameObject.Find(gameObjectName);

		if (target != null)
		{
			for(int i = 0 ; i < target.transform.childCount; i++)
			{
				target.transform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}

	public void Disable(string gameObjectName, string targetName)
	{
		GameObject target = GameObject.Find(gameObjectName);
		
		if (target != null)
		{
			for(int i = 0 ; i < target.transform.childCount; i++)
			{
				GameObject child = target.transform.GetChild(i).gameObject;

				if(child.name == targetName)
				{
					child.SetActive(false);
				}
			}
		}
	}

	public void Enable(string gameObjectName)
	{
		GameObject target = GameObject.Find(gameObjectName);

		if (target != null)
		{
			for(int i = 0 ; i < target.transform.childCount; i++)
			{
				target.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
	}

	public void Enable(string gameObjectName, string targetName)
	{
		GameObject target = GameObject.Find(gameObjectName);
		
		if (target != null)
		{
			for(int i = 0 ; i < target.transform.childCount; i++)
			{
				GameObject child = target.transform.GetChild(i).gameObject;
				
				if(child.name == targetName)
				{
					child.SetActive(true);
				}
			}
		}
	}

	public void Wait(float time, Action callbackAction) 
	{
		this.currentWaitingCallBack = callbackAction;
		this.timeForlaunchTheCallback = Time.time + time;
	}

/** Callback Unity **********************/

	/* Unity */ void Update()
	{
		if(this.currentWaitingCallBack != null && Time.time >= this.timeForlaunchTheCallback)
		{
			this.currentWaitingCallBack.Perform(null);

			this.currentWaitingCallBack = null;
			this.timeForlaunchTheCallback = -1;
		}
	}

/** Private Attributes ******************/

	/** Current Waiting action.
	 */
	private Action currentWaitingCallBack = null;

	/** Time when lanching waiting callback.
	 */
	private float timeForlaunchTheCallback = -1;
}
