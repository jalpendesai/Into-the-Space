using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed = 20f;

    private Rigidbody2D rBody;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody2D>();
        rBody.velocity = this.transform.right * speed;
	}
}
