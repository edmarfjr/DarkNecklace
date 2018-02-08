using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barraVidaBoss : MonoBehaviour {
    public GameObject boss;
    public GameObject BarraVida;
    public GameObject barra;
    public GameObject barra1;
    public GameObject barra2;
    public GameObject barra3;
    public float tamanhoVida;
    public int iniNum;
    
    // Use this for initialization
    void Start () {
        GameObject aux = GameObject.FindWithTag("Boss");
        boss = aux;
    }
	
	// Update is called once per frame
	void Update () {
       if(iniNum==1)
       {
            Bearman script = boss.GetComponent<Bearman>();
            float MAX = script.vidaMax;
            float Atual = script.vida;
            tamanhoVida = Atual / MAX;

            Vector2 temp = BarraVida.transform.localScale;
            Vector2 temp1 = barra.transform.localScale;
            Vector2 temp2 = barra1.transform.localScale;
            Vector2 temp3 = barra2.transform.localScale;
            Vector2 temp4 = barra3.transform.localScale;
            if (script.ativo)
            {
                temp1 = new Vector2(1f, 1f);
                barra.transform.localScale = temp1;
                barra1.transform.localScale = temp1;
                barra2.transform.localScale = temp1;
                barra3.transform.localScale = temp1;
                if (script.vida >= 0)
                    temp = new Vector2(tamanhoVida * 1f, 1f);
                else
                    temp = new Vector2(0, 1f);
                BarraVida.transform.localScale = temp;
            }
            else
            {
                temp1 = new Vector2(0f, 0f);
                barra.transform.localScale = temp1;
                barra1.transform.localScale = temp1;
                barra2.transform.localScale = temp1;
                barra3.transform.localScale = temp1;
                temp = new Vector2(0, 0.50f);
                BarraVida.transform.localScale = temp;
            }
        }
        if (iniNum == 2)
        {
            golemBoss script = boss.GetComponent<golemBoss>();
            float MAX = script.vidaMax;
            float Atual = script.vida;
            tamanhoVida = Atual / MAX;

            Vector2 temp = BarraVida.transform.localScale;
            Vector2 temp1 = barra.transform.localScale;
            Vector2 temp2 = barra1.transform.localScale;
            Vector2 temp3 = barra2.transform.localScale;
            Vector2 temp4 = barra3.transform.localScale;
            if (script.ativo)
            {
                temp1 = new Vector2(1f, 1f);
                barra.transform.localScale = temp1;
                barra1.transform.localScale = temp1;
                barra2.transform.localScale = temp1;
                barra3.transform.localScale = temp1;
                if (script.vida >= 0)
                    temp = new Vector2(tamanhoVida * 1f, 1f);
                else
                    temp = new Vector2(0, 1f);
                BarraVida.transform.localScale = temp;
            }
            else
            {
                temp1 = new Vector2(0f, 0f);
                barra.transform.localScale = temp1;
                barra1.transform.localScale = temp1;
                barra2.transform.localScale = temp1;
                barra3.transform.localScale = temp1;
                temp = new Vector2(0, 0.50f);
                BarraVida.transform.localScale = temp;
            }
        }
        if (iniNum == 3)
        {
            RatoAmareloBoss script = boss.GetComponent<RatoAmareloBoss>();
            float MAX = script.vidaMax;
            float Atual = script.vida;
            tamanhoVida = Atual / MAX;

            Vector2 temp = BarraVida.transform.localScale;
            Vector2 temp1 = barra.transform.localScale;
            Vector2 temp2 = barra1.transform.localScale;
            Vector2 temp3 = barra2.transform.localScale;
            Vector2 temp4 = barra3.transform.localScale;
            if (script.ativo)
            {
                temp1 = new Vector2(1f, 1f);
                barra.transform.localScale = temp1;
                barra1.transform.localScale = temp1;
                barra2.transform.localScale = temp1;
                barra3.transform.localScale = temp1;
                if (script.vida >= 0)
                    temp = new Vector2(tamanhoVida * 1f, 1f);
                else
                    temp = new Vector2(0, 1f);
                BarraVida.transform.localScale = temp;
            }
            else
            {
                temp1 = new Vector2(0f, 0f);
                barra.transform.localScale = temp1;
                barra1.transform.localScale = temp1;
                barra2.transform.localScale = temp1;
                barra3.transform.localScale = temp1;
                temp = new Vector2(0, 0.50f);
                BarraVida.transform.localScale = temp;
            }
        }
    }
}
