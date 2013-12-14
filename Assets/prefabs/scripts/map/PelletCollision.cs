using UnityEngine;
using System.Collections;

public class PelletCollision : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnTriggerEnter(Collider other)
	{

		if(other.gameObject.tag == "Player")
		{
			Messenger.Broadcast(EventDictionary.Instance.onPelletEaten());
			//Debug.Log(" nom, nom, nom!");
			//Destroy(gameObject);
			gameObject.SetActive(false);
		}

	}


}
