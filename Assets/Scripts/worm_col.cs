using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worm_col : MonoBehaviour {

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

        worm scr = rbd.gameObject.GetComponent<worm>();
        if (col.tag.Equals("ataque"))
        {
            PegaAtaque dano = col.gameObject.GetComponent<PegaAtaque>();

            scr.tomouDano(dano.dano, dano.knockback);
        }
        if (col.tag.Equals("itemArremeco"))
        {
            itemArremeco danoI = col.gameObject.GetComponent<itemArremeco>();
            scr.tomouDano(danoI.dano, danoI.knockback);
        }



    }


}
