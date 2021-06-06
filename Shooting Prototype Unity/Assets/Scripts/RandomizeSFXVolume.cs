using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeSFXVolume : MonoBehaviour
{
	private AudioSource sfx;
	
    void Start()
    {
		sfx = GetComponent<AudioSource>();

		sfx.volume = Random.Range(0.15f, 0.35f);
    }
}
