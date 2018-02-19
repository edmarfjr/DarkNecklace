using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vidaAtDez : MonoBehaviour {
    public GameObject PC;
    public Animator anim;
    public Image deze;
    public Sprite[] numero;
    // Use this for initialization
    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        deze = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        cacadoraScript script = PC.GetComponent<cacadoraScript>();
        int dezena = script.vida / 10;
        if (dezena == 0)
        {
            deze.sprite = numero[0];
        }
        if (dezena == 1)
        {
            deze.sprite = numero[1];
        }
        if (dezena == 2)
        {
            deze.sprite = numero[2];
        }
        if (dezena == 3)
        {
            deze.sprite = numero[3];
        }
        if (dezena == 4)
        {
            deze.sprite = numero[4];
        }
        if (dezena == 5)
        {
            deze.sprite = numero[5];
        }
        if (dezena == 6)
        {
            deze.sprite = numero[6];
        }
        if (dezena == 7)
        {
            deze.sprite = numero[7];
        }
        if (dezena == 8)
        {
            deze.sprite = numero[8];
        }
        if (dezena == 9)
        {
            deze.sprite = numero[9];
        }
    }
}

