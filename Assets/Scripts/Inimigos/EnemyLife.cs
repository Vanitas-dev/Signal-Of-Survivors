using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int maxHP; // Vida máxima do inimigo
    [SerializeField] public int currentHP;   // Vida atual do inimigo
    [SerializeField] private GameObject deathEffect; // Efeito visual quando o inimigo é destruído

    private void Start()
    {
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

    public void Die()
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }
}
