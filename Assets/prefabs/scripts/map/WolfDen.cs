/// <summary>
/// Wolf den.
/// Spawns wolves when the level calls for it.
/// </summary>


using UnityEngine;
using System.Collections;

public class WolfDen : MonoBehaviour
{
	public GameObject Random_ChaseWolf;
	public GameObject Direct_ChaseWolf;
	private Transform myTransform;
	private Transform wolfSpawnTransform;




	private float _timeElapsed = 0f;
	private float _spawnRate;
	private int _addToSpawnRate = 3;




	private int _maxWolvesSpawnedAtOnce = 0;
	private int _currentWolvesSpawned = 0;

	private enum DenStates
	{
		Idle,
		Playing,

	};

	DenStates wolfDenStates = DenStates.Idle;

	void OnEnable()
	{
		//Messenger.AddListener(EventDictionary.Instance.onPlaying(), OnPlaying);
		Messenger.AddListener(EventDictionary.Instance.onGameOver(), OnGameOver);
		Messenger.AddListener(EventDictionary.Instance.onLevelEnding(), OnLevelEnding);
		Messenger.AddListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);
		Messenger.AddListener(EventDictionary.Instance.onCarrotEaten(), OnCarrotEaten);
		Messenger.AddListener(EventDictionary.Instance.onPowerUpDone(), OnPowerUpDone);

		//Messenger.AddListener(EventDictionary.Instance.onLevelStartScreenDone(), OnPlaying);
		Messenger.AddListener<int>(EventDictionary.Instance.onLevelChanged(), OnLevelChanged);
		Messenger.AddListener(EventDictionary.Instance.onLevelStartScreenDone(), OnLevelStartScreenDone);
		Messenger.AddListener<Transform>(EventDictionary.Instance.onWolfKilled(), OnWolfKilled);
	}


	void OnLevelChanged(int level)
	{//i want to add 1 wolf max spawn per 10 levels.

		float tempAdd = 0;
		tempAdd = (float)level / 5.0f;//10.0f
		tempAdd =  (int)tempAdd;
		_maxWolvesSpawnedAtOnce = (int)(4 + tempAdd);//was 5
		_timeElapsed = 0.0f;
		//wolfDenStates = DenStates.Playing;

	}

	void OnLevelStartScreenDone()
	{
		_timeElapsed = 0.0f;
		wolfDenStates = DenStates.Playing;
	}

	void OnWolfKilled(Transform temp_Transform)
	{
		_currentWolvesSpawned--;
	}

	// Use this for initialization
	void Start ()
	{
		myTransform = transform;
		wolfSpawnTransform = myTransform.FindChild("wolfspawn");
	
	}

	void OnCarrotEaten()
	{
		wolfDenStates = DenStates.Idle;
	}

	void OnPowerUpDone()
	{
		wolfDenStates = DenStates.Playing;
	}



	void OnPlaying()
	{
		wolfDenStates = DenStates.Playing;
	}

	void OnGameOver()
	{
		wolfDenStates = DenStates.Idle;
		//_spawnRate = _resetSpawnRate;
		_currentWolvesSpawned = 0;
		_timeElapsed = 0.0f;
	}

	void OnLevelEnding()
	{
		wolfDenStates = DenStates.Idle;
		//_spawnRate = _resetSpawnRate;
		_currentWolvesSpawned = 0;
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
		_spawnRate = _currentWolvesSpawned + _addToSpawnRate;

		_timeElapsed += Time.deltaTime;
		
		if(_timeElapsed >= _spawnRate)
		{
			if(_currentWolvesSpawned < _maxWolvesSpawnedAtOnce)
			{
			_timeElapsed = 0f;

				_currentWolvesSpawned++;


				if(_currentWolvesSpawned % 3 == 0)
				{
					Instantiate(Direct_ChaseWolf, wolfSpawnTransform.position, Quaternion.identity);
				}
				else
				{			
					Instantiate(Random_ChaseWolf, wolfSpawnTransform.position, Quaternion.identity);
				}

			Messenger.Broadcast(EventDictionary.Instance.onWolfSpawned());
			

			

			}
			
		}


	}


	void OnPlayerKilled()
	{
		//_spawnRate = _resetSpawnRate;
		//_timeElapsed = _spawnRate;

		_currentWolvesSpawned = 0;
		_timeElapsed = 0;


	}

}
