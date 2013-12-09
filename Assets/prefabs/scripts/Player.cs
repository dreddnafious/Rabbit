using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

	private int _score = 0;
	private int _startingScore = 0;
	private int _startingLives = 1;
	private int _lives = 0;


	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onPelletEaten(), OnPelletEaten);
		Messenger.AddListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);
		//Messenger.AddListener(EventDictionary.Instance.onPlaying(), OnPlaying);
		Messenger.AddListener(EventDictionary.Instance.onGameReset(), OnReset);


	}

	void OnPelletEaten()
	{

		_score += 20;
		Messenger.Broadcast<int>(EventDictionary.Instance.onScoreChanged(), _score);

		if(_score % 1000 == 0)//if the score can be evenly divided by 1000 you gain a life 
		{
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

		Messenger.Broadcast<int>(EventDictionary.Instance.onScoreChanged(), _score);
		Messenger.Broadcast<int>(EventDictionary.Instance.onLivesChanged(), _lives);

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
