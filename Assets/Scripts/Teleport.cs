using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
	public LayerMask layerPC;
	public GameObject tele;//Objeto com este script
	public GameObject spawn;//onde o personagem irá aparecer
	public GameObject PC;
    public GameObject bossObj;
    public GameObject checaini;
    public GameObject vet1;
    public GameObject vet2;
    public GameObject[] vetIni;
    public GameObject[] vetArma;
    public GameObject[] vetItem;
    public GameObject[] vetUpgrade;
    public GameObject[] vetLoja;
    public bool loja;
    public Transform item1;
    public Transform item2;
    public Transform item3;
    public float limite;
    public float limiteSpawn;
    public float auxlimite;
    public bool boss;
    public int fase;
    public GameObject bau;
    public GameObject alambique;
    public GameObject armadilha;
  
    // Use this for initialization
    void Start () {
		GameObject aux = GameObject.FindWithTag("Player");
		PC = aux;
        checaini = GameObject.FindGameObjectWithTag("checaIni");
        bossObj = GameObject.FindGameObjectWithTag("Boss");

    }
	
	// Update is called once per frame
	void Update () {
        ChecaIni checa = checaini.GetComponent<ChecaIni>();
        Collider2D toqueTele;
		toqueTele = Physics2D.OverlapCircle (tele.transform.position, 0.1f, layerPC);
		if (toqueTele&& checa.Nini <= 0 && loja==false && boss==false) {
            lacoRespawn();
            PC.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, PC.transform.position.z);
            cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
            tesouroAleatorio();
            armadilhaSpawn();
            scriPC.vigor = scriPC.vigorMax-1;           
        }
        if (toqueTele && checa.Nini <= 0 && loja == true && boss == false)
        {
            respawnItem();
            PC.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, PC.transform.position.z);
            cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
            scriPC.vigor = scriPC.vigorMax - 1;
        }
        if (toqueTele && checa.Nini <= 0 && loja == false && boss == true)
        {
            if (fase == 1)
            {
                //Bearman scr = bossObj.gameObject.GetComponent<Bearman>();
                //scr.ativo = true;

                PC.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, PC.transform.position.z);
                cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
                scriPC.vigor = scriPC.vigorMax - 1;
            }
            if (fase == 2)
            {
               // golemBoss scr = bossObj.gameObject.GetComponent<golemBoss>();
               // scr.ativo = true;

                PC.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, PC.transform.position.z);
                cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
                scriPC.vigor = scriPC.vigorMax - 1;
            }
            if (fase == 3)
            {
               // RatoAmareloBoss scr = bossObj.gameObject.GetComponent<RatoAmareloBoss>();
               // scr.ativo = true;

                PC.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, PC.transform.position.z);
                cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
                scriPC.vigor = scriPC.vigorMax - 1;
            }
        }
    }
    void armadilhaSpawn ()
    {
        int i = Random.Range(0, 100);
        if (i<=40)
        {
            int aux = Random.Range(1, 5);
            int cont = 0;
            while(cont<aux)
            {

                float posx = Random.Range(vet1.transform.position.x+2*cont, vet2.transform.position.x-2 * cont);
                Vector3 pos = new Vector3(posx, vet1.transform.position.y, -1);
                Instantiate(armadilha, pos, Quaternion.identity);
                cont += 1;
            }
           
        }
    }
    void tesouroAleatorio()
    {
        int i = Random.Range(0, 100);
        int aux = vetIni.Length;
        Vector3 pos = new Vector3(item1.position.x, item1.position.y, -1f);
        if (i <= 10)
        {
            Instantiate(bau, pos, Quaternion.identity);
        }
        if (i <= 20&&i>10)
        {
            Instantiate(alambique , pos, Quaternion.identity);
        }
    }
    void respawn()
    {
        ChecaIni checa = checaini.GetComponent<ChecaIni>();
        float posx = Random.Range(vet1.transform.position.x, vet2.transform.position.x);
        Vector3 pos = new Vector3(posx, vet1.transform.position.y, -1);
        int i = Random.Range(0, 100);
        int aux = vetIni.Length;
        
        if (aux == 2)
        {
            if (i < 50)
            {
                Debug.Log("DEU ZERO");
                Instantiate(vetIni[0], pos, Quaternion.identity);
                auxlimite = 1;
                checa.Nini += 1;
                Debug.Log("SUMONO GERAL");
            }
            if (i >= 50)
            {
                Debug.Log("DEU UM");
                Instantiate(vetIni[1], pos, Quaternion.identity);
                auxlimite = 2;
                checa.Nini += 1;
                Debug.Log("SUMONO GERAL");
            }
        }

        if (aux == 3)
        {
            if (i < 33)
            {
                Debug.Log("DEU ZERO");
                Instantiate(vetIni[0], pos, Quaternion.identity);
                auxlimite = 1;
                checa.Nini += 1;
                Debug.Log("SUMONO GERAL");
            }
            if (i >= 33&& i<66)
            {
                Debug.Log("DEU UM");
                Instantiate(vetIni[1], pos, Quaternion.identity);
                auxlimite = 2;
                checa.Nini += 1;
                Debug.Log("SUMONO GERAL");
            }
            if (i >= 66)
            {
                Debug.Log("DEU DOIS");
                Instantiate(vetIni[2], pos, Quaternion.identity);
                auxlimite = 3;
                checa.Nini += 1;
                Debug.Log("SUMONO GERAL");
            }
        }
        if(aux==4)
        {
            if (i < 25)
            {
                Debug.Log("DEU ZERO");
                Instantiate(vetIni[0], pos, Quaternion.identity);
                auxlimite = 1;
                checa.Nini += 1;
                Debug.Log("SUMONO GERAL");
            }
            if (i >= 25 && i < 50)
            {
                Debug.Log("DEU UM");
                Instantiate(vetIni[1], pos, Quaternion.identity);
                auxlimite = 2;
                checa.Nini += 1;
                Debug.Log("SUMONO GERAL");
            }
            if (i >= 50 && i < 75)
            {
                Debug.Log("DEU DOIS");
                Instantiate(vetIni[2], pos, Quaternion.identity);
                auxlimite = 3;
                checa.Nini += 1;
                Debug.Log("SUMONO GERAL");
            }
            if (i >= 75)
            {
                Debug.Log("DEU DOIS");
                Instantiate(vetIni[3], pos, Quaternion.identity);
                auxlimite = 3;
                checa.Nini += 1;
                Debug.Log("SUMONO GERAL");
            }
        }

    }

    

    void respawnItem()
    {
        for (int i = 0; i <= 2; i++)
        {
            int x = Random.Range(0, 100);
            int y = Random.Range(0, 100);
            int z = Random.Range(0, 100);
           if(x<25)
            {
                vetLoja[0] = vetArma[0];
            }
           if(x>=25&&x<50)
            {
                vetLoja[0] = vetArma[1];
            }
            if (x >= 50 && x < 75)
            {
                vetLoja[0] = vetArma[2];
            }
            if(x>=75)
            {
                vetLoja[0] = vetArma[3];
            }
            if (y < 25)
            {
                vetLoja[1] = vetItem[0];
            }
            if (y >= 25 && x < 50)
            {
                vetLoja[1] = vetItem[1];
            }
            if (y >= 50 && x < 75)
            {
                vetLoja[1] = vetItem[2];
            }
            if (y >= 75)
            {
                vetLoja[1] = vetItem[3];
            }
            if (z < 25)
            {
                vetLoja[2] = vetUpgrade[0];
            }
            if (z >= 25 && x < 50)
            {
                vetLoja[2] = vetUpgrade[1];
            }
            if (z >= 50 && x < 75)
            {
                vetLoja[2] = vetUpgrade[2];
            }
            if (z >= 75)
            {
                vetLoja[2] = vetUpgrade[3];
            }
        }
        
        Vector3 pos1 = new Vector3(item1.position.x, item1.position.y, -0.5f);
        Vector3 pos2 = new Vector3(item2.position.x, item2.position.y, -0.5f);
        Vector3 pos3 = new Vector3(item3.position.x, item3.position.y, -0.5f);

        Instantiate(vetLoja[0], pos1, Quaternion.identity);
        Instantiate(vetLoja[1], pos2, Quaternion.identity);
        Instantiate(vetLoja[2], pos3, Quaternion.identity);


    }


void lacoRespawn()
{
    while (limite <= limiteSpawn)
    {
        respawn();
        limite += auxlimite;
    }
}

}
