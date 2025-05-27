using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    [SerializeField] private GameObject textoEnter;

    void Start()
    {
        textoEnter.SetActive(false);
        StartCoroutine("Inicio");
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    IEnumerator Inicio()
    {
        yield return new WaitForSeconds(3);
        textoEnter.SetActive(true);
    }
}
