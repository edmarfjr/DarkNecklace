using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bearman : MonoBehaviour
{
    public GameObject pe;
    public Rigidbody2D rbd;
    public GameObject PC;
    public GameObject onda;
    public Transform posiOnda;
    public int vida;
    public int vidaMax;
    public float vel;
    public bool atacando;
    public bool ataq1;
    public bool ataq2;
    public bool ataq3;
    public bool encostouDir;
    public Animator anim;
    public GameObject checaini;
    public bool morreu;
    public bool alcAtaq1;
    public bool alcAtaq2;
    public float distAtaq1;
    public float distAtaq2;
    public bool agro;
    public float distAgro;
    public float esperAtaq1;
    public float esperAtaq2;
    public bool andatras;
    public float pausaAnda;
    public bool pulou;
    public bool estaNoChao;
    public bool ativo;
    public CapsuleCollider2D coll;
    public GameObject drop1;
    public GameObject drop2;
    public GameObject drop3;
    public bool parede;


    // Use this for initialization
    void Start()
    {
        coll = GetComponent<CapsuleCollider2D>();
        morreu = false;
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PC = GameObject.FindGameObjectWithTag("Player");
        checaini = GameObject.FindGameObjectWithTag("checaIni");
        vida = vidaMax;
        ativo = false;
    }

    // Update is called once per frame
    void Update()
    {
        estaNoChao = Physics2D.Linecast(pe.transform.position, rbd.transform.position, 1 << LayerMask.NameToLayer("Piso"));
       if(!estaNoChao)
        {
            this.gameObject.layer = LayerMask.NameToLayer("inimigo_pulo");
        }
       if(estaNoChao)
        {
            this.gameObject.layer = LayerMask.NameToLayer("inimigo");
        }
        if(ativo)
        {
            morrer();
            if (vida>0)
            {
                Debug.Log(PC.transform.position);
                if (esperAtaq1 > 0)
                {
                    esperAtaq1 = esperAtaq1 - Time.deltaTime;
                }
                if (esperAtaq2 > 0)
                {
                    esperAtaq2 = esperAtaq2 - Time.deltaTime;
                }
                if (pausaAnda > 0)
                {
                    pausaAnda = pausaAnda - Time.deltaTime;
                }
                agrar();
                //alcanceAtaque1();
                andar();
                StartCoroutine(andarTras());
                if (esperAtaq1 <= 0)
                { StartCoroutine(ataque1()); }
                direcao();
            }
            
        }
        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag=="parede")
        {
            parede = true;
        }
       
    }
    void direcao()
    {
        if (rbd.transform.position.x > PC.transform.position.x && atacando==false)
        {
            rbd.transform.localScale = new Vector2(1, 1);
        }
        if (rbd.transform.position.x < PC.transform.position.x && atacando==false)
        {
            rbd.transform.localScale = new Vector2(-1, 1);
        }
    }
    void andar()
    {
        if (agro == true && !atacando && !alcAtaq1 && pausaAnda<=0 && estaNoChao && Vector2.Distance(rbd.transform.position, PC.transform.position) >= distAtaq1)
        {
            
            if (rbd.transform.position.x > PC.transform.position.x)
            {
                anim.SetBool("movendo", true);
                rbd.velocity = new Vector2(-vel, rbd.velocity.y);
            }
            else
            {
                anim.SetBool("movendo", true);
                rbd.velocity = new Vector2(vel, rbd.velocity.y);
            }
        }
        else
            anim.SetBool("movendo", false);
    }
    IEnumerator andarTras()
    {

        if (andatras==true && estaNoChao)
        {
            pausaAnda = 3.5f;
            coll.enabled = false;
            if (rbd.transform.position.x > PC.transform.position.x)
            {
                if(parede)
                {
                    rbd.velocity = new Vector2(-vel * 0.9f, rbd.velocity.y);
                    rbd.velocity = new Vector2(rbd.velocity.x, 7);
                }
                else
                {
                    rbd.velocity = new Vector2(vel * 0.9f, rbd.velocity.y);
                    rbd.velocity = new Vector2(rbd.velocity.x, 7);
                }
                
            }
            else
            {
                if (parede)
                {
                    rbd.velocity = new Vector2(vel * 0.9f, rbd.velocity.y);
                    rbd.velocity = new Vector2(rbd.velocity.x, 7);
                }
                else
                {
                    rbd.velocity = new Vector2(-vel * 0.9f, rbd.velocity.y);
                    rbd.velocity = new Vector2(rbd.velocity.x, 7);
                }
            }
            andatras = false;
            coll.enabled = true;
            yield return new WaitForSeconds(2f);
            pulou = true;
            
            
            StartCoroutine(ataque2());
        }  
    }
    void agrar()
    {
        if ((Vector2.Distance(rbd.transform.position, PC.transform.position) < distAgro) && (alcAtaq1 == false))
            agro = true;
        else
            agro = false;
    }
   /* void alcanceAtaque1()
    {

        if (Vector2.Distance(rbd.transform.position, PC.transform.position) < distAtaq1)
        {
            rbd.velocity = new Vector2(0, 0);
            alcAtaq1 = true;

        }
        else
        {
            alcAtaq1 = false;

        }

    }*/
    IEnumerator ataque1()
    {
        if (esperAtaq1 <= 0 && !atacando && !andatras && estaNoChao&& Vector2.Distance(rbd.transform.position, PC.transform.position) < distAtaq1)
        {
            parede = false;
            atacando = true;
            rbd.velocity = new Vector2(0, 0);           
            esperAtaq1 = 6f;
            anim.SetBool("ataq2", true);
            yield return new WaitForSeconds(1f);
            anim.SetBool("ataq2", false);
            
            yield return new WaitForSeconds(0.8f);
            esperAtaq1 = 2f;
            atacando = false;
            andatras = true;
        }
        
    }
    IEnumerator ataque2()
    {
        if(esperAtaq1<=0.5 && esperAtaq2<=0.5 && !andatras&&!atacando&&pulou && !andatras && estaNoChao)
        {
            parede = false;
            atacando = true;
            anim.SetBool("ataq1", true);
            yield return new WaitForSeconds(1f);

            anim.SetBool("ataq1", false);
            criaOnda();
            
            alcAtaq2 = false;
            yield return new WaitForSeconds(0.8f);
            esperAtaq2 = 6f;
            pulou = false;
            atacando = false;
        }
        
    }

    public void tomouDano(int x, float kb)
    {
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
            if (vida <= 0&&estaNoChao)
            {
                morreu = true;
                ChecaIni checa = checaini.GetComponent<ChecaIni>();
                checa.morreuBoss();
                anim.SetTrigger("morreu");
                drop();
                Destroy(this.gameObject, 1.1f);
            }
        }
    }

    void criaOnda()
    {
        {
            GameObject cloneFlecha = Instantiate(onda, new Vector2(posiOnda.position.x, posiOnda.position.y), Quaternion.identity);
            cloneFlecha.transform.localScale = this.transform.localScale;
        }
    }

    public void drop()
    {
        int i = Random.Range(0, 99);
        if (i < 33)
        {
            Debug.Log(i);
            Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1, -1);
            Instantiate(drop1, pos, Quaternion.identity);
        }
        if (i>= 33 && i < 66)
        {
            Debug.Log(i);
            Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1, -1);
            Instantiate(drop2, pos, Quaternion.identity);
        }
        if (i >= 66)
        {
            Debug.Log(i);
            Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1, -1);
            Instantiate(drop3, pos, Quaternion.identity);
        }
    }
}