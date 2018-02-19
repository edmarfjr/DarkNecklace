using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armadilhaPedraGatilho : MonoBehaviour {
    public GameObject pedra;
    public GameObject gatilho;
    public GameObject seguraPedra;
    public LayerMask layerPC;
    public Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        
        Collider2D toque;
        toque = Physics2D.OverlapCircle(gatilho.transform.position, 3.0f, layerPC);
        if(toque)
        {
            StartCoroutine(caiPedra());
            
        }
    }

    IEnumerator caiPedra()
    {
        anim.SetBool("balanca", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("balanca", false);
        PedraCai script = pedra.GetComponent<PedraCai>();
        BoxCollider2D gat = seguraPedra.GetComponent<BoxCollider2D>();
        gat.enabled = false;
        script.gatilho = true;
    }
   
}
