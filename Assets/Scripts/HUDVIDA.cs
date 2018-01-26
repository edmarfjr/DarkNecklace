using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDVIDA : MonoBehaviour {
	public GameObject player;
	public GameObject BarraVida;
    public GameObject barra;
    public GameObject barraFundo;
	public float tamanhoVida;
	// Use this for initialization
	void Start () {
		GameObject aux = GameObject.FindWithTag("Player");
		player = aux;
	}
	
	// Update is called once per frame
	void Update () {
		cacadoraScript script = player.GetComponent<cacadoraScript> ();
		float MAX = script.vidaMax;
		float Atual = script.vida;
		tamanhoVida = (Atual/10) ;



        Vector2 temp = BarraVida.transform.localScale;
        if (script.vida >= 0)
            temp = new Vector2(tamanhoVida, 1f);
        else
            temp = new Vector2(0, 0.1f);
        BarraVida.transform.localScale = temp;

        Vector2 temp2 = barra.transform.localScale;
        if (script.vida >= 0)
            temp2 = new Vector2((MAX/10) * 1f, 1f);
        barra.transform.localScale = temp2;

        Vector2 temp3 = barraFundo.transform.localScale;
        if (script.vida >= 0)
            temp3 = new Vector2((MAX / 10) * 1f, 1f);
        barraFundo.transform.localScale = temp3;

    }
}
