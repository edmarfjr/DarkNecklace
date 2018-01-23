using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col_boss1 : MonoBehaviour {

    public GameObject PC;
    public GameObject bear;
    // Use this for initialization
    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        
            Bearman scr = bear.gameObject.GetComponent<Bearman>();
            if (col.tag.Equals("ataque"))
            {
                Arma dano = col.gameObject.GetComponent<Arma>();



                Rigidbody2D rbd = bear.GetComponent<Rigidbody2D>();
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



                Rigidbody2D rbd = bear.GetComponent<Rigidbody2D>();
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
