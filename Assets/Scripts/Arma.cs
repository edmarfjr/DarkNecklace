using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arma : MonoBehaviour {
    public int dano;
    public int bonusAtaq;
    public float knockback;
    public GameObject PC;
    // Use this for initialization
    void Start () {
        GameObject aux = GameObject.FindWithTag("Player");
        PC = aux;
    }
	
	// Update is called once per frame
	void Update () {
        dano=DanoArma();
        knockback = armaKb();
    }
    public int DanoArma()
    {
        cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
        bonusAtaq = scriPC.bonusAtaque;
        
        if (scriPC.armaTipo == "espadaCurta")
        {
            dano = bonusAtaq + 2;
            return dano;
        }
        if (scriPC.armaTipo == "adaga")
        {
            dano = bonusAtaq + 1;
            return dano;
        }
        if (scriPC.armaTipo == "machado")
        {
            dano = bonusAtaq + 4;
            return dano;
        }
        if (scriPC.armaTipo == "espadao")
        {
            dano = bonusAtaq + 3;
            return dano;
        }
        if (scriPC.armaTipo == "lanca")
        {
            dano = bonusAtaq + 2;
            return dano;
        }
        if (scriPC.armaTipo == "rapier")
        {
            dano = bonusAtaq + 2;
            return dano;
        }
        if (scriPC.itemTipo == "facaArremeco")
        {
            dano = 2;
            return dano;
        }
        if (scriPC.itemTipo == "pistola")
        {
            dano = 2;
            return dano;
        }
        if (scriPC.itemTipo == "bombaIncend")
        {
            dano = 1;
            return dano;
        }
        else { return 0; }
    }
    public float armaKb()
    {
        cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
        bonusAtaq = scriPC.bonusAtaque;

        if (scriPC.armaTipo == "espadaCurta")
        {
            return  1f;
        }
        if (scriPC.armaTipo == "adaga")
        {
            return  0.5f;
        }
        if (scriPC.armaTipo == "machado")
        {
            return 1.3f;
        }
        if (scriPC.armaTipo == "espadao")
        {
            return 1.3f;
        }
        if (scriPC.armaTipo == "lanca")
        {
            return 1.2f;
        }
        if (scriPC.armaTipo == "rapier")
        {
            return 0.9f;
        }
        if (scriPC.itemTipo == "facaArremeco")
        {
            return 0.2f;
        }
        if (scriPC.itemTipo == "pistola")
        {
            return 0.5f;
        }
        if (scriPC.itemTipo == "bombaIncend")
        {
            return 0.5f;
        }
        else { return 0; }
    }
}
