using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject hazard;   // Spawned enemies
    public Vector2 spawnValue;  // Where do enemies spawn?
    public int hazardCount; // How many enemies?
    public float startWait; // How long do we wait for the 1st wave
    public float spawnWait; // How long between each asteroid spawn?
    public float waveWait; // How long between waves of enemies

	public Text scoreText;
	public Text restartText;
	public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

	// Use this for initialization
	void Start () {
        gameOver = false;
        restart = false;

		scoreText.text = "";
		restartText.text = "";
		gameOverText.text = "";

        score = 0;
		UpdateScore ();
        StartCoroutine(SpawnWaves()); // Starts and calls my coroutine
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (restart) 
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
				// Application.LoadLevel (Application.loadedLevel); -- DECPRICATED
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit ();
		}
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for(int i = 0; i < hazardCount; i++)
            {
                Vector2 spawnPosition = new Vector2(spawnValue.x, Random.Range(-spawnValue.y, spawnValue.y));
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
				restartText.gameObject.SetActive (true);
				restartText.text = "Press R for Restart";
                restart = true;
                break;
            }
        }
    }

	public void AddScore(int newScoreValue) 
	{
		score += newScoreValue;
		// score = score + newScoreValue;
		UpdateScore();
	}

    void UpdateScore()
    {
        // Text for the score will go here!
		scoreText.text = "Score: " + score;
    }

	public void GameOver() 
	{
		gameOverText.gameObject.SetActive (true);
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}
