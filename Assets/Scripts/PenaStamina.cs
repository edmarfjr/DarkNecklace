using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaStamina : MonoBehaviour
{
    public LayerMask layerPC;
    public GameObject PC;
    public bool abriu;
    public float a;
    public float b;
    public float c;
    public float d;
    public string texto;
    public int preco;
    public bool semGrana;
    // Use this for initialization
    void Start()
    {
        GameObject aux = GameObject.FindWithTag("Player");
        PC = aux;
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D toqueTele;
        toqueTele = Physics2D.OverlapCircle(this.transform.position, 0.5f, layerPC);
        cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
        if (toqueTele)
        {
            abriu = true;
            if (scriPC.moedas > preco)
            {
                semGrana = false;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (scriPC.moedas < preco)
                {
                    semGrana = true;
                }
                else
                {
                    scriPC.moedas -= preco;
                    scriPC.vigorMax += 3;
                    scriPC.vigor += 3;
                    Destroy(this.gameObject);
                }
            }
        }
        else
        {
            abriu = false;
        }
    }

    void OnGUI()
    {
        if (abriu == true)
        {
            if (semGrana == false)
            {
                GUI.Label(new Rect(a, b - 50, c, d), texto);
                GUI.Label(new Rect(a, b, c, d), preco.ToString());
            }
            if (semGrana == true)
            {
                GUI.Label(new Rect(a, b, c, d), "sem moedas suficiente");
            }
        }
    }
}

