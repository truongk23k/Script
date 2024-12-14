using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
	private static T _instance;
	public static T instance => _instance;
	public void Awake()
	{
		if (_instance == null)
		{
			_instance = this.GetComponent<T>();
			//DontDestroyOnLoad(this.GetComponent<T>());
		}
		else if (this.GetInstanceID() != instance.GetInstanceID())
		{
			Destroy(this.GetComponent<T>());
		}
	}
}
