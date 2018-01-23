using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {
    public int dano;
    public float knockback;
    public GameObject PC;
    // Use this for initialization
    void Start () {
        GameObject aux = GameObject.FindWithTag("Player");
        PC = aux;
    }
	
	// Update is called once per frame
	void Update () {
        cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
        if (scriPC.armaTipo == "espadaCurta")
        {
            dano = 2;
            knockback = 1f;
        }
        if (scriPC.armaTipo == "adaga")
        {
            dano = 1;
            knockback = 0.5f;
        }
        if (scriPC.armaTipo == "machado")
        {
            dano = 4;
            knockback = 1.3f;
        }
        if (scriPC.armaTipo == "espadao")
        {
            dano = 3;
            knockback = 1.3f;
        }
        if (scriPC.armaTipo == "lanca")
        {
            dano = 2;
            knockback = 1.2f;
        }
        if (scriPC.armaTipo == "rapier")
        {
            dano = 2;
            knockback = 0.9f;
        }

    }
}
