﻿using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float diversionRate;
	public float delay;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		InvokeRepeating ("Fire", delay, Random.Range (fireRate - diversionRate, fireRate + diversionRate));
	}

	void Fire()
	{
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
	}
}
