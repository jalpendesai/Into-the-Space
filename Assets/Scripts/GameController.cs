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
    public Text livesText;

    private bool startGame = false;
    private bool gameOver;
    private bool restart;
    private float startTime;
    //private int score;
    //private int lives;
    private bool reloadScene = false;

	// Use this for initialization
	void Start () {
        gameOver = false;
        restart = false;

		scoreText.text = "";
		restartText.text = "";
		gameOverText.text = "";
        livesText.text = "";

        //score = 0;
        //lives = 4;
		UpdateScore ();
        UpdateLives();
        StartCoroutine(SpawnWaves()); // Starts and calls my coroutine
    
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (gameOver)
        {
            Restart();
        }
        else
        {
            if (reloadScene)
            {
                StartCoroutine(ReloadScene(3));
                reloadScene = false;
            }
        }


		if (restart) 
		{
			if (Input.GetKeyDown (KeyCode.R)) 
			{
                // Application.LoadLevel (Application.loadedLevel); -- DECPRICATED
                PlayerData.score = 0;
                PlayerData.lives = 4;
                restart = false;
				StartCoroutine(ReloadScene(3));
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape)) 
		{
			Application.Quit ();
		}
	}

    void FixedUpdate()
    {
        //if(!gameOver && startGame)
        //{

        //}
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

    IEnumerator ReloadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        startGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        startTime = Time.time;
    }

    public void Restart()
    {
        restart = true;
    }

    public void AddScore(int newScoreValue) 
	{
        PlayerData.score += newScoreValue;
		// score = score + newScoreValue;
		UpdateScore();
	}

    public void ReduceLives()
    {
        PlayerData.lives --;
        reloadScene = true;
        UpdateLives();

        //if (lives == 0)
        //{
        //    GameOver();
        //}
    }

    public bool LivesLeft()
    {
        return PlayerData.lives > 0;
    }

    void UpdateScore()
    {
        // Text for the score will go here!
		scoreText.text = "Score: " + PlayerData.score;
    }

    void UpdateLives()
    {
        livesText.text = "Lives: " + PlayerData.lives;
    }

	public void GameOver() 
	{
		gameOverText.gameObject.SetActive (true);
		gameOverText.text = "Game Over!";
		gameOver = true;
	}
}
