using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecaIni : MonoBehaviour {
    public int Nini;
    public int boss;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void morreuUm()
    {
        Nini = Nini - 1;
    }
    public void morreuBoss()
    {
        boss = boss - 1;
    }
}
