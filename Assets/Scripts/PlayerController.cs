using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    // public float xMin, xMax, yMin, yMax;
    public Boundary boundary;
    public GameObject laser;
    public Transform laserSpawn;

    private Rigidbody2D rBody;
    private float nextFire = 0.25f;
    private float myTime = 0.0f;


	// Use this for initialization
	void Start () {
        rBody = this.GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
        myTime += Time.deltaTime;

		if(Input.GetButton("Fire1") && myTime > nextFire)
        {
            Instantiate(laser, laserSpawn.position, laserSpawn.rotation);

            myTime = 0.0f;
        }
	}

    // Like Update but used with physics. 
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Debug.Log("H=" + moveHorizontal + " V=" + moveVertical);

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Debug.Log(movement);

        rBody.velocity = movement * speed;

        // rBody.position = new Vector2(Mathf.Clamp(rBody.position.x, -8.0f, 4.0f), Mathf.Clamp(rBody.position.y, -4.0f, 4.0f));
        // rBody.position = new Vector2(Mathf.Clamp(rBody.position.x, xMin, xMax), Mathf.Clamp(rBody.position.y, yMin, yMax));
        rBody.position = new Vector2(Mathf.Clamp(rBody.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(rBody.position.y, boundary.yMin, boundary.yMax));
    }
}
