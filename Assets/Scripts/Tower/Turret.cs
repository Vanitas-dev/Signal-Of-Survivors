using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float turretActiveHPThreshold; // Vida mínima da torre para habilitar a turret
    [SerializeField] private float usageTime; // Tempo que o jogador pode usar a turret
    [SerializeField] private float cooldownTime; // Tempo de cooldown antes de poder usar novamente
    [SerializeField] private float shootCooldown = 0.5f; // Tempo de cooldown entre os tiros
    [SerializeField] private GameObject projectilePrefab; // Prefab do projétil para atirar
    [SerializeField] private Transform firePoint; // Ponto de disparo do projétil

    private float timeSinceLastShot = 0f; // Tempo desde o último tiro
    [SerializeField] private bool isActive; // Se a turret está disponível para uso
    [SerializeField] private bool isPlayerInControl; // Se o jogador está controlando a turret
    [SerializeField] private bool isInCooldown; // Se a turret está em cooldown

    private float timeLeftInUse; // Tempo restante de uso
    private float timeLeftInCooldown; // Tempo restante de cooldown

    [SerializeField] private TowerHP towerHP; // Referência ao HP da torre principal
    [SerializeField] private GameObject Player; // Referência ao jogador
    [SerializeField] private Player player;
    [SerializeField] private Animator animatorTurret;
    [SerializeField] private bool isTurretUp;
    [SerializeField] private bool isTurretDown;
    [SerializeField] private bool isTurretRight;

    private Vector2 shootDirection; // Variável para armazenar a direção do tiro

    private void Start()
    {
        ActivateTurret();
    }

    private void Update()
    {
        if (isActive)
        {
            EnterTurret();
        }
        else if (!isActive)
        {
            ExitTurret();
        }

        if (isPlayerInControl)
        {
            TurretControl(); // Chama o método para controlar a movimentação e o tiro da turret

            timeLeftInUse -= Time.deltaTime;
            if (timeLeftInUse <= 0)
            {
                ExitTurret(); // Expulsa o jogador após o tempo de uso
                StartCooldown(); // Inicia o cooldown
            }
        }

        if (isInCooldown)
        {
            timeLeftInCooldown -= Time.deltaTime;
            if (timeLeftInCooldown <= 0)
            {
                isInCooldown = false; // Acaba o cooldown
            }
        }

        // Atualiza o tempo desde o último tiro
        timeSinceLastShot += Time.deltaTime;
    }

    private void TurretControl()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animatorTurret.Play("Up"); // Movimenta para cima
            isTurretUp = true;
            isTurretDown = false;
            isTurretRight = false;
            shootDirection = firePoint.up;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animatorTurret.Play("Down"); // Movimenta para baixo
            isTurretDown = true;
            isTurretUp = false;
            isTurretRight = false;
            shootDirection = -firePoint.up;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animatorTurret.Play("Base");
            isTurretRight = true;
            isTurretDown = false;
            isTurretUp = false;
            shootDirection = firePoint.up;
        }

        // Só permite o disparo se o cooldown entre tiros tiver acabado
        if (Input.GetKey(KeyCode.Space) && timeSinceLastShot >= shootCooldown)
        {
            Shoot(); // Função de disparo
            timeSinceLastShot = 0f; // Reinicia o contador do cooldown de tiro
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float angle;
            if (isTurretUp)
            {
                angle = 45f;
                float radians = angle * Mathf.Deg2Rad;
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
                rb.AddForce(shootDirection * 12f, ForceMode2D.Impulse);
            }
            else if (isTurretDown)
            {
                angle = -30f;
                float radians = angle * Mathf.Deg2Rad;
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
                rb.AddForce(shootDirection * 15f, ForceMode2D.Impulse);
            }
            else if (isTurretRight)
            {
                angle = 5f;
                float radians = angle * Mathf.Deg2Rad;
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
                rb.AddForce(shootDirection * 15f, ForceMode2D.Impulse);
            }
            else
            {
                angle = -30f;
                float radians = angle * Mathf.Deg2Rad;
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
                rb.AddForce(shootDirection * 15f, ForceMode2D.Impulse);
            }
        }

        Debug.Log("Disparo realizado pela turret.");
    }
private void ActivateTurret()
    {
        isActive = false;
    }

    private void StartCooldown()
    {
        isInCooldown = true;
        timeLeftInCooldown = cooldownTime;
        Debug.Log("A turret entrou em cooldown.");
    }

    // Função para detectar a entrada do jogador na área da turret
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Objeto entrou no Trigger: " + other.name); // Verificar se algum objeto está entrando no trigger
        if (other.CompareTag("Player") && !isActive && !isInCooldown)
        {
            Debug.Log("Press E to use");
            EnterTurret(); // Permite o jogador entrar na turret
            isActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isActive = false;
    }

    // Função chamada quando o jogador entra na área da turret
    public void EnterTurret()
    {
        
        if (isActive && !isInCooldown && Input.GetKeyDown(KeyCode.E))
        {
            isPlayerInControl = true;
            timeLeftInUse = usageTime;
            Debug.Log("O jogador está controlando a turret.");
            player.enabled = false;
            isActive = false;
            //change camera;
        }
    }

    // Função chamada quando o jogador sai da turret ou o tempo acaba
    private void ExitTurret()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInControl)
        {
            isPlayerInControl = false;
            Debug.Log("O jogador saiu da turret.");
            player.enabled = true;
            //camera normalize;
        }
    }
}
