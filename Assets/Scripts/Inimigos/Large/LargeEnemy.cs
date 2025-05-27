using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemy : MonoBehaviour
{
    [SerializeField] public float speed; // Velocidade do inimigo
    [SerializeField] public float attackRange; // Distância em que o inimigo começa a atacar
    [SerializeField] public int attackDamage; // Dano do ataque
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isAttacking; // Flag para verificar se está atacando
    [SerializeField] private bool isGrounded; // Flag para verificar se o inimigo está no chão
    [SerializeField] private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // Verifica se o downTarget ainda existe antes de acessá-lo
        if (TargetManager.instance.downTarget != null)
        {
            // Calcular a direção em relação à torre
            Vector2 direction = new Vector2((TargetManager.instance.downTarget.position.x - transform.position.x), 0).normalized;

            // Verifica a distância entre o inimigo e a torre (downTarget)
            float distanceToTower = Vector2.Distance(transform.position, TargetManager.instance.downTarget.position);

            // Se estiver dentro do alcance de ataque, para e ataca
            if (distanceToTower <= attackRange && !isAttacking && isGrounded)
            {
                isAttacking = true;
                rb.velocity = Vector2.zero; // Para o inimigo
                animator.Play("Attack");
                Attack();
            }
            else if (distanceToTower > attackRange && isGrounded)
            {
                isAttacking = false; // Reseta o estado de ataque se estiver fora do alcance
                rb.velocity = new Vector2(direction.x * speed, rb.velocity.y); // Apenas movimentação horizontal
            }
        }
    }

    void Attack()
    {
        Debug.Log("Inimigo atacou a torre! Dano causado: " + attackDamage);

        if (TargetManager.instance.downTarget != null)
        {
            TowerHP towerHealth = TargetManager.instance.downTarget.GetComponentInParent<TowerHP>();

            if (towerHealth != null)
            {
                towerHealth.TakeDamage(attackDamage);
            }
            else
            {
                Debug.LogWarning("Torre não encontrada ou não tem o componente TowerHP.");
            }
        }
        else
        {
            Debug.LogWarning("downTarget é null.");
        }

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(8f); // Tempo de espera entre os ataques
        isAttacking = false; // Permitir que o inimigo ataque novamente
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o inimigo colidiu com o chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Verifica se o inimigo saiu do chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
