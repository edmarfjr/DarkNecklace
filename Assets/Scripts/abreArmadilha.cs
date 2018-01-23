using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abreArmadilha : MonoBehaviour {
    private Animator anim;
    
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(abreArmad());

    }
    IEnumerator abreArmad()
    {

        yield return new WaitForSeconds(3);
        anim.SetTrigger("abrir");
    }

}
