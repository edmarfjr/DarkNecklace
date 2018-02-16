using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abreArmadilha : MonoBehaviour {
    private Animator anim;
    public float cont;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        cont = 3;
    }
	
	// Update is called once per frame
	void Update () {
        abreArmad();
        if (cont > 0 )
        {
            cont -= Time.deltaTime;
        }
    }
    void abreArmad()
    {

        if (cont <= 0)
        {
            anim.SetTrigger("abrir");
            cont = 3;
        }

    }

}
