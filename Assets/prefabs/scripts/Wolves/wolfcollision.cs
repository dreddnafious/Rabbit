using UnityEngine;
using System.Collections;

public class wolfcollision : MonoBehaviour
{


	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);



	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnPlayerKilled()
	{

		Messenger.RemoveListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);
		Destroy(gameObject);

	}

	void OnTriggerEnter(Collider other)
	{

		//Debug.Log("Some collision");
		
		if(other.gameObject.tag == "Player")
		{
			Messenger.Broadcast(EventDictionary.Instance.onPlayerKilled());
			Debug.Log("Carnage!");
			//Destroy(gameObject);
		}
		
	}


}
