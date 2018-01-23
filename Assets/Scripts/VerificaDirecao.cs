using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaDirecao : MonoBehaviour {
	private Rigidbody2D rat;
	public Transform personagem;
	// Use this for initialization
	void Start () {
		rat= GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (personagem.position.x > rat.position.x) {
			//rat.transform.localScale = new Vector2 (1, 1);
			Vector2 temp = rat.transform.localScale;
			temp = new Vector2 (-2,2);
			rat.transform.localScale = temp;
		} else {
			Vector2 temp = rat.transform.localScale;
			temp = new Vector2(2,2);
			rat.transform.localScale = temp;
		}
	}
}
