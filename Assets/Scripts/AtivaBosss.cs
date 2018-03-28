using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaBosss : MonoBehaviour {
    public GameObject boss;
    public GameObject nomeBoss;
    public int nBoss;
    public LayerMask layerPC;
    public bool ativou;
    // Use this for initialization
    void Start () {
        boss = GameObject.FindGameObjectWithTag("Boss");
        
        nomeBoss.transform.localScale = new Vector3 (0,0,0);
    }
	
	// Update is called once per frame
	void Update () {
        Collider2D toque;
        toque = Physics2D.OverlapCircle(this.transform.position, 2.5f, layerPC);
        if (toque)
        {
            if (!ativou)
            {
                StartCoroutine(ativarBoss());
            }
        }

    }
    
    IEnumerator ativarBoss ()
    {
        if (nBoss == 1)
        {
            Bearman scr = boss.gameObject.GetComponent<Bearman>();
            nomeBoss.transform.localScale = new Vector3(1, 1, 1);
            Time.timeScale = 0.0001f;
            yield return new WaitForSeconds(0.0003f);
            nomeBoss.transform.localScale = new Vector3(0, 0, 0);
            Time.timeScale = 1f;
            scr.ativo = true;
        }
        if (nBoss == 2)
        {
            golemBoss scr = boss.gameObject.GetComponent<golemBoss>();
            nomeBoss.transform.localScale = new Vector3(1, 1, 1);
            Time.timeScale = 0.0001f;
            yield return new WaitForSeconds(0.0003f);
            nomeBoss.transform.localScale = new Vector3(0, 0, 0);
            Time.timeScale = 1f;
            scr.ativo = true;
        }
        if (nBoss == 3)
        {
            RatoAmareloBoss scr = boss.gameObject.GetComponent<RatoAmareloBoss>();
            nomeBoss.transform.localScale = new Vector3(1, 1, 1);
            Time.timeScale = 0.0001f;
            yield return new WaitForSeconds(0.0003f);
            nomeBoss.transform.localScale = new Vector3(0, 0, 0);
            Time.timeScale = 1f;
            scr.ativo = true;
        }
        ativou = true;
    }
}
