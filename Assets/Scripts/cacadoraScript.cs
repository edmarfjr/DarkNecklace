using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class cacadoraScript : MonoBehaviour {
    public GameObject itemArremeco;
    public Transform posiItemArremeco;
	public GameObject encostouIni;
	public GameObject pe;
	public LayerMask layerPlat;
	public LayerMask layerIni;
	public bool dash;
	public bool atacou;
	public float timer;
	public float velocidade;
	public float pulo;
	public float esquiva;
	public int vidaMax;
	public int vida;
	public int vigorMax;
	public int vigor;
	public bool estaNoChao;
	public bool direita;
	private Rigidbody2D rbd;
	private Animator anim;
	public GameObject chaoVerificador;
	public bool dead;
	public GameObject Colisor;
	public bool isRunningCoroutine;
	public AudioClip moveSound1;
    public bool andasom;
	public AudioClip ataca1;
    public GameObject arma;
    public int dano;
	public AudioClip dash1;
    public int municao;
    public int moedas;
    public AudioClip pegaMoeda;
	public AudioClip Jump1;
	public AudioClip die1;
	public AudioClip Gameover;
	public bool encostouDir;
    public bool encostouEsc;
    public Text muni;
    public Text moeda;
    public float knockCont;
	public float knockL;
	public float knockPwr;
    public float x;
    public float invencibilidade;
    public string armaTipo;
    public string itemTipo;
    public float somAndaTemp;
    public GameObject gStatus;

    // Use this for initialization
    void Start () {
        gStatus = GameObject.FindGameObjectWithTag("gameStatus");
        GameStatus gs = gStatus.GetComponent<GameStatus>();
        armaTipo = gs.armaTipo;
        itemTipo = gs.itemTipo;
        //itemTipo = "lancaMagi";
        knockL = 0f;
		knockPwr = 3;
		vidaMax = gs.vida;
		vida = vidaMax;
		atacou = false;
		dead = false;
		vigorMax = gs.vigor;
		dash = false;
		vigor = vigorMax;
		direita = true;
		velocidade = 7;
		pulo = 5;
		esquiva = 10; 
		timer = 0;
		rbd = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
        andasom = false;
        municao = gs.muni;
        moedas = gs.moedas;
	}

	// Update is called once per frame
	void Update () {
        muni.text = municao.ToString();
        if (vida > vidaMax)
            vida = vidaMax;
        if (municao <= 0)
        {
            municao = 0;
        }
        moeda.text = moedas.ToString();
        if (moedas <= 0)
        {
            moedas = 0;
        }
        estaNoChao = Physics2D.Linecast (pe.transform.position, transform.position, 1 << LayerMask.NameToLayer ("Piso"));
        Arma scrArma = arma.GetComponent<Arma>();
        invencivel();
        dano = scrArma.dano;
        if (invencibilidade > 0)
        {
            knockCont -= Time.deltaTime;
            anim.SetBool("golpeada", true);
        }
        if (invencibilidade <= 0)
        {
            anim.SetBool("golpeada", false);
        }
        if (somAndaTemp>0)
        {
            somAndaTemp -= Time.deltaTime;
        }
        

		if (estaNoChao == true)
			anim.SetBool ("chao", true);
		else
			anim.SetBool ("chao", false);

		Andar ();
		Pular ();
		StartCoroutine (Ataque ());
        StartCoroutine (AtaqueEsp ());
        StartCoroutine (Esquivar ());

		if (isRunningCoroutine == false) {
			StartCoroutine (RegeneraVigor ());
		}

		Esquivar ();

		if (vida <= 0) {
            dead = true;
			anim.SetTrigger ("morrendo");
			StartCoroutine (morreu ());
		
		}


		Collider2D toque;
		toque = Physics2D.OverlapCircle(pe.transform.position,0.1f,layerPlat);

		if (toque != null) {
			transform.parent = toque.transform;
			estaNoChao = false;
		} else {
			transform.parent = null;
			estaNoChao = true;
		}

		

	}	

   

	void Andar ()
	{	if (andasom == true && somAndaTemp<=0 && estaNoChao&&!dash)
        {
          //  soundManager.instance.RandomsizeSdx(moveSound1);
            somAndaTemp = 0.5f;       
        }
		if (dash == false && atacou == false&&dead==false) {
			
			if (invencibilidade <= 0) {
                x = Input.GetAxis("Horizontal");
               // x = CrossPlatformInputManager.GetAxis("Horizontal");
                rbd.velocity = new Vector2 (x * velocidade, rbd.velocity.y);
               
            } else {
                x = 0;
                
            }
			

			if (x == 0) {
				anim.SetBool ("movendo", false);
                andasom = false;
            } else {
				anim.SetBool ("movendo", true);
                andasom = true;
            }
			if (x > 0) {
				direita = true;
				transform.localScale = new Vector2 (1, 1);
			
					
			}
			if (x < 0) {
				direita = false;
                transform.localScale = new Vector2(-1, 1);

            }
	} else if(atacou == true)  {
		rbd.velocity = new Vector2 (0, rbd.velocity.y);
		}
	}

	void Pular()
	{
		if (Input.GetButtonDown("Jump") && estaNoChao && !dash) {
			rbd.velocity = new Vector2 (rbd.velocity.x, pulo);
         //   soundManager.instance.PlaySingle(Jump1, 1.3f);
        }
	}

	IEnumerator Esquivar()
	{
		if (Input.GetButtonDown("Fire3") && estaNoChao && vigor>=1 && dash == false&&dead==false&& invencibilidade <= 0) {
			dash = true;
           // soundManager.instance.PlaySingle(dash1, 1.4f);
            anim.SetBool ("esquiva", true);
			if (direita==true) {
				rbd.velocity=new Vector2(esquiva,rbd.velocity.y);
				
			} else {
				rbd.velocity=new Vector2(-esquiva,rbd.velocity.y);
				
			}

			vigor -= 4;

			yield return new WaitForSeconds(1);
			anim.SetBool ("esquiva", false);
			dash = false;
			velocidade = 7;


		}
	}

		



	IEnumerator RegeneraVigor()
	{
		
		if (vigor < vigorMax) {
			if (vigor < 0) {
				vigor = -1;
			}
			isRunningCoroutine = true;
			yield return new WaitForSeconds(0.3f);
			vigor =vigor+ 1;
			
			isRunningCoroutine = false;

		}
	}
    IEnumerator AtaqueEsp()
    {
        if (itemTipo == "facaArremeco")
        {
            if (Input.GetButtonDown("Fire2") && atacou == false && vigor > 0 && dead == false && municao > 0&&estaNoChao)
            {
                // vigor -= 2;
                //soundManager.instance.PlaySingle(ataca1, 1.4f);
                atacou = true;
                anim.SetBool("throw", true);
                yield return new WaitForSeconds(0.2f);
                criaItem();
                yield return new WaitForSeconds(0.3f);
                anim.SetBool("throw", false);
                atacou = false;
                municao -= 1;
            }
        }
            if (itemTipo == "lancaMagi")
            {
                if (Input.GetButtonDown("Fire2") && atacou == false && vigor > 0 && dead == false && municao > 0 && estaNoChao)
                {
                   // vigor -= 4;
                    //soundManager.instance.PlaySingle (ataca1);

                    atacou = true;

                    anim.SetBool("ataqueEsp", true);

                    yield return new WaitForSeconds(0.8f);

                    anim.SetBool("ataqueEsp", false);
                    atacou = false;
                    municao -= 1;
                }
            }
            /*  */
        
    }
	IEnumerator Ataque()
	{
        if (armaTipo=="espadaCurta")
        {
            if (Input.GetButtonDown("Fire1") && atacou == false && vigor > 0 && dead == false && estaNoChao) {
                vigor -= 4;
               // soundManager.instance.PlaySingle(ataca1,1);

                atacou = true;

                anim.SetBool("ataque", true);

                yield return new WaitForSeconds(0.4f);

                anim.SetBool("ataque", false);
                atacou = false;
            }
        }
        
        if (armaTipo == "adaga")
        {
            if (Input.GetButtonDown("Fire1") && atacou == false && vigor > 0 && dead == false && estaNoChao)
            {
                vigor -= 2;
                
              //  soundManager.instance.PlaySingle (ataca1,1.3f);

                atacou = true;
                float x=0;
                float aux = Random.Range(0, 100);
                if (aux < 33)
                    x = 0;
                if (aux > 33 && aux < 66)
                    x = 0.5f;
                if (aux > 66)
                    x = 1;

                anim.SetBool("ataqueAdaga",true);
                anim.SetFloat("adaga", x);

                yield return new WaitForSeconds(0.3f);

                anim.SetBool("ataqueAdaga", false);
                atacou = false;
            }
        }
        if (armaTipo == "machado")
        {
            if (Input.GetButtonDown("Fire1") && atacou == false && vigor > 0 && dead == false && estaNoChao)
            {
                vigor -= 6;
             //   soundManager.instance.PlaySingle (ataca1,0.7f);

                atacou = true;

                anim.SetBool("ataqueMachado", true);

                yield return new WaitForSeconds(1f);

                anim.SetBool("ataqueMachado", false);
                atacou = false;
            }
        }
        if (armaTipo == "espadao")
        {
            if (Input.GetButtonDown("Fire1") && atacou == false && vigor > 0 && dead == false && estaNoChao)
            {
                vigor -= 4;
                yield return new WaitForSeconds(0.1f);
             //   soundManager.instance.PlaySingle (ataca1, 0.6f);

                atacou = true;

                anim.SetBool("ataqueEspadao", true);

                yield return new WaitForSeconds(0.9f);

                anim.SetBool("ataqueEspadao", false);
                atacou = false;
            }
        }
        if (armaTipo == "lanca")
        {
            if (Input.GetButtonDown("Fire1") && atacou == false && vigor > 0 && dead == false && estaNoChao)
            {
                vigor -= 4;
              //  soundManager.instance.PlaySingle (ataca1,1f);

                atacou = true;

                anim.SetBool("ataqueLanca", true);

                yield return new WaitForSeconds(0.7f);

                anim.SetBool("ataqueLanca", false);
                atacou = false;
            }
        }
        if (armaTipo == "rapier")
        {
            if (Input.GetButtonDown("Fire1") && atacou == false && vigor > 0 && dead == false && estaNoChao)
            {
                vigor -= 3;
             //   soundManager.instance.PlaySingle(ataca1, 0.9f);

                atacou = true;

                anim.SetBool("rapier", true);

                yield return new WaitForSeconds(0.6f);

                anim.SetBool("rapier", false);
                atacou = false;
            }
        }
    }





	IEnumerator morreu()
	{
		yield return new WaitForSeconds(1.2f);
		//anim.SetBool ("movendo", false);
		anim.SetBool ("morreu",true);
		dead = true;
        //soundManager.instance.musicSound.Stop ();
        GameStatus gs = gStatus.GetComponent<GameStatus>();
        gs.zeraStatus();
        SceneManager.LoadScene(0);
    }






    public void knockBDIR()
    {
        if (invencibilidade <= 0)
        { 
         
                rbd.velocity = new Vector2(knockPwr, rbd.velocity.y);
         //   soundManager.instance.PlaySingle(die1, 1.4f);
            //encostouDir = false;
        }
    }
    public void knockBESQ()
    {

        if (invencibilidade <= 0)
        {

            rbd.velocity = new Vector2(-knockPwr, rbd.velocity.y);
         //   soundManager.instance.PlaySingle(die1, 1.4f);

            //encostouDir = false;
        }


    }

    public void invencivel()
    {
        if(invencibilidade>0)
        {
            invencibilidade -= Time.deltaTime;
        }
        if (invencibilidade < 0)
            invencibilidade = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag == "municao" && atacou == false)
        {
            municao += 1;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Moeda" && atacou == false)
        {
            int i = Random.Range(1, 5);
            moedas += i;
         //  soundManager.instance.PlaySingle(pegaMoeda, 1.4f);
            Destroy(col.gameObject);
        }

    }
    void criaItem()
    {
        GameObject cloneItem = Instantiate(itemArremeco, new Vector2(posiItemArremeco.position.x, posiItemArremeco.position.y), Quaternion.identity);
        cloneItem.transform.localScale = this.transform.localScale;
    }

}
