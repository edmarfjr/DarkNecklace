using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PARABAINS : MonoBehaviour {
    public GameObject checaini;
    public LayerMask layerPC;
    public GameObject tele;
    public GameObject PC;
    public int Fase;
    // Use this for initialization
    void Start () {
        GameObject aux = GameObject.FindWithTag("Player");
        PC = aux;
        checaini = GameObject.FindGameObjectWithTag("checaIni");
    }
	
	// Update is called once per frame
	void Update () {
        ChecaIni checa = checaini.GetComponent<ChecaIni>();
        Collider2D toqueTele;
        toqueTele = Physics2D.OverlapCircle(tele.transform.position, 0.1f, layerPC);
        if (toqueTele && checa.boss == 0 && Fase == 1)
        {
            GameObject gStatus = GameObject.FindGameObjectWithTag("gameStatus");
            GameStatus GS = gStatus.GetComponent<GameStatus>();
            GS.saveStatus();
            SceneManager.LoadScene(2);
        }
        if (toqueTele && Fase == 2)
        {
            GameObject gStatus = GameObject.FindGameObjectWithTag("gameStatus");
            GameStatus GS = gStatus.GetComponent<GameStatus>();
            GS.saveStatus();
            SceneManager.LoadScene(3);
        }
    }
}
