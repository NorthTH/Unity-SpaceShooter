using UnityEngine;
using System.Collections;

public class MoveScript : MonoBehaviour {

	public float speed;
	public float tilt;
	public float slowForce;
	public Boundary boundary;

	private Vector2 touchBeginPos;
	private Vector2 touchMovedPos;
	private float dragXPos;
	private float dragYPos;

	void Start()
	{
		dragXPos = 0;
		dragYPos = 0;
	}

	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
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
