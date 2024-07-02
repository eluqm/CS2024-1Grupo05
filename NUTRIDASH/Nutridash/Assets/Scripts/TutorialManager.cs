using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class TutorialStep
{
    public string message;
    public Sprite image;
    public TextMeshProUGUI textComponent; // Referencia al componente TextMeshPro específico para este paso
}

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject fruitInfoPanel; // Referencia al panel de información de frutas
    public List<TutorialStep> tutorialSteps;
    public float messageDisplayTime = 5f; // Tiempo que se muestra cada mensaje

    private int currentStepIndex = 0;

    void Start()
    {
        tutorialPanel.SetActive(true);
        fruitInfoPanel.SetActive(false); // Asegurarse de que el panel de información de frutas esté oculto al inicio
        StartCoroutine(DisplayTutorialSteps());
    }

    private IEnumerator DisplayTutorialSteps()
    {
        while (currentStepIndex < tutorialSteps.Count)
        {
            TutorialStep step = tutorialSteps[currentStepIndex];
            step.textComponent.text = step.message;
            step.textComponent.gameObject.SetActive(true); // Activar el componente de texto específico
            tutorialPanel.GetComponent<Image>().sprite = step.image; // Asignar la imagen al panel

            yield return new WaitForSeconds(messageDisplayTime);

            step.textComponent.gameObject.SetActive(false); // Desactivar el componente de texto específico
            currentStepIndex++;
        }

        // Transición al panel de información de frutas
        tutorialPanel.SetActive(false);
        fruitInfoPanel.SetActive(true);

        // Esperar 5 segundos antes de ocultar el FruitInfoPanel
        yield return new WaitForSeconds(messageDisplayTime);
        fruitInfoPanel.SetActive(false);
    }
}
