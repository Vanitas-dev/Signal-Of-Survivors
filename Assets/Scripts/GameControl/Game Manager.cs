using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Transform tower;
    public UnityEvent onTowerDestroy;
    public static GameManager instance;
    public bool getOut;
    private bool isPaused;
    [SerializeField] private GameObject pause;
    [SerializeField] private Player player;

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        // Verifica se a tecla P foi pressionada
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause(); // Chama a função de pausa
        }
    }

    // Função que será chamada quando a torre for destruída
    public void OnTowerDestroyed()
    {
        onTowerDestroy.Invoke();
        DestroyAllEnemies();
        LoadNextScene();
    }
    public void OnGameOver()
    {
        DestroyAllEnemies();
        GameOver();
    }

    // Destroi todos os inimigos na cena
    void DestroyAllEnemies()
    {
        // Encontrar todos os objetos com tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy); // Destroi o inimigo
            getOut = true;
        }
    }

    // Carrega a próxima cena
    void LoadNextScene()
    {
        if (getOut)
        {
            SceneManager.LoadScene("CutSceneFinal");
            getOut = false;
        }
    }
    void GameOver()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("GameOver");
    }
    public void TogglePause()
    {
        isPaused = !isPaused; // Alterna o estado de pausa

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausa o tempo do jogo
            pause.SetActive(true);
            player.enabled = false;
        }
        else
        {
            pause.SetActive(false);
            player.enabled = true;
            Time.timeScale = 1f; // Retoma o tempo normal do jogo
        }
    }

    // Função que retorna o estado de pausa
    public bool IsPaused()
    {
        return isPaused;
    }
    public void OnPauseButtonClicked()
    {
        GameManager.instance.TogglePause(); // Chama a função de pausa
    }
}
