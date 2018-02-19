using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imagemArmaMenu : MonoBehaviour {
    public GameObject PC;
    public Image armaMenu;
    public Sprite[] arma;
    // Use this for initialization
    void Start () {
        PC = GameObject.FindGameObjectWithTag("Player");
        armaMenu = GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        cacadoraScript script = PC.GetComponent<cacadoraScript>();
        if (script.armaTipo=="espadaCurta")
        {
            armaMenu.sprite = arma[0];
        }
        if (script.armaTipo == "adaga")
        {
            armaMenu.sprite = arma[1];
        }
        if (script.armaTipo == "machado")
        {
            armaMenu.sprite = arma[2];
        }
        if (script.armaTipo == "espadao")
        {
            armaMenu.sprite = arma[3];
        }
        if (script.armaTipo == "lanca")
        {
            armaMenu.sprite = arma[4];
        }
        if (script.armaTipo == "rapier")
        {
            armaMenu.sprite = arma[5];
        }
    }
}
