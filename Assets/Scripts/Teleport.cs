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
    public GameObject[] vetItens;
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
            scriPC.vigor = scriPC.vigorMax-1;
        }
        if (toqueTele && checa.Nini == 0 && loja == true && boss == false)
        {
            respawnItem();
            PC.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, PC.transform.position.z);
            cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
            scriPC.vigor = scriPC.vigorMax - 1;
        }
        if (toqueTele && checa.Nini == 0 && loja == false && boss == true)
        {
            if (fase == 1)
            {
                Bearman scr = bossObj.gameObject.GetComponent<Bearman>();
                scr.ativo = true;

                PC.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, PC.transform.position.z);
                cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
                scriPC.vigor = scriPC.vigorMax - 1;
            }
            if (fase == 2)
            {
                golemBoss scr = bossObj.gameObject.GetComponent<golemBoss>();
                scr.ativo = true;

                PC.transform.position = new Vector3(spawn.transform.position.x, spawn.transform.position.y, PC.transform.position.z);
                cacadoraScript scriPC = PC.GetComponent<cacadoraScript>();
                scriPC.vigor = scriPC.vigorMax - 1;
            }
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
            int aux = 0;
            if(x<16)
            {
                vetLoja[i] = vetItens[0];
                aux = 0;
            }
            if (x >= 16 && x <33)
            {
                vetLoja[i] = vetItens[1];
                aux = 1;
            }
            if (x >= 33 && x < 49)
            {
                vetLoja[i] = vetItens[2];
                aux = 2;
            }
            if (x >= 49 && x < 65)
            {
                vetLoja[i] = vetItens[3];
                aux = 3;
            }
            if (x >= 65 && x < 81)
            {
                vetLoja[i] = vetItens[4];
                aux = 4;
            }
            if (x >= 81)
            {
                vetLoja[i] = vetItens[5];
                aux = 5;
            }
            if(vetLoja[1]==vetLoja[0])
            {
                if(aux<5)
                    vetLoja[i] = vetItens[aux + 1];
                if(aux<=0)
                    vetLoja[i] = vetItens[aux + 1];
                if(aux==5)
                    vetLoja[i] = vetItens[aux - 1];
            }
            if (vetLoja[2] == vetLoja[1])
            {
                if(aux<3)
                    vetLoja[i] = vetItens[aux + 2];
                if(aux==4)
                    vetLoja[i] = vetItens[aux - 2];
                else
                    vetLoja[i] = vetItens[aux + 1];
            }       
            if (vetLoja[2] == vetLoja[0])
            {
                if (aux < 3)
                    vetLoja[i] = vetItens[aux + 2];
                else
                    vetLoja[i] = vetItens[aux + 1];
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
