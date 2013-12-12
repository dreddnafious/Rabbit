using UnityEngine;
using System.Collections;


	
	
public class EventDictionary : MonoBehaviour
	{

    private static EventDictionary instance = null;
    public static EventDictionary Instance
		{ 
        get {
            if (instance == null)
            {
                Debug.Log("instantiate Event Dictionary");
                GameObject go = new GameObject();
                instance = go.AddComponent<EventDictionary>();
                go.name = "eventdictionary";
            }

            return instance; 
        } 
    }

	//your event strings go here
		
	/*	
	public string onProjectileHit()
	{
		return "OnProjectileHit";
		
	}	
	*/

	//notifies the sound manager to play a sound and the player to increment the score
	public string onPelletEaten()
	{
		return "OnPelletEaten";
	}

	public string onWolfSpawned()
	{
		return "OnWolfSpawned";
	}
	///*************************
	/// This is for score and life changes
	/// **********************************
	public string onScoreChanged()
	{
		return "OnScoreChanged";
	}

	public string onLivesChanged()
	{
		return "OnLivesChanged";
	}

	public string onLifeGained()
	{
		return "OnLifeGained";
	}

	public string onPlayerKilled()
	{
		return "OnPlayerKilled";
	}


	#region gamestates

	public string onGameMenu()
	{
		return "OnGameMenu";
	}

	public string onLevelStarted()
	{
		return "OnLevelStarted";
	}

	public string onPlaying()
	{
		return "OnPlaying";
	}
	public string onLevelEnding()
	{
		return "OnLevelEnding";
	}
	public string onGameOver()
	{
		return "OnGameOver";
	}

	public string onStartClicked()
	{
		return "OnStartClicked";
	}

	public string onOutOfLives()
	{
		return "OutOfLives";
	}

	public string onGameOverMenuDone()
	{
		return "OnGameOverMenuDone";
	}

	public string onLevelClearedScreenDone()
	{
		return "OnLevelClearedScreenDone";
	}

	public string onLevelStartScreenDone()
	{
		return "OnLevelStartScreenDone";
	}

	public string onLevelChanged()
	{
		return "OnLevelChanged";
	}

	public string onGameReset()
	{
		return "OnResetGame";
	}

	public string onMapClear()
	{
		return "OnMapClear";
	}

	public string onLevelReset()
	{
		return "OnLevelReset";
	}

	#endregion



	///This is for the input messages
	/// *****************************
	#region //this is for the input messages
	public string onMoveUp()
	{
		return "OnMoveUp";
	}

	public string onMoveDown()
	{
		return "OnMoveDown";
	}

	public string onMoveLeft()
	{
		return "OnMoveLeft";
	}

	public string onMoveRight()
	{
		return "OnMoveRight";
	}
	#endregion

}	


	

	

