/// <summary>
/// Wolf random move.
/// Grabs all of the empty game objects named "random"
/// and builds an array of their position
/// it chooses a position at random and when it arrives chooses another.
/// </summary>

using UnityEngine;
using System.Collections;

public class wolfRandomMove : MonoBehaviour
{

	NavMeshAgent navAgent;
	private GameObject[] randomLocations;
	private int _numofLocations = 0;
	bool firstRun = true;

	private float timer = 0.0f;
	private float hertz = 1.0f;
	
	// Use this for initialization
	void Start ()
	{
		navAgent = GetComponent<NavMeshAgent>();
		randomLocations = GameObject.FindGameObjectsWithTag("random");//build an array of random locations
		_numofLocations = randomLocations.Length;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		if(timer >= hertz)
		{
			timer = 0f;
		//Debug.Log("position = " + navAgent.transform.position);
		//Debug.Log("destination = " + navAgent.destination);
		}


		if(navAgent.transform.position.x == navAgent.destination.x && navAgent.transform.position.z
		   == navAgent.transform.position.z || firstRun == true)//we have arrived at our destination
		{
			firstRun = false;
			int next = Random.Range(0, _numofLocations);//randomly choose a location
			//Debug.Log("next = " + next);
			//choose our next destination
			navAgent.destination = randomLocations[next].transform.position;
		



		}
	}
}
