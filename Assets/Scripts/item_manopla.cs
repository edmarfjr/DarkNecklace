using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_manopla : MonoBehaviour {

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
    public bool comprou;
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
            float x = Input.GetAxis("Vertical");
            if (x > 0.5)
            {
                if (scriPC.moedas < preco)
                {
                    semGrana = true;
                }
                if (scriPC.moedas >= preco && comprou == false)
                {
                    this.gameObject.transform.localScale = new Vector2(0, 0);
                    scriPC.moedas -= preco;
                    scriPC.bonusAtaque += 1;
                    comprou = true;
                    Destroy(this.gameObject,2f);
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
            if (semGrana == false && comprou == false)
            {
                GUI.Label(new Rect(Screen.height / 1.2f, Screen.width / 3 - 50, 100, 100), texto);
                GUI.Label(new Rect(Screen.height / 1.2f, Screen.width / 3, 100, 100), preco.ToString());
            }
            if (semGrana == true)
            {
                GUI.Label(new Rect(Screen.height / 1.2f, Screen.width / 3 - 50, 100, 100), "sem moedas suficiente");
            }
            if (comprou == true)
            {
                GUI.Label(new Rect(Screen.height / 1.2f, Screen.width / 3 - 50, 100, 100), "ATAQUE ALMENTADO");
            }
        }
    }
}


