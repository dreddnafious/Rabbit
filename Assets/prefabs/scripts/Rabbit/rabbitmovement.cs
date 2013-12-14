using UnityEngine;
using System.Collections;

public class rabbitmovement : MonoBehaviour
{
	Transform myTransform;
	GameObject _respawn;
	Rigidbody myRigidBody;

	float _movementSpeed = 3.5f;
	float _poweredUpMovementSpeed = 5.25f;
	float _timePassed = 0.0f;

	Vector3 _moveVector;
	Vector3 _lastPositionVector;
	float   _deltaMovement = .01f;

	bool horizontalInput = false;
	bool verticalInput =   false;

	//this is for the raycasting
	float _rayOffset = 0.5f;
	float _diagonalRayOffset = 0.9f;
	float _distanceFromCenter = 0.35f;

	//Vector3 diagonalUpLeft;
	//Vector3 diagonalUpRight;
	//Vector3 diagonalDownLeft;
	//Vector3 diagonalDownRight;


	private enum RabbitStates
	{
		Normal,
		PoweredUp,

	};

	RabbitStates _rabbitState = RabbitStates.Normal;

	// Use this for initialization
	void Start ()
	{
		myTransform = transform;
		myRigidBody = rigidbody;
		_moveVector = new Vector3(0.0f, 0.0f, 0.0f);
		_lastPositionVector = new Vector3(0.0f, 0.0f, 0.0f);
		_respawn = GameObject.Find("PlayerSpawn");


		/*
		diagonalUpLeft = new Vector3(-1.0f, 0.0f, 1.0f);
		diagonalUpRight = new Vector3(1.0f, 0.0f, 1.0f);
		diagonalDownLeft = new Vector3(-1.0f, 0.0f, -1.0f);
		diagonalDownRight = new Vector3(1.0f, 0.0f, -1.0f);
		*/
	}

	void FixedUpdate()
	{

		myRigidBody.velocity = Vector3.zero;


	}

	void OnEnable()
	{
		//add your listeners here.
		Messenger.AddListener(EventDictionary.Instance.onMoveUp(), OnMoveUp);	
		Messenger.AddListener(EventDictionary.Instance.onMoveDown(), OnMoveDown);
		Messenger.AddListener(EventDictionary.Instance.onMoveLeft(), OnMoveLeft);
		Messenger.AddListener(EventDictionary.Instance.onMoveRight(), OnMoveRight);


		Messenger.AddListener(EventDictionary.Instance.onPlayerKilled(), OnPlayerKilled);
		Messenger.AddListener(EventDictionary.Instance.onGameReset(), OnGameReset);
		Messenger.AddListener(EventDictionary.Instance.onLevelReset(), OnLevelReset);
		Messenger.AddListener(EventDictionary.Instance.onLevelEnding(), OnLevelEnding);


		Messenger.AddListener(EventDictionary.Instance.onCarrotEaten(), OnCarrotEaten);
		Messenger.AddListener(EventDictionary.Instance.onPowerUpDone(), OnPowerUpDone);

	}


	void OnCarrotEaten()
	{
		_rabbitState = RabbitStates.PoweredUp;

	}

	void OnPowerUpDone()
	{
		_rabbitState = RabbitStates.Normal;
	}

	void OnMoveUp()
	{
		//Debug.Log("UP Called");
		_moveVector.z = 1f * _timePassed;
		verticalInput = true;
	}

	void OnMoveDown()
	{
		//Debug.Log("Down Called");
		_moveVector.z = -1f * _timePassed;
		verticalInput = true;
	}

	void OnMoveLeft()
	{
		//Debug.Log("Left Called");
		_moveVector.x = -1f * _timePassed;
		horizontalInput = true;
	}

	void OnMoveRight()
	{
		//Debug.Log("Right Called");
		_moveVector.x = 1f * _timePassed;
		horizontalInput = true;
	}

	/// <summary>
	/// Raises the player killed event. - Uses the "PlayerRespawn" Gameobject captured
	/// in the start function to move the player back to the starting point.
	/// </summary>
	void OnPlayerKilled()
	{

		myTransform.position = _respawn.transform.position;

	}

	void OnGameReset()
	{
		myTransform.position = _respawn.transform.position;

	}

	void OnLevelReset()
	{
		myTransform.position = _respawn.transform.position;
	}

	void OnLevelEnding()
	{
		_moveVector.x = 0.0f;
		_moveVector.y = 0.0f;

	}
	
	// Update is called once per frame
	void Update ()
	{

		switch(_rabbitState)
		{
		case RabbitStates.Normal:
			NormalSpeed();
			break;
		case RabbitStates.PoweredUp:
			PoweredUpSpeed();
			break;
		}

		//_timePassed = Time.deltaTime * _movementSpeed;


		DontBounceOffWalls();
	


		_lastPositionVector = myTransform.position;
		myTransform.Translate(_moveVector);
		verticalInput = false;
		horizontalInput = false;


	}


	void NormalSpeed()
	{
		//Debug.Log("normal speed called");
		_timePassed = Time.deltaTime * _movementSpeed;
	}

	void PoweredUpSpeed()
	{
		_timePassed = Time.deltaTime * _poweredUpMovementSpeed;
	}

