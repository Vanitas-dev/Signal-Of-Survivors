using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float turretActiveHPThreshold; // Vida m�nima da torre para habilitar a turret
    [SerializeField] private float usageTime; // Tempo que o jogador pode usar a turret
    [SerializeField] private float cooldownTime; // Tempo de cooldown antes de poder usar novamente
    [SerializeField] private float shootCooldown = 0.5f; // Tempo de cooldown entre os tiros
    [SerializeField] private GameObject projectilePrefab; // Prefab do proj�til para atirar
    [SerializeField] private Transform firePoint; // Ponto de disparo do proj�til

    private float timeSinceLastShot = 0f; // Tempo desde o �ltimo tiro
    [SerializeField] private bool isActive; // Se a turret est� dispon�vel para uso
    [SerializeField] private bool isPlayerInControl; // Se o jogador est� controlando a turret
    [SerializeField] private bool isInCooldown; // Se a turret est� em cooldown

    private float timeLeftInUse; // Tempo restante de uso
    private float timeLeftInCooldown; // Tempo restante de cooldown

    [SerializeField] private TowerHP towerHP; // Refer�ncia ao HP da torre principal
    [SerializeField] private GameObject Player; // Refer�ncia ao jogador
    [SerializeField] private Player player;
    [SerializeField] private Animator animatorTurret;
    [SerializeField] private bool isTurretUp;
    [SerializeField] private bool isTurretDown;
    [SerializeField] private bool isTurretRight;

    private Vector2 shootDirection; // Vari�vel para armazenar a dire��o do tiro

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
            TurretControl(); // Chama o m�todo para controlar a movimenta��o e o tiro da turret

            timeLeftInUse -= Time.deltaTime;
            if (timeLeftInUse <= 0)
            {
                ExitTurret(); // Expulsa o jogador ap�s o tempo de uso
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

        // Atualiza o tempo desde o �ltimo tiro
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

        // S� permite o disparo se o cooldown entre tiros tiver acabado
        if (Input.GetKey(KeyCode.Space) && timeSinceLastShot >= shootCooldown)
        {
            Shoot(); // Fun��o de disparo
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

    // Fun��o para detectar a entrada do jogador na �rea da turret
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Objeto entrou no Trigger: " + other.name); // Verificar se algum objeto est� entrando no trigger
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

    // Fun��o chamada quando o jogador entra na �rea da turret
    public void EnterTurret()
    {
        
        if (isActive && !isInCooldown && Input.GetKeyDown(KeyCode.E))
        {
            isPlayerInControl = true;
            timeLeftInUse = usageTime;
            Debug.Log("O jogador est� controlando a turret.");
            player.enabled = false;
            isActive = false;
            //change camera;
        }
    }

    // Fun��o chamada quando o jogador sai da turret ou o tempo acaba
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
