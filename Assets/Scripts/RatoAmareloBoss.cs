using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatoAmareloBoss : MonoBehaviour {
    public GameObject magia;
    public Transform posiMagia;
    public Transform[] summonPosi;
    public GameObject[] summon;
    public Transform[] telePosi;
    public Rigidbody2D rbd;
    public GameObject PC;
    public GameObject checaini;
    public Animator anim;   
    public float distAgro;
    public float distAtaq;
    public float esperAtaq;
    public float esperSummon;
    public int vida;
    public int vidaMax;
    public int contTele;
    public bool golpeado;
    public bool teleport;
    public bool atacando;
    public bool ativo;
    public bool morreu;
    public bool agro;
    public bool alcAtaq;
    public bool invocou;
    public bool invocafim;
    public ChecaIni checa;
   
    
	// Use this for initialization
	void Start () {
        morreu = false;
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PC = GameObject.FindGameObjectWithTag("Player");
        checaini = GameObject.FindGameObjectWithTag("checaIni");
        vida = vidaMax;
        ativo = false;
        contTele = 3;
        esperSummon = 30;
        checa = checaini.GetComponent<ChecaIni>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        alcanceAtaque();
        if (esperSummon > 0)
        {
            esperSummon -= Time.deltaTime;
        }
        if (esperAtaq>0)
        {
            esperAtaq -= Time.deltaTime;
        }
        if (ativo)
        {
            if (Time.timeScale > 0)
            { 
                morrer();
                if (vida > 0)
                {
                    StartCoroutine(ataque());
                    StartCoroutine(invoca());
                    if (contTele == 0 && !teleport && !invocou && invocafim)
                    {
                        StartCoroutine(tele());
                    }
                    if (invocou && checa.Nini <= 0 && invocafim)
                    {
                        StartCoroutine(tele());
                        invocou = false;
                        esperSummon = 20;
                    }
                }
            }
        }
    }
    void agrar()
    {
        if ((Vector2.Distance(rbd.transform.position, PC.transform.position) < distAgro) && (alcAtaq == false))
            agro = true;
        else
            agro = false;
    }
    void direcao()
    {
        if (rbd.transform.position.x > PC.transform.position.x && atacando == false)
        {
            rbd.transform.localScale = new Vector2(-1, 1);
        }
        if (rbd.transform.position.x < PC.transform.position.x && atacando == false)
        {
            rbd.transform.localScale = new Vector2(1, 1);
        }
    }
    
    IEnumerator tele()
    {
        teleport = true;
        anim.SetBool("teleporte", true);
        yield return new WaitForSeconds(1.1f);
        int i = Random.Range(0, 3);
        this.gameObject.transform.position = telePosi[i].position;
        anim.SetBool("teleporte", false);
        anim.SetBool("aparece", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("aparece", false);
        contTele = 3;
        teleport = false;
        invocou = false;
        golpeado = false;
    }
    void alcanceAtaque()
    {
        if (Vector2.Distance(rbd.transform.position, PC.transform.position) < distAtaq)
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
        yield return new WaitForSeconds(1.8f);
        if (alcAtaq && esperAtaq <= 0  && !morreu && contTele>0 && !invocou && !golpeado && esperSummon>0)
        {
            atacando = true;
            esperAtaq = 3;
            anim.SetBool("magia", true);
            yield return new WaitForSeconds(0.8f);
            criaMagia();
            yield return new WaitForSeconds(0.4f);
            criaMagia();
            yield return new WaitForSeconds(0.4f);
            criaMagia();
            anim.SetBool("magia", false);
            atacando = false;
            esperAtaq = 2;
            contTele -= 1;
        }
    }
    IEnumerator invoca()
    {
        if (esperSummon <= 0 && !morreu &&!invocou&&!atacando)
        {
            invocafim = false;
            anim.SetBool("teleporte", true);
            yield return new WaitForSeconds(1.0f);           
            this.gameObject.transform.position = telePosi[4].position;
            anim.SetBool("teleporte", false);
            anim.SetBool("aparece", true);
            yield return new WaitForSeconds(0.2f);
            anim.SetBool("aparece", false);                      
            atacando = true;
            yield return new WaitForSeconds(0.7f);
            anim.SetBool("magia", true);
            yield return new WaitForSeconds(1.7f);
            anim.SetBool("magia", false);
            criaMob();
            atacando = false;
            invocafim = true;
        }
    }
    void criaMagia()
    {
        GameObject cloneFlecha = Instantiate(magia, new Vector2(posiMagia.position.x, posiMagia.position.y), Quaternion.identity);
        cloneFlecha.transform.localScale = this.transform.localScale;
    }
    void criaMob()
    {
        if(!invocou)
        {
            int i = Random.Range(0, 3);
            GameObject mob1 = Instantiate(summon[i], new Vector2(summonPosi[0].position.x, summonPosi[0].position.y), Quaternion.identity);
            int u = Random.Range(0, 3);
            GameObject mob2 = Instantiate(summon[u], new Vector2(summonPosi[1].position.x, summonPosi[1].position.y), Quaternion.identity);
            int o = Random.Range(0, 3);
            GameObject mob3 = Instantiate(summon[o], new Vector2(summonPosi[2].position.x, summonPosi[2].position.y), Quaternion.identity);
            int p = Random.Range(0, 3);
            GameObject mob4 = Instantiate(summon[p], new Vector2(summonPosi[3].position.x, summonPosi[3].position.y), Quaternion.identity);
            
            checa.Nini = 4;
            invocou = true;
        }        
    }
    public IEnumerator tomouDano(int x, float kb)
    {
        golpeado = true;
        StartCoroutine(piscaCor());
        vida = vida - x;
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("magia", false);
        StartCoroutine(tele());       
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
            if (vida <= 0&&invocafim)
            {
                morreu = true;
                ChecaIni checa = checaini.GetComponent<ChecaIni>();
                checa.morreuBoss();
                anim.SetTrigger("morreu");
                //drop();
                Destroy(this.gameObject, 1.1f);
            }
        }
    }
}
