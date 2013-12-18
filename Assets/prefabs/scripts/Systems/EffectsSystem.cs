using UnityEngine;
using System.Collections;

public class EffectsSystem : MonoBehaviour
{


	public ParticleSystem effect_WolfKilled;

	void OnEnable()
	{

		Messenger.AddListener<Transform>(EventDictionary.Instance.onWolfKilled(), OnWolfKilled);

	}



	void OnWolfKilled(Transform temp_Transform)
	{
		Instantiate(effect_WolfKilled, temp_Transform.position, Quaternion.identity);
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

}
