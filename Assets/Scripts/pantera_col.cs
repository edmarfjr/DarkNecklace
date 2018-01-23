using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pantera_col : MonoBehaviour {

    public GameObject PC;
    public GameObject rbd;
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
        
            Pantera scr = rbd.gameObject.GetComponent<Pantera>();
            if (col.tag.Equals("ataque"))
            {
                Arma dano = col.gameObject.GetComponent<Arma>();



                Rigidbody2D ribd = rbd.GetComponent<Rigidbody2D>();
                if (ribd.transform.position.x < col.transform.position.x)
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



                Rigidbody2D ribd = rbd.GetComponent<Rigidbody2D>();
                if (ribd.transform.position.x < col.transform.position.x)
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




