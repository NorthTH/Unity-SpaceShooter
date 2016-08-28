using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public float slowForce;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn; //public GameObject shotSpawn// shotSpawn.transsform.position
	public float fireRate;

	private float nextFire;

	private Vector2 touchBeginPos;
	private Vector2 touchMovedPos;
	private float dragXPos;
	private float dragYPos;

	void Start()
	{
		dragXPos = 0;
		dragYPos = 0;
	}

	void Update()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
				//GameObject clone =
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation); // as GameObject
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();
		}
	}

	void FixedUpdate()
	{
		//for PC Version
		/*float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = movement * speed;

		rb.position = new Vector3 
		(
				Mathf.Clamp(rb.position.x,boundary.xMin,boundary.xMax),
				0.0f,
				Mathf.Clamp(rb.position.z,boundary.zMin,boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
*/
		//for Mobile Version
		//If a touch is detected
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Began) {
				touchBeginPos = Input.GetTouch (0).position;
			}

			touchMovedPos = Input.GetTouch (0).position;

			dragXPos = (touchMovedPos.x - touchBeginPos.x) / Screen.height;
			dragYPos = (touchMovedPos.y - touchBeginPos.y) / Screen.width;

			Debug.Log (dragXPos);
		}

		Vector3 movement = new Vector3 (dragXPos, 0.0f, dragYPos);
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = movement * speed;

		rb.position = new Vector3 
			(
				Mathf.Clamp(rb.position.x,boundary.xMin,boundary.xMax),
				0.0f,
				Mathf.Clamp(rb.position.z,boundary.zMin,boundary.zMax)
			);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);

		if (dragXPos > -0.0001 && dragXPos < 0.0001)
			dragXPos = 0;
		else
			dragXPos -= (dragXPos) / slowForce;

		if (dragYPos > -0.0001 && dragYPos < 0.0001)
			dragYPos = 0;
		else
			dragYPos -= (dragYPos) / slowForce;
	}
}
