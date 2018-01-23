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

    void Start()
    {
        obj = GetComponent<Transform>();
    }

    void Update()
    {
        Collider2D toqueTele;
        toqueTele = Physics2D.OverlapCircle(this.transform.position, 0.5f, layerPC);
        if(toqueTele)
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