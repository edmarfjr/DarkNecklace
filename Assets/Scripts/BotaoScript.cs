using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoScript : MonoBehaviour {

	public void iniciar(){
		SceneManager.LoadScene (1);
	}
	public void Sair(){
		Application.Quit();
	}
}
