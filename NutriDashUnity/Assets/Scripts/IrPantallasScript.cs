using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicialScript : MonoBehaviour
{
    public void Jugar()
    {
        // Carga la escena con el índice especificado
        //El indice se maneja en el build settings de unity
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
        // Muestra un mensaje en la consola de Unity
        Debug.Log("Salir........");
        // Cierra la aplicación
        Application.Quit();
    }
}
