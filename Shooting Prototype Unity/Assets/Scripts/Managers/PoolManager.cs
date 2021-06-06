using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
	private static PoolManager _instance;
	public static PoolManager Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("PoolManager is NULL");

			return _instance;
		}
	}
	
	public GameObject _bulletContainer;
	public GameObject _bulletPrefab;

	public List<GameObject> _bulletPool;

	private void Awake()
	{
		_instance = this;
	}

	private void Start()
	{
		_bulletPool = GenerateBullets(30);
	}

	List<GameObject> GenerateBullets(int amountOfBullets)
	{
		for (int i = 0; i < amountOfBullets; i++)
		{
			GameObject bullet = Instantiate(_bulletPrefab);          // creating the bullet
			bullet.transform.parent = _bulletContainer.transform;    // assigning a parent
			_bulletPool.Add(bullet);
			bullet.SetActive(false);
		}
		return _bulletPool;
	}

	public GameObject RequestBullet()
	{
		foreach (GameObject b in _bulletPool)
		{
			if (!b.activeInHierarchy)
			{
				b.SetActive(true);
				return b;
			}
		}

		_bulletPool = GenerateBullets(1);
		return RequestBullet();
	}
}
