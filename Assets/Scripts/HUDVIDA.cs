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
		tamanhoVida = (Atual/MAX) ;



        Vector2 temp = BarraVida.transform.localScale;
        if (script.vida >= 0)
            temp = new Vector2(script.vida * 0.179f, 0.50f);
        else
            temp = new Vector2(0, 0.50f);
        BarraVida.transform.localScale = temp;

        Vector2 temp2 = barra.transform.localScale;
        if (script.vida >= 0)
            temp2 = new Vector2(script.vidaMax * 0.2f, 1f);
        barra.transform.localScale = temp2;

        Vector2 temp3 = barraFundo.transform.localScale;
        if (script.vida >= 0)
            temp3 = new Vector2(script.vidaMax * 0.2f, 1f);
        barra.transform.localScale = temp3;

    }
}
