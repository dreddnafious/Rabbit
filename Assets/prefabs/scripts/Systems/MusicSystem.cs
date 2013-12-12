using UnityEngine;
using System.Collections;

public class MusicSystem : MonoBehaviour
{

	public AudioClip music_Level_1;

	private AudioSource _audioSource;




	private enum MusicStates
	{

		Idle,
		StartMenu,
		PlayingLevel,


	};

	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onPlaying(), OnPlaying);
		Messenger.AddListener(EventDictionary.Instance.onLevelEnding(), OnLevelEnd);
		Messenger.AddListener(EventDictionary.Instance.onGameOver(), OnGameOver);

	}


	void OnPlaying()
	{
		//level is playing so play the looping level music
		audio.clip = music_Level_1;
		audio.Play(0);

	}

	void OnLevelEnd()
	{
		audio.Stop();
	}

	void OnGameOver()
	{
		audio.Stop();
	}

	MusicStates musicState;

	// Use this for initialization
	void Start ()
	{
		musicState = MusicStates.Idle;
		_audioSource = GetComponent<AudioSource>();

		_audioSource.volume = 0.1f;
		_audioSource.loop = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		switch(musicState)
		{
		case MusicStates.StartMenu:
			break;
		case MusicStates.PlayingLevel:
			break;
		*/
	}



}
