using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene2 : MonoBehaviour
{
    [SerializeField] public float speedToMove;
    [SerializeField] public float xOffset;
    [SerializeField] GameObject preto;
    [SerializeField] public float tempo;

    //[SerializeField] TimerScript timer;

    void Start()
    {
       
  
    }


    void Update()
    {
       

        if (transform.position.x < -xOffset)
        {
           speedToMove = -speedToMove;
        }
        tempo = tempo - Time.deltaTime;

        if (tempo > 0)
        {
            transform.position += Vector3.left * speedToMove * Time.deltaTime;
        }
        if (tempo < 0)
        {
            speedToMove = 0;
        }

    }

  
}
