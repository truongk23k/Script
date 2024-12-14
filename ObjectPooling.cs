using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
	Dictionary<GameObject, List<GameObject>> _pools = new Dictionary<GameObject, List<GameObject>>();

	public GameObject GetObject(GameObject objKey)
	{
		if (!_pools.ContainsKey(objKey))
		{
			_pools.Add(objKey, new List<GameObject>());
		}

		foreach (GameObject obj in _pools[objKey])
		{
			if (!obj.activeSelf)
			{
				return obj;
			}
		}

		GameObject go = Instantiate(objKey, transform);
		_pools[objKey].Add(go);
		return go;
	}

	public void CreatePool(GameObject objKey, int size)
	{
		if (!_pools.ContainsKey(objKey))
		{
			_pools.Add(objKey, new List<GameObject>());
		}

		for (int i = 0; i < size; i++)
		{
			GameObject go = Instantiate(objKey, transform);
			go.SetActive(false);
			_pools[objKey].Add(go);
		}
	}
}
