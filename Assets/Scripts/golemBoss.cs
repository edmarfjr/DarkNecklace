using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golemBoss : MonoBehaviour {
    public Rigidbody2D rbd;
    public GameObject PC;
    public int vida;
    public int vidaMax;
    public bool atacando;
    public float esperataq1;
    public float esperataq2;
    public Animator anim;
    public GameObject checaini;
    public bool morreu;
    public Transform pedraPois;
    public GameObject pedraObj;
    public Transform vet1;
    public Transform vet2;
    public int pedrasN;
    public int numPedras;
    public bool ativo;
    public float dire;

    // Use this for initialization
    void Start () {
        morreu = false;
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PC = GameObject.FindGameObjectWithTag("Player");
        checaini = GameObject.FindGameObjectWithTag("checaIni");
    }
	
	// Update is called once per frame
	void Update () {
        dir();
        coldown();
        StartCoroutine(ataque1());
        StartCoroutine(ataque2());
        morrer();
    }

    void dir()
    {
        if (rbd.transform.position.x > PC.transform.position.x && !atacando)
        {
            rbd.transform.localScale = new Vector2(1, 1);
            dire = -1;
        }
        if (rbd.transform.position.x < PC.transform.position.x && !atacando)
        {
            rbd.transform.localScale = new Vector2(-1, 1);
            dire = 1;
        }
    }
    IEnumerator ataque1()
    {
        if (Vector2.Distance(rbd.transform.position, PC.transform.position) < 20 && Vector2.Distance(rbd.transform.position, PC.transform.position) > 8 && esperataq1 <= 0 && !atacando)
        {
            rbd.velocity = new Vector2(0, 0);
            atacando = true;
            //PlaySingle(ataq, 1.2f);
            esperataq1 = 5;
            anim.SetBool("tacando", true);
            yield return new WaitForSeconds(1.1f);
            criaPedra();
            yield return new WaitForSeconds(0.5f);
            anim.SetBool("tacando", false);
            atacando = false;

        }
    }
    IEnumerator ataque2()
    {
        if (Vector2.Distance(rbd.transform.position, PC.transform.position) <= 8 && esperataq2 <= 0 && !atacando)
        {
            rbd.velocity = new Vector2(0, 0);
            atacando = true;
            //PlaySingle(ataq, 1.2f);
            esperataq2 = 4;
            anim.SetBool("batendo", true);
            yield return new WaitForSeconds(1.5f);
            pedrasN = 0;
            caiLaco();
            anim.SetBool("batendo", false);
            atacando = false;

        }
    }
    void coldown()
    {
        if (esperataq1>0)
        {
            esperataq1 = esperataq1 - Time.deltaTime;
        }
        if (esperataq2 > 0)
        {
            esperataq2 = esperataq2 - Time.deltaTime;
        }
    }

    void criaPedra()
    {
        var pedra = Instantiate(pedraObj, pedraPois.position, Quaternion.identity) as GameObject;
        pedra.GetComponent<Rigidbody2D>().AddForce(dire*Vector3.right * 1000);
    }

    void caiPedra()
    {
        ChecaIni checa = checaini.GetComponent<ChecaIni>();
        float posx = Random.Range(vet1.transform.position.x, vet2.transform.position.x);
        Vector3 pos = new Vector3(posx, vet1.transform.position.y, -1);
        int i = Random.Range(0, 100);

        Instantiate(pedraObj, pos, Quaternion.identity);
        pedrasN += 1;
    }
    void caiLaco()
    {
        
       while (pedrasN < numPedras)
        {
            caiPedra();
            
            }
    }

    public void tomouDano(int x, float kb)
    {
        /*
        anim.SetTrigger("golpeado");
        if (PC.transform.position.x > rbd.position.x)
            rbd.velocity = new Vector2(-4 * kb, 1);
        if (PC.transform.position.x < rbd.position.x)
            rbd.velocity = new Vector2(4 * kb, 1);
        */
        StartCoroutine(piscaCor());
        vida = vida - x;


    }
    IEnumerator piscaCor()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    void morrer()
    {

        if (!morreu)
        {
            if (vida <= 0 /*&& estaNoChao*/)
            {
                morreu = true;
                ChecaIni checa = checaini.GetComponent<ChecaIni>();
                checa.morreuBoss();
                anim.SetTrigger("morreu");
                //drop();
                Destroy(this.gameObject, 0.9f);
            }
        }
    }

}
