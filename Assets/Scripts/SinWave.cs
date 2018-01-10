using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SinWave : MonoBehaviour {

    public float xSpeed;
    public float movementAmplitude;
    public float angularFrequency;
    public Boundary boundary;

    private Rigidbody2D rBody;
    private float startTime;

    // Use this for initialization
    void Start () {
        rBody = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        rBody.velocity = new Vector2(-xSpeed, movementAmplitude * Mathf.Sin(angularFrequency * (Time.time - startTime)));
        rBody.position = new Vector2(
            Mathf.Clamp(rBody.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rBody.position.y, boundary.yMin, boundary.yMax)
        );
    }
}
