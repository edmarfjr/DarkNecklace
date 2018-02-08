using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombaInimigo : MonoBehaviour {

    private float x;
    private float y;
    public Animator anim;
    Rigidbody2D bomb;
    public float temp;
    public Transform PC;
    // Use this for initialization
    void Start()
    {
        GameObject aux = GameObject.FindGameObjectWithTag("Player");
        PC = aux.transform;
        bomb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); x = 5;
        y = 5;
        if((Vector2.Distance(this.gameObject.transform.position, PC.transform.position) < 6))
            { y=3; x = 4; }
        bomb.velocity = new Vector2(x * this.transform.localScale.x, y);
    }

    // Update is called once per frame
    void Update()
    {

        Destroy(this.gameObject, temp);


    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {

            anim.SetTrigger("explode");
            Destroy(this.gameObject,0.3f);
        }
        if (col.tag.Equals("Piso"))
        {

            anim.SetTrigger("explode");
        }
    }
}
