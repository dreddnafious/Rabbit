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
	private string _levelString;

	//for game over screen
	private float _showGameOverInSeconds = 5.0f;
	private float _GameOverScreenTimer = 0.0f;

	//for level cleared screen
	private float _showLevelClearedInSeconds = 5.0f;
	private float _LevelClearedScreenTimer = 0.0f;

	//for level start screen
	private float _showLevelStartedInSeconds = 5.0f;
	private float _LevelStartedScreenTimer = 0.0f;




	private Rect _lifeRect;
	private Rect _scoreRect;
	private Rect _levelClearedRect;
	private Rect _levelStartRect;


	private GUIStyle _scoreStyle;
	private GUIStyle _lifeStyle;
	private GUIStyle _levelClearedStyle;



	/// <summary>
	/// Raises the enable event - Map all of the events the HUD must respond to.
	/// </summary>
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
		Messenger.AddListener<int>(EventDictionary.Instance.onLevelChanged(), OnLevelChanged);

	}


	void Start()
	{
		_lifeRect = new Rect(10,Screen.height - 30,50,30);
		_scoreRect = new Rect((Screen.width/2) - 125,0,250,30);
		_levelClearedRect = new Rect((Screen.width/2) - 125, (Screen.height/2) - 50, 250, 100);
		_levelStartRect = new Rect((Screen.width/2) - 125, (Screen.height/2) - 100, 250, 100);

		_scoreStyle = new GUIStyle();
		_scoreStyle.alignment = TextAnchor.MiddleCenter;
		_scoreStyle.normal.textColor = Color.yellow;
		_scoreStyle.fontSize = 20;




		_lifeStyle =  new GUIStyle();
		_lifeStyle.alignment = TextAnchor.MiddleCenter;
		_lifeStyle.normal.textColor = Color.red;
		_lifeStyle.fontSize = 20;

		_levelClearedStyle = new GUIStyle();
		_levelClearedStyle.alignment = TextAnchor.MiddleCenter;
		_levelClearedStyle.normal.textColor = Color.red;
		_levelClearedStyle.fontSize = 40;

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

	void OnLevelChanged(int newLevel)
	{
		_levelString = newLevel.ToString();
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
			//Debug.Log("start menu button was clicked");
			Messenger.Broadcast(EventDictionary.Instance.onStartClicked());


		}
	}
	void LevelStart()
	{

			_LevelStartedScreenTimer += Time.deltaTime;

			if(_LevelStartedScreenTimer < _showLevelStartedInSeconds)
			{
			GUI.Label(_levelStartRect, "Level  " + _levelString, _levelClearedStyle);
			}
			else
			{
				_LevelStartedScreenTimer = 0.0f;
				Messenger.Broadcast(EventDictionary.Instance.onLevelStartScreenDone());

			}


	}
	void Playing()
	{
		//GUI.Box (new Rect (0,0,100,50), "Top-left");
		//GUI.Box (new Rect (Screen.width - 100,0,100,50), "Top-right");
		//GUI.Box (new Rect (0,Screen.height - 50,100,50), "Bottom-left");
		//GUI.Box (new Rect (Screen.width - 100,Screen.height - 50,100,50), "Bottom-right");
		//Debug.Log("Inside the HUDSystem.playing function");
		//GUI.skin.label.fontSize = 25;
		GUI.Label (_scoreRect, "Score: " + _scoreString, _scoreStyle);
		GUI.Label (_lifeRect, "Lives: " + _lifeString, _lifeStyle);
	}
	void LevelEnding()
	{

		_LevelClearedScreenTimer += Time.deltaTime;

		if(_LevelClearedScreenTimer < _showLevelClearedInSeconds)//counting seconds to show this screen
		{//draw level cleared label

			GUI.Label(_levelClearedRect, "Level Cleared!", _levelClearedStyle);


		}
		else
		{
			_LevelClearedScreenTimer = 0.0f;
			Messenger.Broadcast(EventDictionary.Instance.onLevelClearedScreenDone());
			hudState = HUDStates.Playing;
		}

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
