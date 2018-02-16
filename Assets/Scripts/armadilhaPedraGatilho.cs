using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armadilhaPedraGatilho : MonoBehaviour {
    public GameObject pedra;
    public GameObject gatilho;
    public GameObject seguraPedra;
    public LayerMask layerPC;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        PedraCai script = pedra.GetComponent<PedraCai>();
        Collider2D toque;
        toque = Physics2D.OverlapCircle(gatilho.transform.position, 2.5f, layerPC);
        if(toque)
        {
            Debug.Log("gatilhou");
            BoxCollider2D gat = seguraPedra.GetComponent<BoxCollider2D>();
            gat.enabled = false;
            script.gatilho = true;
        }
    }
   
}
