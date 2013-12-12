/// <summary>
/// Game system.
/// The primary state machine that determines the rest of the games actions.
/// Calls the main screens and levels
/// Determines the end of the game and the start
/// Determines whether the game is running or paused.
/// </summary>


using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour
{

	private enum GameStates
	{
		Idle,
		StartMenu,
		LevelStart,
		Playing,
		LevelEnd,
		GameOver,
		ResetGame,


	};

	GameStates gameState = GameStates.StartMenu;


	private bool startMenuOnce = true;
	private bool PlayingOnce = true;
	private bool GameOverOnce = true;
	private bool LevelEndOnce = true;
	private bool LevelStartOnce = true;

	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onStartClicked(), OnStartClicked);
		Messenger.AddListener(EventDictionary.Instance.onOutOfLives(), GameOver);
		Messenger.AddListener(EventDictionary.Instance.onGameOverMenuDone(), OnGameOverMenuDone);
		Messenger.AddListener(EventDictionary.Instance.onLevelClearedScreenDone(), OnLevelClearedScreenDone);
		Messenger.AddListener(EventDictionary.Instance.onLevelStartScreenDone(), OnLevelStartScreenDone);
		Messenger.AddListener(EventDictionary.Instance.onMapClear(), OnMapClear);


	}
	
	// Update is called once per frame
	void Update ()
	{
		switch (gameState)
		{

		case GameStates.StartMenu:
			StartMenu();
			break;
		case GameStates.Playing:
			Playing();
			break;
		case GameStates.LevelEnd:
			LevelEnd();
			break;
		case GameStates.LevelStart:
			LevelStart();
			break;
		case GameStates.GameOver:
			GameOver();
			break;



		}
	
	}

	private void OnStartClicked()
	{
		//gameState = GameStates.Playing;//should be GameStates.LevelStarted eventually
		gameState = GameStates.LevelStart;
		PlayingOnce = true;
	}

	private void OnGameOverMenuDone()
	{
		//this resets the game
		startMenuOnce = true;
		PlayingOnce = true;
		LevelStartOnce = true;
		gameState = GameStates.StartMenu;
		Messenger.Broadcast(EventDictionary.Instance.onGameReset());

	}

	private void OnMapClear()
	{
		//the pellet array has signaled that all of the pellets are gone
		gameState = GameStates.LevelEnd;
		Debug.Log("Winner!");

	}


	private void StartMenu()
	{
		//if(startMenuOnce == true)
		//{//first run
			//call the audio system with menu music.
			//call the HUD System for the main menu.
			//startMenuOnce = false;
			Messenger.Broadcast(EventDictionary.Instance.onGameMenu());
			gameState = GameStates.Idle;
	//	}

	}

	private void LevelStart()
	{
		//show the level and signal to the HUD to show the Level# Screen
		//after a set amount of time switch to playing mode.

		Messenger.Broadcast(EventDictionary.Instance.onLevelStarted());
		gameState = GameStates.Idle;

	}

	private void Playing()
	{

		if(PlayingOnce == true)
		{//first run
			//put the input system in play mode
			//put the wolf den in play mode
			//put the AI play mode
			//put the audio system in play mode
			PlayingOnce = false;
			Messenger.Broadcast(EventDictionary.Instance.onPlaying());
		}


	}




	private void LevelEnd()
	{
	//	if(LevelEndOnce == true)
	//	{
		//show the Level# Complete! Screen for a set amount of time.
		//tally the level bonus for the player
		//then switch to Level Start State.
			//LevelEndOnce = false;

			Messenger.Broadcast(EventDictionary.Instance.onLevelEnding());
			gameState = GameStates.Idle;

	//	}

	}

	private void OnLevelClearedScreenDone()
	{
		LevelEndOnce = true;
		Messenger.Broadcast(EventDictionary.Instance.onLevelReset());
		PlayingOnce = true;
		//gameState = GameStates.Playing;
		gameState = GameStates.LevelStart;

	}


	private void OnLevelStartScreenDone()
	{

		gameState = GameStates.Playing;
		PlayingOnce = true;
	}

	private void GameOver()
	{
		if(GameOverOnce == true)
		{//first run
			//put the input system in gameover mode
			//put the audio system in game over mode
			//put the wolf den and AI in game over mode
			//switch to the game menu state
			Messenger.Broadcast(EventDictionary.Instance.onGameOver());
		}


	}

}
