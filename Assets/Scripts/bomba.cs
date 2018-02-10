using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomba : MonoBehaviour {

    public float x;
    public float y;
    public Animator anim;
    Rigidbody2D bomb;
    public float temp;

    // Use this for initialization
    void Start()
    {
        bomb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bomb.velocity = new Vector2(x * this.transform.localScale.x, y);
    }

    // Update is called once per frame
    void Update()
    {
       
            Destroy(this.gameObject,temp);
        

    }
    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("inimigo"))
        {

            anim.SetTrigger("explode");
        }
        if (col.tag.Equals("Piso"))
        {

            anim.SetTrigger("explode");
        }
    }
}