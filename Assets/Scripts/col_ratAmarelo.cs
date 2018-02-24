using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col_ratAmarelo : MonoBehaviour {
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
        RatoAmareloBoss scr = rbd.gameObject.GetComponent<RatoAmareloBoss>();
        if (col.tag.Equals("ataque"))
        {
            PegaAtaque dano = col.gameObject.GetComponent<PegaAtaque>();
            StartCoroutine(scr.tomouDano(dano.dano, dano.knockback));
        }
        if (col.tag.Equals("itemArremeco"))
        {
            itemArremeco danoI = col.gameObject.GetComponent<itemArremeco>();
            StartCoroutine(scr.tomouDano(danoI.dano, danoI.knockback));
        }
    }
}

