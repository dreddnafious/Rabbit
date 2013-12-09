using UnityEngine;
using System.Collections;

public class WolfFollow : MonoBehaviour
{

	public GameObject target;

	NavMeshAgent navAgent;
	private GameObject Rabbit;

	// Use this for initialization
	void Start ()
	{
		navAgent = GetComponent<NavMeshAgent>();
		Rabbit = GameObject.Find("rabbit");
	}
	
	// Update is called once per frame
	void Update ()
	{
		navAgent.destination = Rabbit.transform.position;
		//navAgent.destination = target.transform.position;
	}

}
