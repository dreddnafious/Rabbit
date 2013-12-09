using UnityEngine;
using System.Collections;

public class PelletArray : MonoBehaviour
{

	private Transform[] _allPellets;
	private Transform[] _currentPellets;


	private enum MapStates
	{
		Idle,
		Playing,
	};

	MapStates mapState;

	void OnEnable()
	{

		Messenger.AddListener(EventDictionary.Instance.onGameReset(), OnReset);

	}

	void OnReset()
	{
		//_allPellets = gameObject.GetComponentsInChildren<Transform>();
		Debug.Log("inside OnReset in pelletarray and _allpellets count is " + _allPellets.Length.ToString());

		for(int i = 0; i < _allPellets.Length; i++)
		{

			_allPellets[i].gameObject.SetActive(true);
			mapState = MapStates.Playing;

		}

	}

	// Use this for initialization
	void Start ()
	{

		_allPellets = gameObject.GetComponentsInChildren<Transform>();
		mapState = MapStates.Playing;
	}
	
	// Update is called once per frame
	void Update ()
	{

		switch(mapState)
		{
		case MapStates.Playing:
			Playing();
			break;

		}


	
	}

	void Playing()
	{
		_currentPellets = gameObject.GetComponentsInChildren<Transform>();
		
		if(_currentPellets.Length <= 1)
		{
			Debug.Log("Winner!");
			
			mapState = MapStates.Idle;
		}


	}

}
