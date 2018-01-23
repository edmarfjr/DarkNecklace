using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaMove : MonoBehaviour {
	public int altura;
	public int largura;
	public int velocidade;
	public float contador;
	private Vector2 posInicial;

	// Use this for initialization
	void Start () {
		altura = 0;
		largura = 3;
		velocidade = 1;
		posInicial = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		contador += velocidade*Time.deltaTime;
		float x = Mathf.Sin(contador)*largura;
		float y = Mathf.Cos(contador)*altura;

		if (contador >= 2 * Mathf.PI)
			contador = 0;

		transform.position = new Vector2 (x, y)+posInicial;
	}
}
