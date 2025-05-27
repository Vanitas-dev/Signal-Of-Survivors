using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneFinal : MonoBehaviour
{
    [SerializeField] public float speedToMove;
    [SerializeField] public float xOffset;
    [SerializeField] public float tempo;

    //[SerializeField] TimerScript timer;

    void Start()
    {


    }


    void Update()
    {


        if (transform.position.x > xOffset)
        {
            speedToMove = speedToMove = 0;
        }
        tempo = tempo - Time.deltaTime;

        if (tempo < 0)
        {
            transform.position += Vector3.left * speedToMove * Time.deltaTime;
        }
        

    }
}
