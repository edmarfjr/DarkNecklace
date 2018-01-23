using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

	// Use this for initialization

	public Transform posi1;
	public Transform posi2;
    public AudioSource sfxSource;
    public int vida; 
	public bool chao;
	public bool agro;
	private Rigidbody2D slime;
	public Transform personagem;
	public float velx;
	public float vely;
	public bool pausaPulo;
	public float tempoPulo;
	private Animator anim;
	public GameObject PC;
	public cacadoraScript pcScr;
	public AudioClip pula;
	public AudioClip danoS;
	public AudioClip morre;
    public GameObject checaini;
    public bool morreu;
    public GameObject drop1;
    public GameObject drop2;

    void Start () {
        morreu = false;
		GameObject aux = GameObject.FindWithTag("Player");
		personagem = aux.transform;
		slime = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		tempoPulo = 4;
		pcScr = PC.GetComponent<cacadoraScript>();
        checaini = GameObject.FindGameObjectWithTag("checaIni");
    }
	
	// Update is called once per frame
	void Update () {
		Raycast ();
        morrer();
        controleAnimation();
        if (Vector2.Distance(transform.position, personagem.position)<12) {



			if (personagem.position.x > slime.position.x) {
				//slime.transform.eulerAngles = new Vector2 (0, 0);

				if (chao == true && pausaPulo == false) {
				    PlaySingle (pula,1);
					slime.velocity = new Vector2 (1 * velx, slime.velocity.y);
					slime.velocity = new Vector2 (slime.velocity.x, vely);
					StartCoroutine(PausaPulo());
				}
			}
			if (personagem.position.x < slime.position.x) {
				//slime.transform.eulerAngles = new Vector2 (180, 0);

				//slime.transform.localScale.x=-1;
				if (chao == true && pausaPulo == false) {
					PlaySingle (pula,1);
					slime.velocity = new Vector2 (-1 * velx, slime.velocity.y);
					slime.velocity = new Vector2 (slime.velocity.x, vely);
					StartCoroutine(PausaPulo());
				}
				}
		}
		


	}

	void Raycast()
	{
		Debug.DrawLine(posi1.position, posi2.position, Color.red);


		if(Physics2D.Linecast(posi1.position,posi2.position, 1 << LayerMask.NameToLayer("Piso")))
		{
			chao = true;
		}
		else
		{
			chao = false;
		}


	}
	IEnumerator PausaPulo()
	{
		pausaPulo = true;
		yield return new WaitForSeconds(2);
		pausaPulo = false;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag.Equals ("ataque")) {
            Arma dano = col.gameObject.GetComponent<Arma>();
            float kn = dano.knockback;
            if (personagem.position.x > slime.position.x) {
				slime.velocity = new Vector2 (-1 * 5*kn, slime.velocity.y);
			}
			if (personagem.position.x < slime.position.x) {
				slime.velocity = new Vector2 (1 * 5 * kn, slime.velocity.y);
			}
		    PlaySingle (danoS,1);
			vida -= dano.dano;
		}
        if (col.tag.Equals("itemArremeco"))
        {
            itemArremeco dano = col.gameObject.GetComponent<itemArremeco>();
            float kn = dano.knockback;
            if (personagem.position.x > slime.position.x)
            {
                slime.velocity = new Vector2(-1 * 5 * kn, slime.velocity.y);
            }
            if (personagem.position.x < slime.position.x)
            {
                slime.velocity = new Vector2(1 * 5 * kn, slime.velocity.y);
            }
            PlaySingle (danoS,1);
            vida -= dano.dano;
        }
     
        
    }
    


	void controleAnimation(){
        if (chao == false)
        {
            anim.SetBool("pulando", true);
        }
        else
        {
            anim.SetBool("pulando", false);
        }
	}
    void morrer()
    {

        if (!morreu)
        {
            if (vida <= 0)
            {
                morreu = true;
                PlaySingle(morre, 1);
                ChecaIni checa = checaini.GetComponent<ChecaIni>();
                checa.morreuUm();
                anim.SetBool("morrendo",true);
                drop();
                Destroy(this.gameObject, 0.5f);
            }
        }
    }
    public void drop()
    {
        int i = Random.Range(0, 100);
        if (i < 20)
        {
            Vector3 pos = new Vector3(slime.transform.position.x, slime.transform.position.y+1,-1);
            Instantiate(drop1, pos, Quaternion.identity);
        }
        else
        {
            Vector3 pos = new Vector3(slime.transform.position.x, slime.transform.position.y + 1,-1);
            Instantiate(drop2, pos, Quaternion.identity);
        }
    }
    public void PlaySingle(AudioClip clip, float aux)
    {
        sfxSource.clip = clip;
        sfxSource.pitch = aux;
        sfxSource.Play();
    }
}
