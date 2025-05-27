using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float totalTime; // Tempo total em segundos
    [SerializeField] private TextMeshProUGUI timerText; // Use TextMeshProUGUI ao invés de Text
    [SerializeField] private bool isPaused;
    [SerializeField] private GameObject goToText;


    [SerializeField] private float timeRemaining;

    private void Start()
    {
        timeRemaining = totalTime; // Inicializa o tempo restante
        UpdateTimerText();
    }

    private void FixedUpdate()
    {
        
    }
    private void Update()
    {
        if (timeRemaining <= 250 && timeRemaining >= 249)
        {
            StartCoroutine("goTo");
        }
        if (!isPaused && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            timeRemaining = 0;
            TimeIsUp();
            goToText.SetActive(false);
        }
    }
    IEnumerator goTo()
    {
        goToText.SetActive(true);
        yield return new WaitForSeconds(10f);
        goToText.SetActive(false);
    }

    private void UpdateTimerText()
    {
        // Calcula minutos e segundos restantes
        int minutes = Mathf.FloorToInt(timeRemaining / 60); // Obtém o número de minutos
        int seconds = Mathf.FloorToInt(timeRemaining % 60); // Obtém o número de segundos restantes

        // Formata a string para mostrar os minutos e segundos
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimeIsUp()
    {
        SceneManager.LoadScene("CutSceneFinal");
    }
}
