using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingKeys : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener eventos de teclado
        // Si se presiona la tecla de espacio.
        if (Input.GetKeyDown(KeyCode.Space)) {
        // Imprime un mensaje en la consola.
            Debug.Log("Using Keycode. You can use this to make the player jump");
        }

        // Si se presiona el botón de salto
        if (Input.GetButtonDown("Jump")) {
            Debug.Log("Using Jump. You can use this to make the player jump");
        }

        float horizontal = Input.GetAxis("Horizontal"); //-1 to 1
        float vertical = Input.GetAxis("Vertical"); // -1 to 1

        if (horizontal < 0f || horizontal > 0f) {
            Debug.Log("Horizontal axis is " + horizontal);
        }
        if (vertical <0f || vertical > 0f) {
            Debug.Log("Vertical axis is" + vertical);
        }
    }
}
