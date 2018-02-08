using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bauAlambique : MonoBehaviour {
    public GameObject pocao;
    public GameObject moeda;
    public int tipo;
    public GameObject checaini;
    public bool spawnou;
    public Animator anim;

    // Use this for initialization
    void Start () {
        checaini = GameObject.FindGameObjectWithTag("checaIni");
        spawnou = false;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        spawnaItem();

    }
    void spawnaItem()
    {
        ChecaIni checa = checaini.GetComponent<ChecaIni>();
        Vector3 pos = new Vector3(this.transform.position.x, this.transform.position.y+2, this.transform.position.z-1);
        Vector3 pos2 = new Vector3(this.transform.position.x-2, this.transform.position.y + 2, this.transform.position.z - 1);
        Vector3 pos3 = new Vector3(this.transform.position.x+2, this.transform.position.y + 2, this.transform.position.z - 1);
        if (checa.Nini<=0&&spawnou==false)
        {
            if(tipo==1)
            {
                anim.SetTrigger("abriu");   
                Instantiate(moeda, pos, Quaternion.identity);
                Instantiate(moeda, pos2, Quaternion.identity);
                Instantiate(moeda, pos3, Quaternion.identity);
            }
            if (tipo == 2)
            {
                Instantiate(pocao, pos, Quaternion.identity);
            }
            spawnou = true;
        }
    }
}
