using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    public Transform highTarget;
    public Transform downTarget;
    public Transform highTargetCheck;
    public Transform downTargetCheck;

    private bool isTowerDestroyed = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Verifica se a torre foi destruída
        if (isTowerDestroyed)
        {
            return; // Se a torre foi destruída, não atualiza mais o target
        }

        // Verifica se o target ainda existe
        if (highTargetCheck == null || downTarget == null)
        {
            isTowerDestroyed = true;
        }
    }

    // Função para ser chamada quando a torre for destruída
    public void OnTowerDestroyed()
    {
        isTowerDestroyed = true;
        // Opcional: Remova referências aos alvos destruídos
        downTarget = null;
        highTarget = null;
    }
}
