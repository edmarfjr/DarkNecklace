using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBearman : MonoBehaviour {
    public Rigidbody2D rbd;
    public GameObject PC;
    public Animator anim;
    public float vel;
    public bool agro;
    public bool atacando;
    public bool alcAtaq;
	// Use this for initialization
	void Start () {
        rbd = GetComponent<Rigidbody2D>();
        PC = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        agrar();
        andar();
	}
    void andar()
    {

        if (agro == true && !atacando && !alcAtaq)
        {
            anim.SetBool("movendo", true);
            if (rbd.transform.position.x > PC.transform.position.x)
            {
                rbd.velocity = new Vector2(-vel, rbd.velocity.y);
            }
            else
            {
                rbd.velocity = new Vector2(vel, rbd.velocity.y);
            }
        }
        else
            anim.SetBool("movendo", false);
    }
    void agrar()
    {
        if ((Vector2.Distance(rbd.transform.position, PC.transform.position) < 20) && (alcAtaq == false))
            agro = true;
        else
            agro = false;
    }
}
