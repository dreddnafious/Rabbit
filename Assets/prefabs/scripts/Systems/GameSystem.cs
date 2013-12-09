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

	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onStartClicked(), OnStartClicked);
		Messenger.AddListener(EventDictionary.Instance.onOutOfLives(), GameOver);
		Messenger.AddListener(EventDictionary.Instance.onGameOverMenuDone(), OnGameOverMenuDone);


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
		case GameStates.GameOver:
			GameOver();
			break;



		}
	
	}

	private void OnStartClicked()
	{
		gameState = GameStates.Playing;//should be GameStates.LevelStarted eventually
		PlayingOnce = true;
	}

	private void OnGameOverMenuDone()
	{
		//this resets the game
		startMenuOnce = true;
		PlayingOnce = true;
		gameState = GameStates.StartMenu;
		Messenger.Broadcast(EventDictionary.Instance.onGameReset());

	}


	private void StartMenu()
	{
		if(startMenuOnce == true)
		{//first run
			//call the audio system with menu music.
			//call the HUD System for the main menu.
			startMenuOnce = false;
			Messenger.Broadcast(EventDictionary.Instance.onGameMenu());
		}

	}

	private void LevelStart()
	{
		//show the level and signal to the HUD to show the Level# Screen
		//after a set amount of time switch to playing mode.


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
		//show the Level# Complete! Screen for a set amount of time.
		//tally the level bonus for the player
		//then switch to Level Start State.

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
