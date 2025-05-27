using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 dir;
    [SerializeField] public int direction;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        switch (direction)
        {
            case 0:
                dir = new Vector2(bulletSpeed, 0);
                break;
            case 1:
                dir = new Vector2(-bulletSpeed, 0);
                break;
        }

        rb.velocity = dir;

        Destroy(gameObject, 5f);
    }
}
