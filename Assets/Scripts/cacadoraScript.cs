using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class cacadoraScript : MonoBehaviour {
    public GameObject[] itemArremeco;
    public Transform posiItemArremeco;
	public GameObject encostouIni;
	public GameObject pe;
	public LayerMask layerPlat;
	public LayerMask layerIni;
	public bool dash;
	public bool atacou;
	public float timer;
	public float velocidade;
    public float velAnda;
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
    public AudioSource sfxSource;
    public float lovPitchRange;
    public float highPitchRange;
    public int armadura;
    public int bonusAtaque;
    public int escudoDivino;
    public bool andaDir;
    public bool bloq;
    public bool andaDirTouch;
    public bool andaEsqTouch;
    public GameObject armaObj;
    public bool envenenada;
    public float tempVeneno;
    public float auxVeneno;

    // Use this for initialization
    void Start () {
        tempVeneno = 0;
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
		velocidade = gs.velocidade;
        velAnda = velocidade;
		pulo = 5;
		esquiva = 10; 
		timer = 0;
		rbd = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
        andasom = false;
        municao = gs.muni;
        moedas = gs.moedas;
        armadura = gs.armadura;
        bonusAtaque = gs.bonusAtaque;
	}

	// Update is called once per frame
	void Update () {
        // Arma Armascript = armaObj.GetComponent<Arma>();
        // Armascript.chamaArma();
        
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
        if (tempVeneno>0)
        {
            tempVeneno -= Time.deltaTime;
        }
        if (auxVeneno > 0)
        {
            auxVeneno -= Time.deltaTime;
        }

        if (estaNoChao == true)
			anim.SetBool ("chao", true);
		else
			anim.SetBool ("chao", false);
        if(Time.timeScale>0)
        {
            
            Andar();
            Pular();
            StartCoroutine(Ataque());
            StartCoroutine(AtaqueEsp());
            StartCoroutine(Esquivar());
        }
		
        veneno();
		if (isRunningCoroutine == false) {
			StartCoroutine (RegeneraVigor ());
		}

		//Esquivar ();

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
    IEnumerator piscaCor()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.green;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    void veneno()
    {
        if (tempVeneno>0)
        {
            envenenada = true;
        }
        else
        {
            envenenada = false;
        }
        
        if (envenenada)
        {

            
            Debug.Log(auxVeneno);
            if(auxVeneno <= 0)
            {
                auxVeneno = 2;
                StartCoroutine(piscaCor());
                vida -= 1;

            }
            
        }
    }

    void Andar ()
	{	if (andasom == true && somAndaTemp<=0 && estaNoChao&&!dash)
        {
          RandomsizeSdx(moveSound1);
            somAndaTemp = 0.5f;       
        }
		if (dash == false && atacou == false&&dead==false) {
			
			if (invencibilidade <= 0) {
                x = Input.GetAxis("Horizontal");
                if (andaDirTouch)
                {
                    x = 1;
                }
                if (andaEsqTouch)
                {
                    x = -1;
                }
                if (x>0)
                { andaDir = true; }
                if (x <= 0)
                { andaDir = false; }
                // x = CrossPlatformInputManager.GetAxis("Horizontal");
                if(bloq)
                {
                    rbd.velocity = new Vector2(0, rbd.velocity.y);
                }
                if(!bloq)
                {
                    rbd.velocity = new Vector2(x * velAnda, rbd.velocity.y);
                }
              

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
			if (x > 0||andaDirTouch) {
				direita = true;
				transform.localScale = new Vector2 (1, 1);				
			}
			if (x < 0||andaEsqTouch) {
				direita = false;
                transform.localScale = new Vector2(-1, 1);
            }
	} else if(atacou == true)  {
		rbd.velocity = new Vector2 (0, rbd.velocity.y);
		}
	}
    
    void OnCollision2D(Collision2D coll)
    {
        gStatus = GameObject.FindGameObjectWithTag("gameStatus");
        GameStatus gs = gStatus.GetComponent<GameStatus>();
        int dire = 0;
        if (this.transform.position.x > coll.transform.position.x)
        {
            dire = 1;
        }
        else
        {
            dire = -1;
        }
        if (coll.gameObject.tag == "inimigo" && dire == 1 && andaDir == false)
        {
            bloq = true;
            Debug.Log(bloq);
        }
        if (coll.gameObject.tag == "inimigo" && dire == 1 && andaDir == true)
        {
            bloq = false;
            Debug.Log(bloq);
        }
        if (coll.gameObject.tag == "inimigo" && dire == -1 && andaDir == true)
        {
            bloq = true;
            Debug.Log(bloq);
        }
        if (coll.gameObject.tag == "inimigo" && dire == -1 && andaDir == false)
        {
            bloq = false;
            Debug.Log(bloq);
        }
    }

    
    void Pular()
	{
		if (Input.GetButtonDown("Jump") && estaNoChao && !dash) {
			rbd.velocity = new Vector2 (rbd.velocity.x, pulo);
         PlaySingle(Jump1, 1.3f);
        }
	}

	IEnumerator Esquivar()
	{
		if (Input.GetButtonDown("Fire3") && estaNoChao && vigor>=1 && dash == false&&dead==false&& invencibilidade <= 0) {
			dash = true;
           PlaySingle(dash1, 1.3f);
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
                PlaySingle(ataca1, 1.4f);
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
        if (itemTipo == "pistola")
        {
            if (Input.GetButtonDown("Fire2") && atacou == false && vigor > 0 && dead == false && municao > 0 && estaNoChao)
            {
                // vigor -= 2;
                PlaySingle(ataca1, 1.4f);
                atacou = true;
                anim.SetBool("pistola", true);
                yield return new WaitForSeconds(0.2f);
                criaItem();
                yield return new WaitForSeconds(0.6f);
                anim.SetBool("pistola", false);
                atacou = false;
                municao -= 1;
            }
        }
        if (itemTipo == "rifle")
        {
            if (Input.GetButtonDown("Fire2") && atacou == false && vigor > 0 && dead == false && municao > 0 && estaNoChao)
            {
                // vigor -= 2;
                PlaySingle(ataca1, 1.6f);
                atacou = true;
                anim.SetBool("rifle", true);
                yield return new WaitForSeconds(0.2f);
                criaItem();
                yield return new WaitForSeconds(0.6f);
                anim.SetBool("rifle", false);
                atacou = false;
                municao -= 1;
            }
        }
        if (itemTipo == "bombaIncend")
        {
            if (Input.GetButtonDown("Fire2") && atacou == false && vigor > 0 && dead == false && municao > 0 && estaNoChao)
            {
                // vigor -= 2;
                PlaySingle(ataca1, 1.4f);
                atacou = true;
                anim.SetBool("bombaIncend", true);
                yield return new WaitForSeconds(0.2f);
                criaItem();
                yield return new WaitForSeconds(0.3f);
                anim.SetBool("bombaIncend", false);
                atacou = false;
                municao -= 1;
            }
        }
        if (itemTipo == "lancaMagi")
            {
                if (Input.GetButtonDown("Fire2") && atacou == false && vigor > 0 && dead == false && municao > 0 && estaNoChao)
                {
                   // vigor -= 4;
                   // PlaySingle (ataca1);
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
               PlaySingle(ataca1,1);
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
                
              PlaySingle (ataca1,1.3f);

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
             PlaySingle (ataca1,0.7f);

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
             PlaySingle (ataca1, 0.6f);

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
              PlaySingle (ataca1,1f);

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
             PlaySingle(ataca1, 0.9f);

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
		yield return new WaitForSeconds(1.1f);
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
         PlaySingle(die1, 1.4f);
            //encostouDir = false;
        }
    }
    public void knockBESQ()
    {

        if (invencibilidade <= 0)
        {

            rbd.velocity = new Vector2(-knockPwr, rbd.velocity.y);
         PlaySingle(die1, 1.4f);

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
            moedas += i/2;
         PlaySingle(pegaMoeda, 1.4f);
            Destroy(col.gameObject);
        }

    }
    void criaItem()
    {
        if(itemTipo=="facaArremeco")
        {
            GameObject cloneItem = Instantiate(itemArremeco[0], new Vector2(posiItemArremeco.position.x, posiItemArremeco.position.y), Quaternion.identity);
            cloneItem.transform.localScale = this.transform.localScale;
        }
        if (itemTipo == "pistola")
        {
            GameObject cloneItem = Instantiate(itemArremeco[1], new Vector2(posiItemArremeco.position.x, posiItemArremeco.position.y), Quaternion.identity);
            cloneItem.transform.localScale = this.transform.localScale;
        }
        if (itemTipo == "bombaIncend")
        {
            GameObject cloneItem = Instantiate(itemArremeco[2], new Vector2(posiItemArremeco.position.x, posiItemArremeco.position.y), Quaternion.identity);
            cloneItem.transform.localScale = this.transform.localScale;
        }
        if (itemTipo == "rifle")
        {
            GameObject cloneItem = Instantiate(itemArremeco[3], new Vector2(posiItemArremeco.position.x, posiItemArremeco.position.y), Quaternion.identity);
            cloneItem.transform.localScale = this.transform.localScale;
        }
    }

    public void PlaySingle(AudioClip clip, float aux)
    {
        sfxSource.clip = clip;
        sfxSource.pitch = aux;
        sfxSource.Play();
    }

    public void RandomsizeSdx(params AudioClip[] clip)
    {
        int randomIndex = Random.Range(0, clip.Length);
        float randomPitch = Random.Range(lovPitchRange, highPitchRange);
        sfxSource.clip = clip[randomIndex];
        sfxSource.pitch = randomPitch;
        sfxSource.Play();
    }
    public void andarDireitaTouch()
    {
        andaDirTouch = true;
    }
    public void andarEsquerdaTouch()
    {
        andaEsqTouch = true;
    }
    public void andarDireitaTouchF()
    {
        andaDirTouch = false;
    }
    public void andarEsquerdaTouchF()
    {
        andaEsqTouch = false;
    }
    public IEnumerator ataqueTouch()
    {
        if (atacou == false && vigor > 0 && dead == false && !estaNoChao)
        {
            if (armaTipo == "espadaCurta")
            {

                vigor -= 4;
                PlaySingle(ataca1, 1);
                atacou = true;
                anim.SetBool("ataque", true);
                yield return new WaitForSeconds(0.4f);
                anim.SetBool("ataque", false);
                atacou = false;

            }

            if (armaTipo == "adaga")
            {

                vigor -= 2;

                PlaySingle(ataca1, 1.3f);

                atacou = true;
                float x = 0;
                float aux = Random.Range(0, 100);
                if (aux < 33)
                    x = 0;
                if (aux > 33 && aux < 66)
                    x = 0.5f;
                if (aux > 66)
                    x = 1;

                anim.SetBool("ataqueAdaga", true);
                anim.SetFloat("adaga", x);

                yield return new WaitForSeconds(0.3f);

                anim.SetBool("ataqueAdaga", false);
                atacou = false;

            }
            if (armaTipo == "machado")
            {

                vigor -= 6;
                PlaySingle(ataca1, 0.7f);

                atacou = true;

                anim.SetBool("ataqueMachado", true);

                yield return new WaitForSeconds(1f);

                anim.SetBool("ataqueMachado", false);
                atacou = false;

            }
            if (armaTipo == "espadao")
            {

                vigor -= 4;
                yield return new WaitForSeconds(0.1f);
                PlaySingle(ataca1, 0.6f);

                atacou = true;

                anim.SetBool("ataqueEspadao", true);

                yield return new WaitForSeconds(0.9f);

                anim.SetBool("ataqueEspadao", false);
                atacou = false;

            }
            if (armaTipo == "lanca")
            {

                vigor -= 4;
                PlaySingle(ataca1, 1f);

                atacou = true;

                anim.SetBool("ataqueLanca", true);

                yield return new WaitForSeconds(0.7f);

                anim.SetBool("ataqueLanca", false);
                atacou = false;

            }
            if (armaTipo == "rapier")
            {

                vigor -= 3;
                PlaySingle(ataca1, 0.9f);

                atacou = true;

                anim.SetBool("rapier", true);

                yield return new WaitForSeconds(0.6f);

                anim.SetBool("rapier", false);
                atacou = false;

            }
        }
    }
    public void chamaAtaqueTouch()
    {
        StartCoroutine(ataqueTouch());
    }
    public void chamaAtaqueEspTouch()
    {
        StartCoroutine(ataqueEspTouch());
    }
    public void chamaDashTouch()
    {
        StartCoroutine(DashTouch());
    }
    public IEnumerator ataqueEspTouch()
    {
        if(atacou == false  && dead == false && municao > 0 && !estaNoChao && !dash)
        {
            if (itemTipo == "facaArremeco")
            {
                
                    // vigor -= 2;
                    PlaySingle(ataca1, 1.4f);
                    atacou = true;
                    anim.SetBool("throw", true);
                    yield return new WaitForSeconds(0.2f);
                    criaItem();
                    yield return new WaitForSeconds(0.3f);
                    anim.SetBool("throw", false);
                    atacou = false;
                    municao -= 1;
                
            }
            if (itemTipo == "pistola")
            {
                
                    // vigor -= 2;
                    PlaySingle(ataca1, 1.4f);
                    atacou = true;
                    anim.SetBool("pistola", true);
                    yield return new WaitForSeconds(0.2f);
                    criaItem();
                    yield return new WaitForSeconds(0.6f);
                    anim.SetBool("pistola", false);
                    atacou = false;
                    municao -= 1;
                
            }
            if (itemTipo == "bombaIncend")
            {
               
                
                    // vigor -= 2;
                    PlaySingle(ataca1, 1.4f);
                    atacou = true;
                    anim.SetBool("bombaIncend", true);
                    yield return new WaitForSeconds(0.2f);
                    criaItem();
                    yield return new WaitForSeconds(0.3f);
                    anim.SetBool("bombaIncend", false);
                    atacou = false;
                    municao -= 1;
                
            }
            if (itemTipo == "lancaMagi")
            {

                {
                    // vigor -= 4;
                    // PlaySingle (ataca1);
                    atacou = true;
                    anim.SetBool("ataqueEsp", true);
                    yield return new WaitForSeconds(0.8f);
                    anim.SetBool("ataqueEsp", false);
                    atacou = false;
                    municao -= 1;
                }
            }
        }
        
    }

    public IEnumerator DashTouch()
    {
        if (!estaNoChao && vigor >= 1 && dash == false && dead == false && invencibilidade <= 0)
        {
            
            Debug.Log("cu");
            dash = true;
            PlaySingle(dash1, 1.3f);
            anim.SetBool("esquiva", true);
            if (direita == true)
            {
                rbd.velocity = new Vector2(esquiva, rbd.velocity.y);
            }
            else
            {
                rbd.velocity = new Vector2(-esquiva, rbd.velocity.y);

            }
            vigor -= 4;
            yield return new WaitForSeconds(1);
            anim.SetBool("esquiva", false);
            dash = false;
            velocidade = 7;
        }
    }
}
