using UnityEngine;
using System.Collections;

public class SoundSystem : MonoBehaviour
{
	//a list of audio clips to play
	//link all sound clips here.
	public AudioClip sound_pelletEaten;
	public AudioClip sound_wolfSpawned;
	public AudioClip sound_playerDead;
	public AudioClip sound_LifeUp;
	public AudioClip sound_LevelCleared;
	public AudioClip sound_wolfKilled;
	public AudioClip sound_powerUp;

	
	void OnEnable()
	{
		//add your listeners here.
		//Messenger.AddListener<Vector3, PlayerStats>(EventDictionary.Instance.onGrenadeHit(), OnGrenadeHit);
		Messenger.AddListener(EventDictionary.Instance.onPelletEaten(), OnPelletEaten);	
		Messenger.AddListener(EventDictionary.Instance.onWolfSpawned(), OnWolfSpawned);
		Messenger.AddListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);
		Messenger.AddListener(EventDictionary.Instance.onLifeGained(), OnLifeGained);
		Messenger.AddListener(EventDictionary.Instance.onLevelEnding(), OnLevelEnding);
		Messenger.AddListener(EventDictionary.Instance.onCarrotEaten(), OnPowerUp);


		Messenger.AddListener<Transform>(EventDictionary.Instance.onWolfKilled(), OnWolfKilled);

	}
	
	
	void OnDisable()
	{
		//Messenger.RemoveListener(EventDictionary.Instance.onRifleFired(), OnRifleFired);
		
	}
	
	// Use this for initialization
	void Start ()
	{
		
	}

	void OnWolfKilled(Transform temp_Transform)
	{
		audio.PlayOneShot(sound_wolfKilled);
	}
	
	void OnPelletEaten()
	{

		audio.PlayOneShot(sound_pelletEaten);
	}

	void OnWolfSpawned()
	{
		//Debug.Log("wolf spawned sound called.");
		audio.PlayOneShot(sound_wolfSpawned);
	}

	void OnPlayerKilled()
	{
		audio.PlayOneShot(sound_playerDead);
	}

	void OnLifeGained()
	{
		audio.PlayOneShot(sound_LifeUp);
	}

	void OnLevelEnding()
	{
		audio.PlayOneShot(sound_LevelCleared);
	}

	void OnPowerUp()
	{
		audio.PlayOneShot(sound_powerUp);
	}
	
	/*
	void OnRifleFired(Vector3 position, Vector3 targetposition, PlayerStats pStats)
	{
		audio.PlayOneShot(sound_laserRifle);	
		
	}

	
	void OnHealthZero(Vector3 tmpVec)
	{
		float lastvolume = audio.volume;
		audio.volume = 1.0f;
		//Debug.Log("Boom Sound");
		audio.PlayOneShot(sound_minorExplosion);
		audio.volume = lastvolume;
		
	}
	*/

}
