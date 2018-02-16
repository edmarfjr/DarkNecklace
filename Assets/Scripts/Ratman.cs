using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ratman : MonoBehaviour
{
    public GameObject flecha;
    public Transform posiFlecha;
    public Rigidbody2D rbd;
    public GameObject PC;
    public Transform agro1;
    public Transform agro2;
    public float vel;
    public Animator anim;
    public bool agro;
    public float distAgro;
    public bool alcAtaq;
    public float esperAtaq;
    public int vida;
    public bool atacando;
    public bool golpeado;
    public bool encostouDir;
    public float tempStag;
    public bool arqueiro;
    public bool morreu;
    public GameObject checaini;
    public GameObject drop1;
    public GameObject drop2;
    public AudioClip ataq;
    public AudioClip tiro;
    public AudioClip hit;
    public AudioSource sfxSource;
    public bool armadura;
    public bool alq;

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
        if (rbd.transform.position.x > PC.transform.position.x && !atacando && tempStag <= 0) {
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
        if ((Vector2.Distance(rbd.transform.position, PC.transform.position) < distAgro)&&(alcAtaq==false))
            agro = true;
        else
            agro = false;
    }

    void alcanceAtaque()
    {
        if (arqueiro == false) {
            if (Vector2.Distance(rbd.transform.position, PC.transform.position) < 2f && tempStag <= 0)
            {
                rbd.velocity = new Vector2(0, 0);
                alcAtaq = true;                
            }
            else
            {
                alcAtaq = false;
            }
        }
        if (arqueiro == true)
        {
            if (Vector2.Distance(rbd.transform.position, PC.transform.position) < 12 && tempStag <= 0)
            {
                rbd.velocity = new Vector2(0, 0);
                alcAtaq = true;
            }
            else
            {
                alcAtaq = false;
            }
        }
        if (alq == true)
        {
            if (Vector2.Distance(rbd.transform.position, PC.transform.position) < 8 && tempStag <= 0)
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

    IEnumerator ataque()
    {
        if (alcAtaq && esperAtaq <= 0 && tempStag <= 0 && alq == true)
        {
            rbd.velocity = new Vector2(0, 0);
            atacando = true;
            esperAtaq = 6;
            anim.SetBool("atacando", true);
            yield return new WaitForSeconds(0.7f);
            PlaySingle(ataq, 1.2f);
            criaFlecha();
            yield return new WaitForSeconds(0.4f);
            anim.SetBool("atacando", false);
            atacando = false;

        }
        if (alcAtaq&&esperAtaq<=0&&tempStag<=0 && arqueiro==false)
        {
            rbd.velocity = new Vector2(0, 0);
            atacando = true;
            PlaySingle(ataq, 1.2f);
            esperAtaq = 2;
            if(armadura)
            {
                anim.SetBool("atacando", true);
                yield return new WaitForSeconds(1.3f);
                anim.SetBool("atacando", false);
            }
            else
            {
                anim.SetBool("atacando", true);
                yield return new WaitForSeconds(0.8f);
                anim.SetBool("atacando", false);
            }
            atacando = false;
            
        }
        if (alcAtaq && esperAtaq <= 0 && tempStag <= 0 && arqueiro == true && !morreu)
        {
            atacando = true;
            esperAtaq = 4;
            if(armadura)
            {
                anim.SetBool("atirando", true);
                PlaySingle(tiro, 0.2f);
                yield return new WaitForSeconds(0.7f);
                
                criaFlecha();
                yield return new WaitForSeconds(0.3f);
                
                anim.SetBool("atirando", false);
            }
            else
            {
                anim.SetBool("atirando", true);
                yield return new WaitForSeconds(0.3f);
                PlaySingle(tiro, 1f);
                yield return new WaitForSeconds(0.6f);
                criaFlecha();
                anim.SetBool("atirando", false);
            }
            atacando = false;       
        }

    }

    public void tomouDano(int x, float kb)
    {
        if(!golpeado)
        {
            Debug.Log(x);
            golpeado = true;
            if (!armadura)
            {
                tempStag = 1f;
                PlaySingle(hit, 1f);
                anim.SetTrigger("golpeado");
                PlaySingle(hit, 1f);
                StartCoroutine(piscaCor());
                if (PC.transform.position.x > rbd.position.x)
                    rbd.velocity = new Vector2(-4 * kb, 1);
                if (PC.transform.position.x < rbd.position.x)
                    rbd.velocity = new Vector2(4 * kb, 1);
            }
            if (armadura)
            {
                float auxKb;
                auxKb = kb - 0.8f;
                if(auxKb<0)
                {
                    auxKb = 0;
                }
                PlaySingle(hit, 1f);
                StartCoroutine(piscaCor());
                if (PC.transform.position.x > rbd.position.x)
                    rbd.velocity = new Vector2(-4 * auxKb, 1);
                if (PC.transform.position.x < rbd.position.x)
                    rbd.velocity = new Vector2(4 * auxKb, 1);
            }
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
            Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1,-1);
            Instantiate(drop1, pos, Quaternion.identity);
        }
        else
        {
            Debug.Log(i);
            Vector3 pos = new Vector3(rbd.transform.position.x, rbd.transform.position.y + 1, -1);
            Instantiate(drop2, pos, Quaternion.identity);
        }
    }

    void criaFlecha()
    {

        if (tempStag<=0)
        {
            GameObject cloneFlecha = Instantiate(flecha, new Vector2(posiFlecha.position.x, posiFlecha.position.y), Quaternion.identity);
            cloneFlecha.transform.localScale = this.transform.localScale;
        }
        
    }
    public void PlaySingle(AudioClip clip, float aux)
    {
        sfxSource.clip = clip;
        sfxSource.pitch = aux;
        sfxSource.Play();
    }
}
