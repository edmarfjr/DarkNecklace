using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class golem : MonoBehaviour {
    public int vida;
    public Rigidbody2D rbd;
    public GameObject PC;
    public float vel;
    public bool agro;
    public bool atacando;
    public Animator anim;
    public bool golpeado;
    public bool morreu;
    public GameObject checaini;
    public bool alcAtaq;
    public float distAgro;
    public float esperAtaq;
    public GameObject drop1;
    public GameObject drop2;
    public float dir;
    public float distAtaq;
    public BoxCollider2D coll;
    public bool dropou;
    // Use this for initialization
    void Start () {
        morreu = false;
        GameObject aux = GameObject.FindWithTag("Player");
        PC = aux;
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        checaini = GameObject.FindGameObjectWithTag("checaIni");
        coll = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (esperAtaq > 0)
        {
            esperAtaq = esperAtaq - Time.deltaTime;
        }
        dire();
        alcanceAtaque();
        StartCoroutine(ataque());
        if (atacando)
        {
            coll.enabled = false;
            moveAtaque();
        }
        if(!atacando)
        {
            coll.enabled = true;
        }
        if(vida<=0)
        {
             morrer();
        }
    }
    void dire()
    {
        if (rbd.transform.position.x > PC.transform.position.x && !atacando)
        {
            rbd.transform.localScale = new Vector2(1, 1);
        }
        if (rbd.transform.position.x < PC.transform.position.x && !atacando)
        {
            rbd.transform.localScale = new Vector2(-1, 1);
        }
    }
    void morrer()
    {
             if(!morreu)
             {
                
                morreu = true;
                ChecaIni checa = checaini.GetComponent<ChecaIni>();
                checa.morreuUm();
                anim.SetTrigger("morrendo");
                drop();
                Destroy(this.gameObject, 1f);
                rbd.velocity = new Vector2(0, rbd.velocity.y);
        }
    }
    public void drop()
    {
        int i = Random.Range(0, 100);
        if(!dropou)
        {
            if (i < 20)
            {
                Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1, -1);
                Instantiate(drop1, pos, Quaternion.identity);
                dropou = true;
            }
            else
            {
                Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1, -1);
                Instantiate(drop2, pos, Quaternion.identity);
                dropou = true;
            }
        }
       
    }
    public void tomouDano(int x, float kb)
    {
        if (!golpeado)
        {
            golpeado = true;
            StartCoroutine(piscaCor());
            vida = vida - x;
            golpeado = false;
        }
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
    void moveAtaque()
    {
        if (!morreu)
        {
            rbd.velocity = new Vector2(vel * 2.5f * dir, rbd.velocity.y);
        }
        if(morreu)
        {
            rbd.velocity = new Vector2(0, rbd.velocity.y);
        }
    }
    IEnumerator ataque()
    {
        yield return new WaitForSeconds(0.4f);
        if (alcAtaq == true && esperAtaq <= 0 && !morreu)
        {
            Vector2 posInicio = rbd.transform.position;
            Vector2 posPC = PC.transform.position;

            if (posInicio.x > posPC.x && !atacando)
            {
                dir = -1;
                anim.SetBool("rolando", true);
                yield return new WaitForSeconds(0.5f);
                atacando = true;
                yield return new WaitForSeconds(0.7f);
                atacando = false;
                anim.SetBool("rolando", false);
                esperAtaq = 1.6f;
            }
            if (posInicio.x < posPC.x && !atacando)
            {
                dir = 1;
                anim.SetBool("rolando", true);
                yield return new WaitForSeconds(0.5f);
                atacando = true;
                yield return new WaitForSeconds(0.7f);
                atacando = false;
                anim.SetBool("rolando", false);
                esperAtaq = 1.6f;
            }


        }
    }
    void alcanceAtaque()
    {
        Vector2 PCposi = new Vector2(PC.transform.position.x, 0);
        Vector2 morcego = new Vector2(rbd.transform.position.x, 0);
        if (Vector2.Distance(morcego, PCposi) <= distAtaq&& !morreu)
        {
            rbd.velocity = new Vector2(0, 0);
            alcAtaq = true;
        }
        else
        {
             alcAtaq = false;
        }

    }
}
