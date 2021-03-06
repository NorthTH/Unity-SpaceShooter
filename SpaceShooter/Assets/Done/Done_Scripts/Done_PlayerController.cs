﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform[] shotSpawns;
	public float fireRate;
	public AudioSource m_ShootingAudio;   

	private SimpleTouchPad touchPad;
	private SimpleTouchAreaButton areaButton;
	 
	private float nextFire;
	private Quaternion calibrationQuaternion;

	void Start()
	{
		CalibrateAccelleromter ();
		touchPad = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().touchPad;
		areaButton = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ().areaButton;
	}
	
	void Update ()
	{
		if (areaButton.CanFire() && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			foreach (var shotSpawn in shotSpawns) {
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			}
			m_ShootingAudio.Play ();
		}
	}
		
	void CalibrateAccelleromter()
	{
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAccelleration(Vector3 acceleration)
	{
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

	void FixedUpdate ()
	{
//		float moveHorizontal = Input.GetAxis ("Horizontal");
//		float moveVertical = Input.GetAxis ("Vertical");

//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		//Vector3 accelerationRaw = Input.acceleration;
		//Vector3 acceleration = FixAccelleration (accelerationRaw);
		//Vector3 movement = new Vector3 (acceleration.x, 0.0f, acceleration.y);

		Vector2 direction = touchPad.GetDirection ();
		Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
