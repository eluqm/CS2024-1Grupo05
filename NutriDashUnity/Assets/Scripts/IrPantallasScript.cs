using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialScript : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void irMenuInicial()
    {
        SceneManager.LoadScene(0);
    }

    public void irIniciarSesion()
    {
        SceneManager.LoadScene(2);
    }

    public void irRegistro()
    {
        SceneManager.LoadScene(3);
    }

    public void Salir()
    {
        Debug.Log("Salir........");
        Application.Quit();
    }
}