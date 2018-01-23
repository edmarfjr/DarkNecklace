using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisorDanoRato : MonoBehaviour {
    public GameObject PC;
    public GameObject rat;
    public bool xaman;
    // Use this for initialization
    void Start () {
        PC = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (!xaman)
        {
            Ratman scr = rat.gameObject.GetComponent<Ratman>();
            if (col.tag.Equals("ataque"))
            {
                Arma dano = col.gameObject.GetComponent<Arma>();



                Rigidbody2D rbd = rat.GetComponent<Rigidbody2D>();
                if (rbd.transform.position.x < col.transform.position.x)
                {

                    scr.encostouDir = true;
                }
                else
                {

                    scr.encostouDir = false;
                }
                scr.tomouDano(dano.dano, dano.knockback);
            }
            if (col.tag.Equals("itemArremeco"))
            {
                itemArremeco danoI = col.gameObject.GetComponent<itemArremeco>();



                Rigidbody2D rbd = rat.GetComponent<Rigidbody2D>();
                if (rbd.transform.position.x < col.transform.position.x)
                {

                    scr.encostouDir = true;
                }
                else
                {

                    scr.encostouDir = false;
                }
                scr.tomouDano(danoI.dano, danoI.knockback);
            }
        }
        if (xaman)
        {
            ratmanXaman scr = rat.gameObject.GetComponent<ratmanXaman>();
            if (col.tag.Equals("ataque"))
            {
                Arma dano = col.gameObject.GetComponent<Arma>();



                Rigidbody2D rbd = rat.GetComponent<Rigidbody2D>();
                if (rbd.transform.position.x < col.transform.position.x)
                {

                    scr.encostouDir = true;
                }
                else
                {

                    scr.encostouDir = false;
                }
                scr.tomouDano(dano.dano, dano.knockback);
            }
            if (col.tag.Equals("itemArremeco"))
            {
                itemArremeco danoI = col.gameObject.GetComponent<itemArremeco>();



                Rigidbody2D rbd = rat.GetComponent<Rigidbody2D>();
                if (rbd.transform.position.x < col.transform.position.x)
                {

                    scr.encostouDir = true;
                }
                else
                {

                    scr.encostouDir = false;
                }
                scr.tomouDano(danoI.dano, danoI.knockback);
            }
        }

    }
        
            
    }



