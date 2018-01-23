using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worm : MonoBehaviour {
    public Rigidbody2D rbd;
    public GameObject PC;
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
    public bool morreu;
    public GameObject checaini;
    public GameObject drop1;
    public GameObject drop2;
    public AudioClip ataq;
    public AudioClip hit;
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
        if (tempStag > 0)
        {
            tempStag = tempStag - Time.deltaTime;
        }
        if (esperAtaq > 0)
        {
            esperAtaq = esperAtaq - Time.deltaTime;
        }

        dir();
        agrar();
        andar();
        StartCoroutine(ataque());

        morrer();
    }

    void dir()
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

        if (agro == true && !atacando && tempStag <= 0)
        {
           
            if (rbd.transform.position.x > PC.transform.position.x)
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
        if ((Vector2.Distance(rbd.transform.position, PC.transform.position) < distAgro) && (alcAtaq == false))
            agro = true;
        else
            agro = false;
    }

    IEnumerator ataque()
    {
        if ((Vector2.Distance(rbd.transform.position, PC.transform.position) < 0.2f) && esperAtaq <= 0 && tempStag <= 0)
        {
            rbd.velocity = new Vector2(0, 0);
            atacando = true;
           // soundManager.instance.PlaySingle(ataq, 1.2f);
            esperAtaq = 1;
            anim.SetBool("atacando", true);
            yield return new WaitForSeconds(1.2f);
            anim.SetBool("atacando", false);
            atacando = false;

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
    public void tomouDano(int x, float kb)
    {
       
        tempStag = 1.0f;
        //soundManager.instance.PlaySingle(hit, 1f);
        StartCoroutine(piscaCor());
       /* if (tempStag>0)
        {
            if (PC.transform.position.x > rbd.position.x)
                rbd.velocity = new Vector2(-4 * kb, rbd.velocity.y);
            if (PC.transform.position.x < rbd.position.x)
                rbd.velocity = new Vector2(4 * kb, rbd.velocity.y);
        }
        */

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
}
