using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class armadilhaBomba : MonoBehaviour {
    public GameObject gatilho;
    public LayerMask layerPC;
    public Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Collider2D toque;
        toque = Physics2D.OverlapCircle(gatilho.transform.position, 1.5f, layerPC);
        if(toque)
        {
            Debug.Log("bum");
            anim.SetTrigger("ativar");
            Destroy(this.gameObject, 1.5f);
        }

    }
}
