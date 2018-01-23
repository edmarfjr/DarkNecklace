using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vaiporra : MonoBehaviour {
    public float vel;
    public Rigidbody2D rbd;
    // Use this for initialization
    void Start () {
        rbd = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        rbd.velocity = new Vector2(vel, rbd.velocity.y);
    }
}
