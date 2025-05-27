using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Final : MonoBehaviour
{
    [SerializeField] public float speedToMove;
    [SerializeField] public float xOffset;
    [SerializeField] public float tempo;
    [SerializeField] private Animator aniCutScene;
    [SerializeField] private Animator anipreto;
    [SerializeField] private GameObject textYouWin; 
    void Start()
    {
        textYouWin.SetActive(false);
        StartCoroutine("final");
    }

    private void Update()
    {

        if (transform.position.x < xOffset)
        {
            speedToMove = 0;
        }
        tempo = tempo - Time.deltaTime;

        if (tempo > 0)
        {
            transform.position += Vector3.left * speedToMove * Time.deltaTime;
        }
      
    }


    IEnumerator final()
    {
       
        yield return new WaitForSeconds(2f);
        aniCutScene.Play("Foi");
        yield return new WaitForSeconds(4f);
        anipreto.Play("Aparece");
        yield return new WaitForSeconds(1f);
        textYouWin.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("TesteLogo");
    }
}
