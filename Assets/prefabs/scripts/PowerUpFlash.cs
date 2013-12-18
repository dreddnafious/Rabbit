using UnityEngine;
using System.Collections;

public class PowerUpFlash : MonoBehaviour
{

	public float flashTime = 0.0f;
	private float flashTimer = 0.0f;
	private float flashMaterialTimer = 0.0f;
	private bool _flashed = false;
	private Color _defaultMaterialColor;
	private bool _flashedOnce = true;


	private enum flashingStates
	{
		idle,
		flashing,

	};



	flashingStates _flashState = flashingStates.idle;


	void OnEnable()
	{

		//Messenger.AddListener(EventDictionary.Instance.onCarrotEaten(), OnCarrotEaten);


	}

	void OnDestroy()
	{
		Messenger.RemoveListener(EventDictionary.Instance.onCarrotEaten(), OnCarrotEaten);
		Messenger.RemoveListener(EventDictionary.Instance.onPowerUpDone(), OnPowerUpDone);
	}


	void OnCarrotEaten()
	{
		//_flashState = flashingStates.flashing;
		flashTimer = 0.0f;
		StopCoroutine("SolidColorCountdown");
		StopCoroutine("FlashMaterial");
		ChangeMaterialToColor(Color.yellow);
		StartCoroutine("SolidColorCountdown");
	}

	void OnPowerUpDone()
	{
		StopCoroutine("SolidColorCountdown");
		StopCoroutine("FlashMaterial");
		ChangeMaterialToColor(_defaultMaterialColor);

	}


	void ChangeMaterialToColor(Color tempColor)
	{
		gameObject.renderer.material.color = tempColor;

	}

	// Use this for initialization
	void Start ()
	{
		Messenger.AddListener(EventDictionary.Instance.onCarrotEaten(), OnCarrotEaten);
		Messenger.AddListener(EventDictionary.Instance.onPowerUpDone(), OnPowerUpDone);

		_defaultMaterialColor = gameObject.renderer.material.color;
	}

	// Update is called once per frame
	void Update ()
	{

		flashMaterialTimer += Time.deltaTime;

	}



	IEnumerator SolidColorCountdown()
	{
		yield return new WaitForSeconds(2.5f);
		StartCoroutine("FlashMaterial");

	}

	IEnumerator FlashMaterial()
	{



		flashMaterialTimer = 0.0f;

		while(flashMaterialTimer < 2.5f)
		{
			if(!_flashed)
			{
				gameObject.renderer.material.color = _defaultMaterialColor;
				_flashed = !_flashed;
			}
			else
			{
				ChangeMaterialToColor(Color.yellow);
				_flashed = !_flashed;
			}

			yield return new WaitForSeconds(0.1f);

		}

		flashMaterialTimer = 0.0f;
		gameObject.renderer.material.color = _defaultMaterialColor;

	}

}
