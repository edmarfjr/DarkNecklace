using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class facaArremeco : MonoBehaviour {
    public Vector2 velocidade;
    public bool tiro;
    Rigidbody2D faca;

    // Use this for initialization
    void Start () {
        faca = GetComponent<Rigidbody2D>();
        faca.velocity = velocidade * this.transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {
       
        Destroy(this, 2f);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("inimigo")|| col.tag.Equals("boss"))
        {
            if(!tiro)
            Destroy(gameObject);
        }

    }
}
