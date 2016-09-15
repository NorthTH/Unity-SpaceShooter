using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{
	public Transform playerShip;
	public int playerLives;
	public GameObject player;

	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	//	public GUIText restartText;
	public Text gameOverText;
	public Text livesText;
	public GameObject restartButton;

	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton; 

	private bool gameOver;
	private bool die;
	//private bool restart;
	private int score;
	private GameObject playerClone;

	void Start ()
	{
		playerClone = Instantiate (player, playerShip.position, Quaternion.identity) as GameObject;

		gameOver = false;
		die = false;
		//restart = false;
		//		restartText.text = "";
		gameOverText.text = "";
		restartButton.SetActive (false);
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update ()
	{
		if (playerClone.Equals(null) && playerLives <= 0) 
		{
			GameOver ();
		}

		if (die)
		{
			die = false;
		}
	}

	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			if (gameOver)
			{
				restartButton.SetActive (true);
				//				restartText.text = "Press 'R' for Restart";
				//restart = true;
				break;
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void playerDie ()
	{
		if (playerLives >= 1) {
			playerLives--;
			livesText.text = "X " + playerLives;
			playerClone = Instantiate (player, playerShip.position, Quaternion.identity) as GameObject;
		}
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	public void RestartGame()
	{
		SceneManager.LoadScene (1);
		//Application.LoadLevel (Application.loadedLevel);
	}
}