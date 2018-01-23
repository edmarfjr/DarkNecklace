using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratmanXaman : MonoBehaviour {

    public GameObject magia;
    public Transform posiMagia;
    public Rigidbody2D rbd;
    public GameObject PC;
    public Transform agro1;
    public Transform agro2;
    public float vel;
    public Animator anim;
    public bool agro;
    public float distAgro;
    public float distAtaq;
    public bool alcAtaq;
    public float esperAtaq;
    public int vida;
    public bool atacando;
    public bool golpeado;
    public bool encostouDir;
    public float tempStag;
    public bool morreu;
    public GameObject checaini;
    public GameObject drop1;
    public GameObject drop2;

    void Start()
    {
        morreu = false;
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PC = GameObject.FindGameObjectWithTag("Player");
        checaini = GameObject.FindGameObjectWithTag("checaIni");
    }
    void Update()
    {

        if (tempStag > 0)
        {
            tempStag = tempStag - Time.deltaTime;
        }
        if (esperAtaq > 0)
        {
            esperAtaq = esperAtaq - Time.deltaTime;
        }
        dirRato();
        agrar();
        alcanceAtaque();
        andar();
        StartCoroutine(ataque());

        morrer();
    }

    void dirRato()
    {
        if (rbd.transform.position.x > PC.transform.position.x && !atacando && tempStag <= 0)
        {
            rbd.transform.localScale = new Vector2(-1, 1);
        }
        if (rbd.transform.position.x < PC.transform.position.x && !atacando && tempStag <= 0)
        {
            rbd.transform.localScale = new Vector2(1, 1);
        }
    }

    void andar()
    {

        if (agro == true && !atacando && tempStag <= 0 && !alcAtaq)
        {
            anim.SetBool("movendo", true);
            if (rbd.transform.position.x > PC.transform.position.x)
            {
                rbd.velocity = new Vector2(-vel, rbd.velocity.y);
            }
            else
            {
                rbd.velocity = new Vector2(vel, rbd.velocity.y);
            }
        }
        else
            anim.SetBool("movendo", false);
    }

    void agrar()
    {
        if ((Vector2.Distance(rbd.transform.position, PC.transform.position) < distAgro) && (alcAtaq == false))
            agro = true;
        else
            agro = false;
    }

    void alcanceAtaque()
    {
        
            if (Vector2.Distance(rbd.transform.position, PC.transform.position) < distAtaq && tempStag <= 0)
            {
                rbd.velocity = new Vector2(0, 0);
                alcAtaq = true;

            }
            else
            {
                alcAtaq = false;

            }
    }       

    IEnumerator ataque()
    {
        
        if (alcAtaq && esperAtaq <= 0 && tempStag <= 0 && !morreu)
        {
            atacando = true;
            esperAtaq = 3;
            anim.SetBool("magia", true);
            yield return new WaitForSeconds(0.8f);
            criaMagia();
            anim.SetBool("magia", false);

            atacando = false;
        }

    }

    public void tomouDano(int x, float kb)
    {
        Debug.Log("MORRE RATO");
        tempStag = 1f;

        anim.SetTrigger("golpeado");
        if (PC.transform.position.x > rbd.position.x)
            rbd.velocity = new Vector2(-4 * kb, 1);
        if (PC.transform.position.x < rbd.position.x)
            rbd.velocity = new Vector2(4 * kb, 1);

        vida = vida - x;


    }


    void morrer()
    {

        if (!morreu)
        {
            if (vida <= 0)
            {
                morreu = true;
                ChecaIni checa = checaini.GetComponent<ChecaIni>();
                checa.morreuUm();
                anim.SetTrigger("morreu");
                drop();
                Destroy(this.gameObject, 0.7f);
            }
        }
    }

    public void drop()
    {
        int i = Random.Range(0, 100);
        if (i < 10)
        {
            Debug.Log(i);
            Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1, -1);
            Instantiate(drop1, pos, Quaternion.identity);
        }
        else
        {
            Debug.Log(i);
            Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1, -1);
            Instantiate(drop2, pos, Quaternion.identity);
        }
    }

    void criaMagia()
    {

        if (tempStag <= 0)
        {
            GameObject cloneFlecha = Instantiate(magia, new Vector2(posiMagia.position.x, posiMagia.position.y), Quaternion.identity);
            cloneFlecha.transform.localScale = this.transform.localScale;
        }

    }
}

