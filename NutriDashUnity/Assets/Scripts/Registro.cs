using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class Registro : MonoBehaviour
{
    // Referencia al campo de entrada de texto para el nombre de usuario
    [SerializeField] private TMP_InputField m_txtUusario = null;
    // Referencia al campo de entrada de texto para la contraseña
    [SerializeField] private TMP_InputField m_txtContrasena = null;
    // Referencia al botón de registro
    [SerializeField] private Button m_btnRegistrar = null; 
    public void InvocarRegistro()
    {
    // Inicia la coroutine para el proceso de registro
        StartCoroutine(Registrar());
    }

    IEnumerator Registrar()
    {
        if (m_txtUusario.text != "" && m_txtContrasena.text != "")
        {
            WWWForm form = new WWWForm();
            form.AddField("nombre", m_txtUusario.text);
            form.AddField("contrasena", m_txtContrasena.text);

            using UnityWebRequest w = UnityWebRequest.Post("http://localhost/nutridashphp/crearUsuario.php", form);
            yield return w.SendWebRequest();

            if (w.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(w.error);
            }
            else
            {
                // Aquí es donde verificas la respuesta del servidor
                string response = w.downloadHandler.text;
                if (response.Contains("exito")) // Asumiendo que tu servidor devuelve "exito" en caso de éxito
                {
                    Debug.Log("La conexión a la base de datos fue exitosa.");
                }
                else
                {
                    Debug.Log("Hubo un error al conectar con la base de datos: " + response);
                }
            }
        }
        else
        {
            Debug.Log("Los cuadros de texto no deben estar vacios.");
        }
    }


}
