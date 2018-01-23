using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class col_dano : MonoBehaviour {

	public GameObject player;
	// Use this for initialization
	void Start () {
        
    }

    /*void OnTriggerEnter2D(Collider2D col){
        cacadoraScript script = player.GetComponent<cacadoraScript>();
        if (col.tag.Equals ("ataqueIni")&& script.invencibilidade <= 0)
        {
		
		
		Rigidbody2D rbd = player.GetComponent<Rigidbody2D>();
		if (rbd.transform.position.x > col.transform.position.x) {
                Debug.Log("ENCOSTOU DIREITA");
                script.knockBDIR();
            } else {
                Debug.Log("ENCOSTOU ESQUERDA");
                script.knockBESQ();
            }
		script.knockCont = script.knockL;
		//levaDano (1);
		}

	}

	// Update is called once per frame
	void Update () {
		
	} */
    public void levaDano(int x)
	{	
		
		cacadoraScript script = player.GetComponent<cacadoraScript> ();
		script.vida -= 1;
        script.invencibilidade = 0.7f;

    }  
}
