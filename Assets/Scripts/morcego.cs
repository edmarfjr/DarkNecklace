using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class morcego : MonoBehaviour
{
    public int vida;
    public Rigidbody2D rbd;
    public GameObject PC;
    public Transform personagem;
    public float vel;
    public bool agro;
    public bool atacando;
    public Animator anim;
    public bool golpeado;
    public bool morreu;
    public GameObject checaini;
    public cacadoraScript pcScr;
    public bool alcAtaq;
    public float tempStag;
    public float distAgro;
    public float distAtaq;
    public float esperAtaq;
    public float dir;
    public GameObject drop1;
    public GameObject drop2;

    // Use this for initialization
    void Start()
    {
        morreu = false;
        GameObject aux = GameObject.FindWithTag("Player");
        PC = aux;
        personagem = aux.transform;
        rbd = GetComponent<Rigidbody2D>(); 
        anim = GetComponent<Animator>();
        pcScr = PC.GetComponent<cacadoraScript>();
        checaini = GameObject.FindGameObjectWithTag("checaIni");
    }

    // Update is called once per frame
    void Update()
    {
        morrer();
        if (tempStag > 0)
        {
            tempStag = tempStag - Time.deltaTime;
        }
        if (esperAtaq > 0)
        {
            esperAtaq = esperAtaq - Time.deltaTime;
        }
        if(tempStag>0)
        {
            anim.SetBool("golpeado", true);
        }
        if (tempStag <= 0)
        {
            anim.SetBool("golpeado", false);
        }
        agrar();
        //andar();
        alcanceAtaque();
        StartCoroutine(ataque());
        if (atacando && tempStag<=0)
            moveAtaque();
        if (atacando==false && tempStag <= 0)
            andar();
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
                //anim.SetTrigger("morreu");
                drop();
                Destroy(this.gameObject, 0.7f);
            }
        }
    }
    void andar()
    {

        if (agro == true && atacando==false && tempStag <= 0 && alcAtaq==false)
        {

            if (rbd.transform.position.x > personagem.position.x)
            {
                rbd.velocity = new Vector2(-vel, rbd.velocity.y);
            }
            else
            {
                rbd.velocity = new Vector2(vel, rbd.velocity.y);
            }
        }
    }

    void agrar()
    {
        if ((Vector2.Distance(rbd.transform.position, PC.transform.position) <= distAgro) && (alcAtaq == false))
            agro = true;
        else
            agro = false;
    }
    void alcanceAtaque()
    {
        Vector2 PCposi = new Vector2(personagem.position.x, 0);
        Vector2 morcego = new Vector2(rbd.transform.position.x, 0);
            if (Vector2.Distance(morcego, PCposi) <= distAtaq && tempStag <= 0)
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
        if (alcAtaq==true && esperAtaq <= 0 && tempStag <= 0)
        {
            Vector2 posInicio = rbd.transform.position;
            Vector2 posPC = personagem.position;

            if (posInicio.x > posPC.x&& !atacando)
            {
                atacando = true;
                dir = -1;
                anim.SetBool("atacando", true);
                yield return new WaitForSeconds(1.1f);
                atacando = false;
                anim.SetBool("atacando", false);
                esperAtaq = 2f;
            }
            if (posInicio.x < posPC.x && !atacando)
            {
                atacando = true;
                dir = 1;
                anim.SetBool("atacando", true);
                yield return new WaitForSeconds(1.1f);
                atacando = false;
                anim.SetBool("atacando", false);
                esperAtaq = 2f;
            }
            

        }
    }

    void moveAtaque()
    { 
        if(tempStag <= 0)
            rbd.velocity = new Vector2(vel * 1.2f*dir, rbd.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("ataque"))
        { 
            tempStag =0.6f;
            Arma dano = col.gameObject.GetComponent<Arma>();
            float kn = dano.knockback;
            if (personagem.position.x > rbd.position.x)
            {
                rbd.velocity = new Vector2(-1 * 5 * kn, rbd.velocity.y);
            }
            if (personagem.position.x < rbd.position.x)
            {
                rbd.velocity = new Vector2(1 * 5 * kn, rbd.velocity.y);
            }
            //soundManager.instance.PlaySingle (dano);
            vida -= dano.dano;
        }
        if (col.tag.Equals("itemArremeco"))
        {
            
            
            tempStag = 0.6f;
            itemArremeco dano = col.gameObject.GetComponent<itemArremeco>();
            float kn = dano.knockback;
            if (personagem.position.x > rbd.position.x)
            {
                rbd.velocity = new Vector2(-1 * 5 * kn, rbd.velocity.y);
            }
            if (personagem.position.x < rbd.position.x)
            {
                rbd.velocity = new Vector2(1 * 5 * kn, rbd.velocity.y);
            }
            //soundManager.instance.PlaySingle (dano);
            vida -= dano.dano;
            
        }
        if (col.gameObject.tag == "player_col")
        {

            Debug.Log("colidiu com inimigo");
            col_dano script = col.gameObject.GetComponent<col_dano>();
            script.levaDano(1);
        }
    }

    public void drop()
    {
        int i = Random.Range(0, 100);
        if (i < 20)
        {
            Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1, -1);
            Instantiate(drop1, pos, Quaternion.identity);
        }
        else
        {
            Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1, -1);
            Instantiate(drop2, pos, Quaternion.identity);
        }
    }
}