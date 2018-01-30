using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventarioItem : MonoBehaviour {
    public Image invent;
    public Sprite[] item;
    public GameObject PC;
    // Use this for initialization
    void Start () {
        invent = GetComponent<Image>();
        GameObject aux = GameObject.FindWithTag("Player");
        PC = aux;
    }
	
	// Update is called once per frame
	void Update () {
        cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
        if (scriPC.itemTipo=="facaArremeco")
        {
            invent.sprite = item[0];
        }
        if (scriPC.itemTipo == "lancaMagi")
        {
            invent.sprite = item[1];
        }
        if (scriPC.itemTipo == "pistola")
        {
            invent.sprite = item[2];
        }
        if (scriPC.itemTipo == "bombaIncend")
        {
            invent.sprite = item[3];
        }
    }
}
