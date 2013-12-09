using UnityEngine;
using System.Collections;

public class HUDSystem : MonoBehaviour
{

	public Texture startmenu_background;


	private enum HUDStates
	{
		Idle,
		Menu,
		LevelStart,
		Playing,
		LevelEnding,
		GameOver,
	};


	HUDStates hudState = HUDStates.Idle;


	//private int _playerLives = 3;
	//private int _playerScore = 0;


	private string _scoreString;
	private string _lifeString;


	private float _showGameOverInSeconds = 5.0f;
	private float _GameOverScreenTimer = 0.0f;


	void OnEnable()
	{


		//this is for the major game states
		Messenger.AddListener(EventDictionary.Instance.onGameMenu(), OnGameMenu);
		Messenger.AddListener(EventDictionary.Instance.onLevelStarted(), OnLevelStart);
		Messenger.AddListener(EventDictionary.Instance.onPlaying(), OnPlaying);
		Messenger.AddListener(EventDictionary.Instance.onLevelEnding(), OnLevelEnd);
		Messenger.AddListener(EventDictionary.Instance.onGameOver(), OnGameOver);

		Messenger.AddListener<int>(EventDictionary.Instance.onScoreChanged(), OnScoreChanged);
		Messenger.AddListener<int>(EventDictionary.Instance.onLivesChanged(), OnLifeChanged);

	}

	#region GameStates Receivers
	void OnGameMenu()
	{
		hudState = HUDStates.Menu;
	}

	void OnLevelStart()
	{
		hudState = HUDStates.LevelStart;
	}

	void OnPlaying()
	{
		hudState = HUDStates.Playing;
	}

	void OnLevelEnd()
	{
		hudState = HUDStates.LevelEnding;
	}

	void OnGameOver()
	{
		hudState = HUDStates.GameOver;
	}
	#endregion


	void OnScoreChanged(int newScore)
	{
		_scoreString = newScore.ToString();
	}

	void OnLifeChanged(int newLives)
	{
		_lifeString = newLives.ToString();
	}



	void OnGUI()
	{

		switch(hudState)
		{
		case HUDStates.Menu:
			Menu ();
			break;
		case HUDStates.LevelStart:
			LevelStart();
			break;
		case HUDStates.Playing:
			Playing();
			break;
		case HUDStates.LevelEnding:
			LevelEnding();
			break;
		case HUDStates.GameOver:
			GameOver();
			break;


		}


	}

	void Menu()
	{
		GUI.skin.box.fontSize = 50;
		//GUI.Box(new Rect (0,0,Screen.width,Screen.height), "Rabbit"); 
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), startmenu_background);
		GUI.Box(new Rect ((Screen.width/2) - 100,(Screen.height/2) - 100,150,100), "Rabbit");

		if(GUI.Button(new Rect((Screen.width/2) - 70,(Screen.height/2) -40,100,50), "Start"))
		{
			//button was clicked
			Debug.Log("start menu button was clicked");
			Messenger.Broadcast(EventDictionary.Instance.onStartClicked());


		}
	}
	void LevelStart()
	{
	}
	void Playing()
	{
		//GUI.Box (new Rect (0,0,100,50), "Top-left");
		//GUI.Box (new Rect (Screen.width - 100,0,100,50), "Top-right");
		//GUI.Box (new Rect (0,Screen.height - 50,100,50), "Bottom-left");
		//GUI.Box (new Rect (Screen.width - 100,Screen.height - 50,100,50), "Bottom-right");
		//Debug.Log("Inside the HUDSystem.playing function");
		GUI.skin.label.fontSize = 25;
		GUI.Label (new Rect (0,Screen.height - 50,250,100), "Score: " + _scoreString);
		GUI.Label (new Rect (0,Screen.height - 70,250,100), "Lives: " + _lifeString);
	}
	void LevelEnding()
	{
	}
	void GameOver()
	{
		//show the Game Over screen for a little while and then switch back to 
		//the Start Menu.

		_GameOverScreenTimer += Time.deltaTime;
		//Debug.Log("inside GameOver function in HUD");
		if(_GameOverScreenTimer < _showGameOverInSeconds)
		{
		GUI.skin.box.fontSize = 50; 
		GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), startmenu_background);
		GUI.Box(new Rect ((Screen.width/2) - 200,(Screen.height/2) - 100,350,200), "Game Over");

		}
		else
		{
			_GameOverScreenTimer = 0.0f;
			Messenger.Broadcast(EventDictionary.Instance.onGameOverMenuDone());
		}


	}

}
