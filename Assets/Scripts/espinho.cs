using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class espinho : MonoBehaviour {

    public GameObject PC;
    public int dano;
    public Transform espim;
    // Use this for initialization
    void Start()
    {
        PC = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        cacadoraScript script = PC.GetComponent<cacadoraScript>();
        if (col.tag.Equals("player_col") && script.invencibilidade <= 0)
        {
            if(espim.position.x<PC.transform.position.x)
            {
                script.knockBDIR();
            }
            else
            {
                script.knockBESQ();
            }
            
                Debug.Log("ENCOSTOU DIREITA");
              

            
            
            script.knockCont = script.knockL;
            levaDano(1);
        }

    }
    public void levaDano(int x)
    {

        cacadoraScript script = PC.GetComponent<cacadoraScript>();
        script.vida -= dano;
        script.invencibilidade = 1;

    }
}
