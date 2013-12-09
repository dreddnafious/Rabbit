/// <summary>
/// Wolf random AI.
/// Currently a mess.
/// Need to consider A* or some alternative method 
/// Raytracing just isnt working out on the corners specifically.
/// </summary>


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class wolfRandomAI : MonoBehaviour
{
	Rigidbody myRigidBody;
	Transform myTransform;
	Vector3 _moveVector;

	float _timePassed = 0f;
	float _movementSpeed = 50.0f;
	float _rayOffset = 1.5f;
	float _debugRayOffset = 1.5f;


	bool newChoices = false;
	bool canMoveUp = false;
	bool canMoveDown = false;
	bool canMoveLeft = false;
	bool canMoveRight = false;

	bool couldMoveUp = false;
	bool couldMoveDown = false;
	bool couldMoveLeft = false;
	bool couldMoveRight = false;

	List<Directions> wolfDirectionList;

	enum Directions
	{
		None,
		Up,
		Down,
		Left,
		Right,

	}

	private Directions WolfDirection;
	private Directions LastWolfDirection;


	enum States
	{
		Idle,
		Travel,
		Choose,
	}

	private States AIState;

	// Use this for initialization
	void Start ()
	{
		myRigidBody = rigidbody;
		myTransform = transform;
		_moveVector = new Vector3(0.0f, 0.0f, 0.0f);
		AIState = States.Travel;
		WolfDirection = Directions.None;
		wolfDirectionList = new List<Directions>();
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		_timePassed = Time.deltaTime * _movementSpeed;

		switch(AIState)
		{
		case States.Idle:
			break;
		case States.Travel:
			Travel();
			break;
		case States.Choose:
			Choose();
			break;
		}
	
	}

	void FixedUpdate()
	{
		
		//myRigidBody.velocity = Vector3.zero;
		
		
	}


	void Travel()
	{

		couldMoveUp = canMoveUp;
		couldMoveDown = canMoveDown;
		couldMoveLeft = canMoveLeft;
		couldMoveRight = canMoveRight;

		//Debug.Log("In Travel Mode");

		if(Physics.Raycast(myTransform.position, Vector3.forward,  _rayOffset))
		{
			//Debug.Log("length of ray= " + _rayOffset);
			Debug.DrawRay(myTransform.position, Vector3.forward * _debugRayOffset);
			//if this is true then we cant go this direction(UP).
			canMoveUp =false;
		}
		else
		{
			canMoveUp = true;
		}

		
		if(Physics.Raycast(myTransform.position, Vector3.back, _rayOffset))
		{
			Debug.DrawRay(myTransform.position, Vector3.back *  _debugRayOffset);
			//If this is true then we can't go this direction(DOWN)
			canMoveDown = false;		
		}
		else
		{
			canMoveDown = true;
		}


		
		if(Physics.Raycast(myTransform.position, Vector3.left, _rayOffset))
		{
			Debug.DrawRay(myTransform.position, Vector3.left *  _debugRayOffset);
			//if this is true then we can't go this direction(LEFT)
			canMoveLeft = false;
		}
		else
		{
			canMoveLeft = true;
		}


		if(Physics.Raycast(myTransform.position, Vector3.right, _rayOffset))
		{
			Debug.DrawRay(myTransform.position, Vector3.right *  _debugRayOffset);
			//if this is true then we can't go this direction(RIGHT)
			canMoveRight = false;			
		}
		else
		{
			canMoveRight = true;
		}


		if (canMoveUp != couldMoveUp)
		{	//Debug.Log("New Can Move Up");
			AIState = States.Choose;
			wolfDirectionList.Add(Directions.Up);
		}

		if(canMoveDown != couldMoveDown)
		{	//Debug.Log("New Can Move Down");
			AIState = States.Choose;
			wolfDirectionList.Add(Directions.Down);
		}

		if(canMoveLeft != couldMoveLeft)
		{	//Debug.Log("New Can Move Left");
			AIState = States.Choose;
			wolfDirectionList.Add(Directions.Left);
		}
		if(canMoveRight != couldMoveRight)
		{	//Debug.Log("New Can Move Right");
			AIState = States.Choose;
			wolfDirectionList.Add(Directions.Right);
		}


		switch(WolfDirection)
		{
		case Directions.Up:
			_moveVector.z = 1f * _timePassed;
			break;
		case Directions.Down:
			_moveVector.z = -1f * _timePassed;
			break;
		case Directions.Left:
			_moveVector.x = -1f * _timePassed;
			break;
		case Directions.Right:
			_moveVector.x = 1f * _timePassed;
			break;
		}

		//myTransform.Translate(_moveVector);
		myRigidBody.velocity = _moveVector;


	}

	void Choose()
	{
		LastWolfDirection = WolfDirection;
		Debug.Log("In choose state");
		int choice;
		choice = Random.Range(0, wolfDirectionList.Count);
		WolfDirection = wolfDirectionList[choice];
		/*
		if(WolfDirection == LastWolfDirection)
		{
			wolfDirectionList.Remove(WolfDirection);
			Random.Range(0, wolfDirectionList.Count);
		}
		*/
		Debug.Log("Number of choices = " + choice);
		Debug.Log("Chose " + WolfDirection);
		wolfDirectionList.Clear();

		AIState = States.Travel;

	}



}
