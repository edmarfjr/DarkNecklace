using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarregarCena : MonoBehaviour {
	public GameObject vet1;
	public GameObject vet2;
	public GameObject[] vetIni;
	public GameObject inicio;
	public GameObject PC;
	private float tamanhoTela;
	private float borda;
	private float limite;
	public float limiteSpawn;
	public float auxlimite;
	// Use this for initialization
	void Start () {
		tamanhoTela = Camera.main.orthographicSize;
		limite = 0;
		borda = tamanhoTela * Camera.main.aspect;
		//comecar ();

	}
	
	// Update is called once per frame
	void Update () {
		
		if(limite <=limiteSpawn){
			respawn ();
			limite += auxlimite;
		}
	}
	void comecar(){
		Instantiate(PC,inicio.transform.position,Quaternion.identity);	
	}
	void respawn(){
		float posx = Random.Range (vet1.transform.position.x,vet2.transform.position.x);
		Vector3 pos = new Vector3 (posx,vet1.transform.position.y,-1);
		int i=Random.Range(0,100);
		if (i <50) {
			Debug.Log ("DEU ZERO");
			Instantiate(vetIni[0],pos,Quaternion.identity);	
			auxlimite = 1;
			Debug.Log ("SUMONO GERAL");
		}if (i >=50) {
			Debug.Log ("DEU UM");
			Instantiate(vetIni[1],pos,Quaternion.identity);	
			auxlimite = 2;
			Debug.Log ("SUMONO GERAL");
		}

	}
}
