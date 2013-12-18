using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	private int _score = 0;
	private int _startingScore = 0;
	private int _startingLives = 3;
	private int _lives = 0;


	private int _nextLife = 10000;
	private int _currentLevel = 0;


	//power up stuff
	private float _powerUpDuration = 4.0f;
	private float _powerUpTimer = 0.0f;


	private enum PlayerStates
	{
		normal,
		poweredup,
	}

	PlayerStates playerState = PlayerStates.normal;

	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onPelletEaten(), OnPelletEaten);
		Messenger.AddListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);
		//Messenger.AddListener(EventDictionary.Instance.onPlaying(), OnPlaying);
		Messenger.AddListener(EventDictionary.Instance.onGameReset(), OnReset);
		Messenger.AddListener(EventDictionary.Instance.onLevelStarted(), OnLevelStarted);


		Messenger.AddListener(EventDictionary.Instance.onCarrotEaten(), OnCarrotEaten);
		Messenger.AddListener<Transform>(EventDictionary.Instance.onWolfKilled(), OnWolfKilled);


	}


	void Update()
	{

		switch(playerState)
		{
		case PlayerStates.poweredup:
			PoweredUp();
			break;
		}

	}

	void PoweredUp()
	{
		_powerUpTimer += Time.deltaTime;


		if(_powerUpTimer >= _powerUpDuration)
		{
			_powerUpTimer = 0.0f;
			playerState = PlayerStates.normal;
			Messenger.Broadcast(EventDictionary.Instance.onPowerUpDone());
			Debug.Log("on Power up done called from player object");
		}

	}

	void OnCarrotEaten()
	{

		playerState = PlayerStates.poweredup;
		_powerUpTimer = 0.0f;
		/*
		Time.timeScale = 0.33f;
		Time.fixedDeltaTime = 0.33f;
		Time.maximumDeltaTime = 0.33f;
		*/

	}

	void OnPelletEaten()
	{

		_score += 150;
		Messenger.Broadcast<int>(EventDictionary.Instance.onScoreChanged(), _score);
		TestLifeUp();


	}

	void OnWolfKilled(Transform temp_Transform)
	{
		_score += 500;
		Messenger.Broadcast<int>(EventDictionary.Instance.onScoreChanged(), _score);
		TestLifeUp();

	}

	/// <summary>
	/// Tests the score to see if the player has earned an extra life.
	/// </summary>
	void TestLifeUp()
	{
		if(_score >= _nextLife)
		{
			_nextLife += 10000;
			//add a life
			_lives++;
			Messenger.Broadcast<int>(EventDictionary.Instance.onLivesChanged(), _lives);//calls the GUI
			Messenger.Broadcast(EventDictionary.Instance.onLifeGained());//calls the Sound System
			
		}

	}

	void OnPlayerKilled()
	{
		_lives--;
		Messenger.Broadcast<int>(EventDictionary.Instance.onLivesChanged(), _lives);
		Messenger.Broadcast(EventDictionary.Instance.onPowerUpDone());


		if(_lives <= 0)
		{
			//out of lives, game over
			//calling this from this object may be evidence of a leaky abstraction
			//consider pushing this to the gamestate system if this becomes an issue
			Messenger.Broadcast(EventDictionary.Instance.onOutOfLives());
		}

	}

	void OnReset()
	{
		_lives = _startingLives;
		_score = _startingScore;
		_nextLife = 10000;
		_currentLevel = 0;

		Messenger.Broadcast<int>(EventDictionary.Instance.onScoreChanged(), _score);
		Messenger.Broadcast<int>(EventDictionary.Instance.onLivesChanged(), _lives);
		Messenger.Broadcast<int>(EventDictionary.Instance.onLevelChanged(), _currentLevel);

	}

	void OnLevelStarted()
	{

		_currentLevel++;
		Messenger.Broadcast<int>(EventDictionary.Instance.onLevelChanged(), _currentLevel);
		Messenger.Broadcast(EventDictionary.Instance.onPowerUpDone());
	}

	// Use this for initialization
	void Start ()
	{
		_lives = _startingLives;
		_score = _startingScore;
		Messenger.Broadcast<int>(EventDictionary.Instance.onScoreChanged(), _score);
		Messenger.Broadcast<int>(EventDictionary.Instance.onLivesChanged(), _lives);
	
	}


}
