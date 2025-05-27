using UnityEngine;

public class LargeEnemyHP : MonoBehaviour
{
    [SerializeField] private int maxHP; // Vida máxima do inimigo
    [SerializeField] private int currentHP;   // Vida atual do inimigo
    [SerializeField] private GameObject deathEffect; // Efeito visual quando o inimigo é destruído

    private void Start()
    {
        // Inicializa a vida atual como a vida máxima
        currentHP = maxHP;
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduz a vida atual
        currentHP -= damageAmount;
        Debug.Log(gameObject.name + " recebeu " + damageAmount + " de dano. Vida atual: " + currentHP);

        // Verifica se o inimigo morreu
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Executa lógica de destruição do inimigo
        Debug.Log(gameObject.name + " foi destruído!");

        // Se houver um efeito de morte, instancie-o
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Destrói o inimigo
        Destroy(gameObject);
    }
}
