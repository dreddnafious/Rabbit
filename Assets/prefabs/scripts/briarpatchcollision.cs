using UnityEngine;
using System.Collections;

public class briarpatchcollision : MonoBehaviour
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
			Debug.Log("Oh No! Not the Briar Patch");
			//teleport player to random spot near other briar patch
		}
		
	}
}
