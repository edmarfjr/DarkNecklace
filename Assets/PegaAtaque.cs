using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegaAtaque : MonoBehaviour {
    public GameObject PC;
    public int dano;
    public float knockback;
	// Use this for initialization
	void Start () {
        GameObject aux = GameObject.FindWithTag("Player");
        PC = aux;
    }
	
	// Update is called once per frame
	void Update () {
        Arma scriPC = PC.GetComponent<Arma>();
        dano = scriPC.dano;
        knockback = scriPC.knockback;
    }
}
