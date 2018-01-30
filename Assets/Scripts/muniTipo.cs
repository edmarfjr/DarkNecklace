using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muniTipo : MonoBehaviour {
    public GameObject PC;
    private Animator anim;
    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
        GameObject aux = GameObject.FindWithTag("Player");
        PC = aux;
    }
	
	// Update is called once per frame
	void Update () {
        cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
        if (scriPC.itemTipo == "facaArremeco")
        {
            anim.SetTrigger("facaArremeco");
        }
        if (scriPC.itemTipo == "lancaMagi")
        {
            anim.SetTrigger("lancaMagi");
        }
        if (scriPC.itemTipo == "pistola")
        {
            anim.SetTrigger("bala");
        }
    }
}
