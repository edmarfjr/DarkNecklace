﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour {
    public int vida;
    public int vigor;
    public string armaTipo;
    public string itemTipo;
    public int muni;
    public int moedas;
	// Use this for initialization
	void Start () {
        vida = 10;
        vigor = 10;
        armaTipo = "espadaCurta";
        itemTipo = "facaArremeco";
        muni = 3;
        moedas = 0;
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void saveStatus()
    {
        GameObject pc = GameObject.FindWithTag("Player");
        cacadoraScript scrPc = pc.GetComponent<cacadoraScript>();
        vida = scrPc.vidaMax;
        vigor = scrPc.vigorMax;
        armaTipo = scrPc.armaTipo;
        itemTipo = scrPc.itemTipo;
        muni = scrPc.municao;
        moedas = scrPc.moedas;
    }

    public void zeraStatus()
    {
        vida = 10;
        vigor = 10;
        armaTipo = "espadaCurta";
        itemTipo = "facaArremeco";
        muni = 3;
        moedas = 0;
    }
}