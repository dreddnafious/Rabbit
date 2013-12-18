using UnityEngine;
using System.Collections;

public class WolfFollow : MonoBehaviour
{

	public GameObject target;

	NavMeshAgent navAgent;
	private GameObject Rabbit;


	private float _moveSpeed = 3.0f;

	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onCarrotEaten(), OnCarrotEaten);
		Messenger.AddListener(EventDictionary.Instance.onPowerUpDone(), OnPowerUpDone);
		//Messenger.AddListener(EventDictionary.Instance.onWolfCleanUp(), CleanUp);
	}

	void OnCarrotEaten()
	{
		_moveSpeed = 1.0f;
		navAgent.speed = _moveSpeed;
	}
	
	void OnPowerUpDone()
	{
		_moveSpeed = 3.0f;
		navAgent.speed = _moveSpeed;
	}


	public void CleanUp()
	{
		Debug.Log("AI cleanup called");
		Messenger.RemoveListener(EventDictionary.Instance.onCarrotEaten(), OnCarrotEaten);
		Messenger.RemoveListener(EventDictionary.Instance.onPowerUpDone(), OnPowerUpDone);
		//Messenger.RemoveListener(EventDictionary.Instance.onWolfCleanUp(), CleanUp);
		
	}

	// Use this for initialization
	void Start ()
	{
		navAgent = GetComponent<NavMeshAgent>();
		Rabbit = GameObject.Find("rabbit");
		navAgent.speed = _moveSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		navAgent.destination = Rabbit.transform.position;
		//navAgent.destination = target.transform.position;
	}

}
