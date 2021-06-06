using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactive : MonoBehaviour
{
	private void Start()
	{
		PlayerController.onFirstShoot += FirstShoot;
	}

	public void FirstShoot()
	{
		gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		PlayerController.onFirstShoot -= FirstShoot;
	}
}
