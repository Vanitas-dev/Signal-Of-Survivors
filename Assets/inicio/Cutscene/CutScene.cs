using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    [SerializeField] private Animator cutScene;
    [SerializeField] private GameObject radioText;
    [SerializeField] private Animator radioani;

    [SerializeField] private GameObject pretoGO;
    [SerializeField] private Animator preto;

    void Start()
    {
        
        StartCoroutine("Tempo");
        pretoGO.SetActive(true);
    }

    void Update()
    {
        
    }

    IEnumerator Tempo()
    {
        yield return new WaitForSeconds(4f);
        cutScene.Play("Inicio");
        yield return new WaitForSeconds(4f);
        radioani.Play("RadioDialogo");
        radioText.SetActive(true);
        yield return new WaitForSeconds(40f);
        radioani.Play("RadioDesligando");
        preto.Play("PretoAparece");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Fase1");
    }
}
