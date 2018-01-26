using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDVIGOR : MonoBehaviour {
	public GameObject player;
	public GameObject BarraVigor;
    public GameObject barra;
    public float tamanhoVigor;
    public GameObject barraFundo;
    // Use this for initialization
    void Start () {
		GameObject aux = GameObject.FindWithTag("Player");
		player = aux;
	}

	// Update is called once per frame
	void Update () {
		cacadoraScript script = player.GetComponent<cacadoraScript> ();
		float MAX = script.vigorMax;
		float Atual = script.vigor;
		tamanhoVigor = (Atual/10) ;

		Vector2 temp =BarraVigor.transform.localScale;
        if (script.vigor>=0)
		    temp = new Vector2 (tamanhoVigor,1f);
        else
            temp = new Vector2((MAX / 10) * 1f, 1f);
        BarraVigor.transform.localScale = temp;

        Vector2 temp2 = barra.transform.localScale;
        if (script.vigor >= 0)
            temp2 = new Vector2((MAX / 10) * 1f, 1f);
        barra.transform.localScale = temp2;

        Vector2 temp3 = barraFundo.transform.localScale;
        if (script.vigor >= 0)
            temp3 = new Vector2((MAX / 10) * 1f, 1f);
        barraFundo.transform.localScale = temp3;

    }
}
