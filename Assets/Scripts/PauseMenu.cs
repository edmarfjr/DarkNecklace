using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public GameObject menu;
    public bool pausado;
    // Update is called once per frame
    private void Start()
    {
        pausado = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (Time.timeScale > 0)
            {
                menu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                menu.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }  

    public void botaoVoltar()
    {
        Time.timeScale = 1f;
        menu.SetActive(false);
    }
    public void botaoSair()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
