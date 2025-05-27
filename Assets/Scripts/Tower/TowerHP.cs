using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHP : MonoBehaviour
{
    [SerializeField] public int maxHP; // Vida máxima
    [SerializeField] public int currentHP; // Vida atual
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        currentHP = maxHP; // Inicializa a vida atual como a vida máxima
        Debug.Log("Torre iniciada com " + currentHP + " de vida.");
        if (healthBar != null)
        {
            healthBar.tower = this; // Passa a referência da torre para a barra de saúde
        }
    }

    public void TakeDamage(int damageAmount) // Método para receber dano
    {
        currentHP -= damageAmount; // Reduz a vida atual
        Debug.Log("Torre recebeu " + damageAmount + " de dano. Vida atual: " + currentHP);

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(); // Atualiza a barra de saúde quando a torre leva dano
        }

        if (currentHP <= 0)
        {
            DestroyTower();
        }
    }

    public void DestroyTower()
    {
        Debug.Log("A torre foi destruída!");

        // Aqui chamamos o evento que destrói inimigos e carrega a próxima cena
        GameManager.instance.OnTowerDestroyed();

        // adicionar efeitos / animação / evento
        Destroy(gameObject);
    }

    public void Heal(int healAmount)
    {
        currentHP += healAmount;

        // Garante que a vida da torre não exceda o valor máximo
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        Debug.Log("Torre curada em " + healAmount + ". Vida atual: " + currentHP);
    }
}
