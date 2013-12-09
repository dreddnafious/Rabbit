/// <summary>
/// Wolf den.
/// Spawns wolves when the level calls for it.
/// </summary>


using UnityEngine;
using System.Collections;

public class WolfDen : MonoBehaviour
{
	public GameObject ChaseWolf;
	private Transform myTransform;
	private Transform wolfSpawnTransform;




	private float _timeElapsed = 0f;
	private float _spawnRate = 3.0f;
	private float _resetSpawnRate = 3.0f;


	private enum DenStates
	{
		Idle,
		Playing,

	};

	DenStates wolfDenStates = DenStates.Idle;

	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onPlaying(), OnPlaying);
		Messenger.AddListener(EventDictionary.Instance.onGameOver(), OnGameOver);
	}

	// Use this for initialization
	void Start ()
	{
		myTransform = transform;
		wolfSpawnTransform = myTransform.FindChild("wolfspawn");
	
	}



	void OnPlaying()
	{
		wolfDenStates = DenStates.Playing;
	}

	void OnGameOver()
	{
		wolfDenStates = DenStates.Idle;
		_spawnRate = _resetSpawnRate;
		_timeElapsed = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{


		switch (wolfDenStates)
		{
		case DenStates.Playing:
			Playing ();
			break;

		}



	
	}

	void Playing()
	{
		_timeElapsed += Time.deltaTime;
		
		if(_timeElapsed >= _spawnRate)
		{
			_timeElapsed = 0f;
			_spawnRate += 3.0f;
			Instantiate(ChaseWolf, wolfSpawnTransform.position, Quaternion.identity);
			Messenger.Broadcast(EventDictionary.Instance.onWolfSpawned());
			//Instantiate(ChaseWolf, myTransform, Quaternion.identity) as GameObject;
			
		}


	}

}
