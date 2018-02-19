using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imagemSubarmaMenu : MonoBehaviour {
    public GameObject PC;
    public Image armaMenu;
    public Sprite[] arma;
    // Use this for initialization
    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player");
        armaMenu = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        cacadoraScript script = PC.GetComponent<cacadoraScript>();
        if (script.itemTipo == "facaArremeco")
        {
            armaMenu.sprite = arma[0];
        }
        if (script.itemTipo == "pistola")
        {
            armaMenu.sprite = arma[1];
        }
        if (script.itemTipo == "bombaIncend")
        {
            armaMenu.sprite = arma[2];
        }       
    }
}

