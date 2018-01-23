
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magiaPersegue : MonoBehaviour {

    public Vector3 target;
    public float moveSpeed;
    public float rotationSpeed;
    public GameObject PC;
    public int dano;
    public Rigidbody2D rbd;
    public bool encontrouAlvo;
    public Animator anim;
    // Use this for initialization
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PC = GameObject.FindGameObjectWithTag("Player");
        encontrouAlvo = false;
        encontraAlvo();
    }

    // Update is called once per frame
    void Update()
    {
        persegue();
        explode();
    }
    void persegue()
    {
        Vector3 zero = Vector3.zero;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion qt = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, qt, Time.deltaTime * rotationSpeed);
        if(vectorToTarget==zero)
        {
            encontrouAlvo = true;
        }

    }
    
    void encontraAlvo()
    {
        
        if (encontrouAlvo==false)
        {
            target = PC.transform.position;
            target.y += 2;
        }
        
    }
    void explode()
    {
        if (encontrouAlvo==true)
        {
            anim.SetTrigger("explode");
            Destroy(this.gameObject, 0.6f);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        cacadoraScript script = PC.GetComponent<cacadoraScript>();
        if (col.tag.Equals("player_col") && script.invencibilidade <= 0)
        {


            // Rigidbody2D rbd = PC.GetComponent<Rigidbody2D>();
            if (rbd.transform.position.x < col.transform.position.x)
            {
                Debug.Log("ENCOSTOU DIREITA");
                script.knockBDIR();

            }
            else if (rbd.transform.position.x > col.transform.position.x)
            {
                Debug.Log("ENCOSTOU ESQUERDA");
                script.knockBESQ();
            }
            script.knockCont = script.knockL;
            levaDano(1);
            Destroy(this.gameObject);
        }

    }
    public void levaDano(int x)
    {

        cacadoraScript script = PC.GetComponent<cacadoraScript>();
        script.vida -= dano;
        script.invencibilidade = 1;

    }
}
