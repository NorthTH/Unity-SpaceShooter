using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject exposion;
	public GameObject playerExposion;
	public int scoreValue;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) 
		{
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
		if (gameController == null) 
		{
			Debug.Log ("Cannot find 'GameController Script'");
		}
	}

	void OnTriggerEnter(Collider other) {
		//if (other.tag == "Boundary"||other.tag=="Enemy") 
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy")) 
		{
			return;
		}

		if (exposion != null) 
		{
			Instantiate (exposion, transform.position, transform.rotation);
		}

		//if (other.tag == "Player") 
		if (other.CompareTag ("Player"))
		{
			Instantiate (playerExposion, other.transform.position, other.transform.rotation);
			gameController.playerDie ();
			//gameController.GameOver ();
		}

		gameController.AddScore (scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}

}
