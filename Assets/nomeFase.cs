using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nomeFase : MonoBehaviour {

    // Use this for initialization
    public Transform gatilho;
    public LayerMask layerPlat;
    public GameObject nome;
    private bool ativou;
    public float tempo;

    void Start () {
        ativou = false;
        nome.transform.localScale = new Vector2(0, 0);
    }
    private void Update()
    {
        StartCoroutine(ApareceNome());
    }
    IEnumerator ApareceNome ()
    {
        Collider2D toque;
        toque = Physics2D.OverlapCircle(gatilho.position, 0.1f, layerPlat);
        if (toque && !ativou)
        {
            ativou = true;
            nome.transform.localScale = new Vector2(1, 1);
            yield return new WaitForSeconds(tempo);
            nome.transform.localScale = new Vector2(0, 0);
        }
    }
}
