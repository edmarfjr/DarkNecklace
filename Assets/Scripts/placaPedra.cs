using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class placaPedra : MonoBehaviour
{

    public LayerMask layerPC;
    public Collider2D toqueTele;
    public bool abriu;
    public string texto;
    public Transform obj;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        obj = GetComponent<Transform>();
    }

    void Update()
    {
        Collider2D toque;
        toque = Physics2D.OverlapCircle(this.transform.position, 2.5f, layerPC);
        anim.SetBool("aceso", toque);
        if (toque)
        {
            abriu = true;
        }
        else
        {
            abriu = false;
        }
    }

    

    void OnGUI()
    {
        if (abriu==true)
        {
            
            GUI.Label(new Rect(Screen.height/1.2f, Screen.width/3, 100, 100), texto);
        }
    }

}