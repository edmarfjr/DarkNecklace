using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col_golemBoss : MonoBehaviour {
    public GameObject PC;
    public GameObject rbd;
    // Use this for initialization
    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
   
    void OnTriggerEnter2D(Collider2D col)
    {
        golemBoss scr = rbd.gameObject.GetComponent<golemBoss>();
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


