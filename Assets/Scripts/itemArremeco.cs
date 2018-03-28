using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemArremeco : MonoBehaviour {

    public int dano;
    public float knockback;
    public GameObject PC;
    // Use this for initialization
    void Start()
    {
        GameObject aux = GameObject.FindWithTag("Player");
        PC = aux;
    }

    // Update is called once per frame
    void Update () {
        cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
        if (scriPC.itemTipo == "facaArremeco")
        {
            dano = 2;
            knockback = 0.2f;
        }
        if (scriPC.itemTipo == "pistola")
        {
            dano = 2;
            knockback = 0.8f;
        }
        if (scriPC.itemTipo == "rifle")
        {
            dano = 2;
            knockback = 1.2f;
        }
        if (scriPC.itemTipo == "bombaIncend")
        {
            dano = 1;
            knockback = 0.4f;
        }
    }
}
