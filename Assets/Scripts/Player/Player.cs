using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator pretoani;
    [SerializeField] private GameObject perdeuText;

    [SerializeField] private bool isAlive = true;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float xOffsetSpawner;
    [SerializeField] private float yOffsetSpawner;
    [SerializeField] private GameObject bulletSpawner;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float xSpawnerPos;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spRenderer;

    [Header("Animation Bool Var")]
    [SerializeField] private bool isIdle = true;
    [SerializeField] private bool isShoot;
    [SerializeField] private bool isCrouch;
    [SerializeField] private bool isCrouchMove;
    [SerializeField] private bool isCrouchShoot;
    [SerializeField] private bool isRunning;
    [SerializeField] private bool isRunningShoot;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Movement();
            if (isAlive && Input.GetKeyDown(KeyCode.Space) && !isCrouchMove)
            {
                isShoot = true; 
                Shooting();
            }
            PlayerAnimations();
            BulletDirection();
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = Vector2.right * speed;
            isIdle = false;
            isRunning = true;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = Vector2.left * speed; // Jogador corre
            isIdle = false;
            isRunning = true;
            
        }
        else
        {
            rb.velocity *= 0.95f;
            isIdle = true;
            isRunning = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            bulletSpawner.transform.position = new Vector2(transform.position.x + xOffsetSpawner, transform.position.y + yOffsetSpawner / 1.4f);
            if(Input.GetKey(KeyCode.LeftArrow)) bulletSpawner.transform.position = new Vector2(transform.position.x - xOffsetSpawner, transform.position.y + yOffsetSpawner / 1.4f);
            isCrouch = true;

            if (isCrouch && isIdle && isShoot) isCrouchShoot = true;
            else isCrouchShoot = false;

            if (isCrouch && isRunning) isCrouchMove = true;
            else isCrouchMove = false;
        }
        else
        {
            isCrouchMove = false;
            isCrouch = false;
         
        }

    }

    private void Shooting()
    {
        Instantiate(bullet,bulletSpawner.transform.position,Quaternion.identity);
    }

    private void BulletDirection()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            spRenderer.flipX = false;
            bulletSpawner.transform.position = new Vector2(transform.position.x + xOffsetSpawner, transform.position.y + yOffsetSpawner);
            bullet.direction = 0;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            spRenderer.flipX = true;
            bulletSpawner.transform.position = new Vector2(transform.position.x - xOffsetSpawner, transform.position.y + yOffsetSpawner);
            bullet.direction = 1;
        }
        //else if (Input.GetKey(KeyCode.UpArrow))
        //{
            //bulletSpawner.transform.position = new Vector2(transform.position.x, transform.position.y + 2f);
            //bullet.direction = 2;
        //}
        
        else
        {
            spRenderer.flipX = false;
            bulletSpawner.transform.position = new Vector2(transform.position.x + xOffsetSpawner, transform.position.y + yOffsetSpawner);
            bullet.direction = 0;
        }
        
            isShoot = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyTriggerCollider"))
        {
            StartCoroutine("GameOver");
            Debug.Log("foi");
        }
    }

    IEnumerator GamerOver()
    {
        pretoani.Play("Aparece");
        perdeuText.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Menu");
    }

    private void PlayerAnimations()
    {
        animator.SetBool("isIdle", isIdle);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isRunningShoot", isRunningShoot);
        animator.SetBool("isShoot", isShoot);
        animator.SetBool("isCrouch", isCrouch);
        animator.SetBool("isCrouchMove", isCrouchMove);
        animator.SetBool("isCrouchShoot", isCrouchShoot);
    }
}