	//because you run in the last direction pressed
	//this stops your movement in that direction when you hit a wall
	void DontBounceOffWalls()
	{
		if(Mathf.Abs((myRigidBody.position.x - _lastPositionVector.x)) < _deltaMovement &&
		   horizontalInput == false)
		{//we're bouncing off a wall in this direction without input
			
			_moveVector.x = 0.0f;
			
		}
		
		if(Mathf.Abs((myRigidBody.position.z - _lastPositionVector.z)) < _deltaMovement &&
		   verticalInput == false)
		{//we're bouncing off a wall in this direction without input
			
			_moveVector.z = 0.0f;
			
		}


	}


	/// <summary>
	/// RayCasting.
	/// This throws out rays in 6 directions and attempts to limit
	/// player movement based on what doesn't collide
	/// doesn't work for shit though.
	/// Switched back to a physics solution.
	/// </summary>
	void RayCasting()
	{
		/*
		RaycastHit rayHit;

		if(Physics.Raycast(myTransform.position, Vector3.forward, out rayHit, (_timePassed + _rayOffset)))
		{
			Debug.DrawRay(myTransform.position, Vector3.forward * (_timePassed + 0.9f));
			
			
			
			if(_moveVector.z > 0f)
				_moveVector.z = 0f;//1f * _timePassed;
			
			if(rayHit.distance < _distanceFromCenter)
				_moveVector.z += rayHit.distance;
			
		}
		
		if(Physics.Raycast(myTransform.position, Vector3.back, out rayHit, _timePassed + _rayOffset))
		{
			Debug.DrawRay(myTransform.position, Vector3.back * (_timePassed + 0.9f));
			if(_moveVector.z < 0f)
				_moveVector.z = 0f;//1f * _timePassed;
			
			if(rayHit.distance < _distanceFromCenter)
				_moveVector.z -= rayHit.distance;
			
		}
		
		if(Physics.Raycast(myTransform.position, Vector3.left, out rayHit, _timePassed + _rayOffset))
		{
			Debug.DrawRay(myTransform.position, Vector3.left * (_timePassed + 0.9f));
			if(_moveVector.x < 0f)
				_moveVector.x = 0f;//1f * _timePassed;
			
			if(rayHit.distance < _distanceFromCenter)
				_moveVector.x -= rayHit.distance;
			
		}
		
		if(Physics.Raycast(myTransform.position, Vector3.right, out rayHit, _timePassed + _rayOffset))
		{
			Debug.DrawRay(myTransform.position, Vector3.right * (_timePassed + 0.9f));
			if(_moveVector.x > 0f)
				_moveVector.x = 0f;//1f * _timePassed;
			
			if(rayHit.distance < _distanceFromCenter)
				_moveVector.x += rayHit.distance;
			
		}
		
		
		//diagonal up ray
		if(Physics.Raycast(myTransform.position, diagonalUpLeft, out rayHit, (_timePassed + _diagonalRayOffset)))
		{
			Debug.DrawRay(myTransform.position, diagonalUpLeft * (_timePassed + 1.0f), Color.red);
			
			if(rayHit.distance < _distanceFromCenter)
			{
				_moveVector.z -= _distanceFromCenter * Time.deltaTime;
				_moveVector.x += _distanceFromCenter * Time.deltaTime;
				Debug.Log("moveVec Z " + _moveVector.z);
				Debug.Log("moveVec X " +_moveVector.x);
				
			}
			
			/*
			if(_moveVector.z > 0f) 
			{
				_moveVector.z = 0f;

			}

			if(_moveVector.x < 0f)
			{
				_moveVector.x = 0f;

			}
			*/
	/*	}
		
		if(Physics.Raycast(myTransform.position, diagonalUpRight, out rayHit, (_timePassed + _diagonalRayOffset)))
		{
			Debug.DrawRay(myTransform.position, diagonalUpRight * (_timePassed + 1.0f), Color.magenta);
			
			if(rayHit.distance < _distanceFromCenter)
			{
				_moveVector.z -= rayHit.distance;
				_moveVector.x -= rayHit.distance;
			}
			
			if(_moveVector.z > 0f && _moveVector.x > 0f)
			{
				_moveVector.z = 0f;
				_moveVector.x = 0f;
			}
		}
		
		if(Physics.Raycast(myTransform.position, diagonalDownLeft, out rayHit, (_timePassed + _diagonalRayOffset)))
		{
			Debug.DrawRay(myTransform.position, diagonalDownLeft * (_timePassed + 1.0f), Color.blue);
			
			if(rayHit.distance < _distanceFromCenter)
			{
				_moveVector.z += rayHit.distance;
				_moveVector.x += rayHit.distance;
			}
			
			if(_moveVector.z < 0f && _moveVector.x < 0f)
			{
				_moveVector.z = 0f;
				_moveVector.x = 0f;
			}
		}
		
		if(Physics.Raycast(myTransform.position, diagonalDownRight, out rayHit, (_timePassed + _diagonalRayOffset)))
		{
			Debug.DrawRay(myTransform.position, diagonalDownRight * (_timePassed + 1.0f), Color.yellow);
			
			if(rayHit.distance < _distanceFromCenter)
			{
				_moveVector.z += rayHit.distance;
				_moveVector.x -= rayHit.distance;
			}
			
			
			if(_moveVector.z < 0f && _moveVector.x > 0f)
			{
				_moveVector.z = 0f;
				_moveVector.x = 0f;
			}

		}
	*/
	}

	void OnTriggerEnter(Collider other)
	{
		

		/*
		if(other.gameObject.tag == "wolf")
		{
			Debug.Log("Carnage!");
			//Destroy(gameObject);
		}
		*/
	}

}
