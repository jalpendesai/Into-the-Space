using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    public GameObject explosionPlayer;
    public GameObject explosion;
	public int scoreValue;

	private GameController gameController; // Reference to GameController SCRIPT

	void Start() 
	{
		// Reference to GameController GAMEOBJECT
		GameObject gameObjectGameController = GameObject.FindWithTag("GameController");

		if (gameObjectGameController != null) 
		{
			gameController = gameObjectGameController.GetComponent<GameController> ();
		}

		if (gameController == null) 
		{
			Debug.Log ("Cannot find Game Controller script on object");
		}
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary")
        {
            // Debug.Log("Destroybycontact");
            return;
        }

        if(other.tag == "Player")
        {
            Instantiate(explosionPlayer, other.gameObject.transform.position, other.gameObject.transform.rotation);
			gameController.GameOver ();
        }
			
        Instantiate(explosion, this.transform.position, this.transform.rotation);
		gameController.AddScore (scoreValue);

        Destroy(other.gameObject); // Destroying the "other" thing
        Destroy(this.gameObject); // Destroying this thing
    }
}
