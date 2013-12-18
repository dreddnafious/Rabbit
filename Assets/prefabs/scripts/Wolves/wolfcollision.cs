using UnityEngine;
using System.Collections;

public class wolfcollision : MonoBehaviour
{

	Transform myTransform;


	private wolfRandomMove randomMoveAI;
	private WolfFollow     wolfFollowAI;

	private enum Wolfstates
	{
		predator,
		prey,
	};

	private enum WolfAIs
	{
		random,
		follow,
	};


	Wolfstates _wolfState = Wolfstates.predator;
	WolfAIs _wolfAI = WolfAIs.random;


	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);
		Messenger.AddListener(EventDictionary.Instance.onLevelEnding(), OnLevelEnding);

		Messenger.AddListener(EventDictionary.Instance.onCarrotEaten(), OnCarrotEaten);
		Messenger.AddListener(EventDictionary.Instance.onPowerUpDone(), OnPowerUpDone);


	}

	void OnCarrotEaten()
	{
		_wolfState = Wolfstates.prey;
	}

	void OnPowerUpDone()
	{
		_wolfState = Wolfstates.predator;
	}

	// Use this for initialization
	void Start ()
	{


		myTransform = transform;
		//grab a reference to the wolf AI

		if(randomMoveAI = gameObject.GetComponent<wolfRandomMove>())
		{
			_wolfAI = WolfAIs.random;
		}
		if(wolfFollowAI = gameObject.GetComponent<WolfFollow>())
		{
			_wolfAI = WolfAIs.follow;
		}


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
		//Messenger.Broadcast(EventDictionary.Instance.onWolfCleanUp());

		switch(_wolfAI)
		{
		case WolfAIs.random:
		randomMoveAI.CleanUp();
			break;
		case WolfAIs.follow:
			wolfFollowAI.CleanUp();
			break;
		}
		Debug.Log("wolf controller cleanup called");
		Messenger.RemoveListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);
		Messenger.RemoveListener(EventDictionary.Instance.onLevelEnding(), OnLevelEnding);
		Messenger.RemoveListener(EventDictionary.Instance.onCarrotEaten(), OnCarrotEaten);
		Messenger.RemoveListener(EventDictionary.Instance.onPowerUpDone(), OnPowerUpDone);
		Destroy(gameObject);

	}

	void OnTriggerEnter(Collider other)
	{

		//Debug.Log("Some collision");
		
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("inside collision with wolf and player");
			switch(_wolfState)
			{
			case Wolfstates.predator:
				Predator();
				break;
			case Wolfstates.prey:
				Prey();
				break;
			}



			//Messenger.Broadcast(EventDictionary.Instance.onPlayerKilled());
			//Debug.Log("Carnage!");
			//Destroy(gameObject);
		}
		
	}

	void Predator()
	{
		Debug.Log("wolf predator called");
		Messenger.Broadcast(EventDictionary.Instance.onPlayerKilled());
	}

	void Prey()
	{
		Debug.Log("on wolf killed called");
		Messenger.Broadcast(EventDictionary.Instance.onWolfKilled(), myTransform);
		
		CleanUp();
	}


}
