using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform healthBarSprite; // Referência ao Transform da barra de vida (Sprite)
    [SerializeField] public TowerHP tower; // Referência ao script da torre

    private Vector3 originalScale; // Escala original da barra de saúde

    private void Start()
    {
        if (tower == null)
        {
            tower = GetComponentInParent<TowerHP>(); // Obtém a torre se não for definido no Inspector
        }

        if (healthBarSprite != null)
        {
            originalScale = healthBarSprite.localScale; // Armazena a escala original no eixo X
        }

        // Inicializa a barra de saúde
        UpdateHealthBar();
    }

    private void Update()
    {
        // Atualiza a barra de saúde sempre que a vida mudar
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // Atualiza a escala da barra de saúde com base na vida atual da torre
        if (tower != null && healthBarSprite != null)
        {
            // Calcula a porcentagem de vida
            float healthPercent = (float)tower.currentHP / tower.maxHP;

            // Atualiza a escala da barra de vida no eixo X
            healthBarSprite.localScale = new Vector3(originalScale.x * healthPercent, originalScale.y, originalScale.z);
        }
    }
}
