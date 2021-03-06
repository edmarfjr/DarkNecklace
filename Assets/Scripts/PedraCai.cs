﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedraCai : MonoBehaviour {
    public float tempoDestroi;
    public GameObject PC;
    public int dano;
    public Rigidbody2D rbd;
    public bool armadilha;
    public bool gatilho;

    // Use this for initialization
    void Start () {
        rbd = GetComponent<Rigidbody2D>();
        
        PC = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if (!armadilha)
        {
            Destroy(this.gameObject, tempoDestroi);
        }
        else
        {
            if(gatilho)
            {
                Destroy(this.gameObject, tempoDestroi);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        cacadoraScript script = PC.GetComponent<cacadoraScript>();
        if (col.tag.Equals("player_col") && script.invencibilidade <= 0)
        {


            // Rigidbody2D rbd = PC.GetComponent<Rigidbody2D>();
            if (rbd.transform.position.x < col.transform.position.x)
            {
                Debug.Log("ENCOSTOU DIREITA");
                script.knockCont = 0.7f;
                script.knockBDIR();

            }
            else if (rbd.transform.position.x > col.transform.position.x)
            {
                Debug.Log("ENCOSTOU ESQUERDA");
                script.knockCont = 0.7f;
                script.knockBESQ();
            }
            script.knockCont = script.knockL;
            levaDano(1);
            Destroy(this.gameObject);
        }

    }
    public void levaDano(int x)
    {

        cacadoraScript script = PC.GetComponent<cacadoraScript>();
        script.vida -= dano;
        script.invencibilidade = 1;

    }
}
