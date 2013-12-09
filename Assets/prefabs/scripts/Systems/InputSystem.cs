using UnityEngine;
using System.Collections;

public class InputSystem : MonoBehaviour
{

	private enum States
	{
		Idle,
		Playing,
	};

	private States InputStates;


	// Use this for initialization
	void Start ()
	{
		InputStates = States.Idle;
	}

	void OnEnable()
	{
		Messenger.AddListener(EventDictionary.Instance.onPlaying(), OnPlaying);
		Messenger.AddListener(EventDictionary.Instance.onGameOver(), OnGameOver);

	}

	void OnPlaying()
	{
		InputStates = States.Playing;


	}

	void OnGameOver()
	{
		InputStates = States.Idle;

	}

	// Update is called once per frame
	void Update ()
	{

		switch(InputStates)
		{

		case States.Playing:
			Playing();
			break;


		}

		
	}


		void Playing()
		{
			if(Input.GetKey(KeyCode.UpArrow))
			{
				
			Messenger.Broadcast(EventDictionary.Instance.onMoveUp());
				
			}
			
			if(Input.GetKey(KeyCode.DownArrow))
			{
				
			Messenger.Broadcast(EventDictionary.Instance.onMoveDown());
				
			}
			
			if(Input.GetKey(KeyCode.LeftArrow))
			{
				
			Messenger.Broadcast(EventDictionary.Instance.onMoveLeft());
				
			}
			
			if(Input.GetKey(KeyCode.RightArrow))
			{
				
			Messenger.Broadcast(EventDictionary.Instance.onMoveRight());
				
			}


		}

	


}
