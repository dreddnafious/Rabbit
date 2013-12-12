using UnityEngine;
using System.Collections;

public class wolfcollision : MonoBehaviour
{


	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);
		Messenger.AddListener(EventDictionary.Instance.onLevelEnding(), OnLevelEnding);


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

		CleanUp();

	}

	void OnLevelEnding()
	{
		CleanUp();

	}


	void CleanUp()
	{
		Messenger.RemoveListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);
		Messenger.RemoveListener(EventDictionary.Instance.onLevelEnding(), OnLevelEnding);
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
