using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.CompilerServices;

public class Menu : MonoBehaviour
{
    [Header("Sons")]
    [SerializeField] AudioSource playSound;
    [SerializeField] AudioSource creditsSound;
    [SerializeField] AudioSource quitSound;
    [SerializeField] AudioSource musica;
    [SerializeField] Animator musicaani;
    [Space]
    [SerializeField] private GameObject preto;
    [SerializeField] private Animator animatorPainel;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelCreditos;
    [Space]

    [Header("Enfeites")]
    [SerializeField] private Animator fundoConfigAni;
    [SerializeField] private Animator enfeites;
    [Space]
    [Header("Botoes")]

    [SerializeField] private Button playButton;
    [SerializeField] private GameObject playButtonGO;
    [SerializeField] private Animator playButtonAni;

    [SerializeField] private Button voltarButton;
    [SerializeField] private Animator voltarButtonAni;

    [SerializeField] private Button configButton;
    [SerializeField] private GameObject configButtonGO;
    [SerializeField] private Animator configButtonAni;

    [SerializeField] private GameObject quitButtonGO;
    [SerializeField] private Animator quitButtonAni;

    [SerializeField] private bool pressedBool = false;
    



    private void Start()
    {
        StartCoroutine("Spawn");
        Cursor.visible = false;
        
        preto.SetActive(false);
         
    }




    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && pressedBool == false)
        {
            playButton.Select();
        }
        else if (Input.GetMouseButtonDown(0) && pressedBool == true)
        {
            voltarButton.Select();
        }
        configButtonAni.SetBool("PressedBool", pressedBool);



    }
    public void Jogar()
    {
        musicaani.SetTrigger("Abaixar");
        playSound.Play();
        StartCoroutine("Play");
    }

    public void AbrirCreditos()
    {
        pressedBool = true;
        EventSystem.current.SetSelectedGameObject(null);
        voltarButton.Select();
        creditsSound.Play();
        playButtonGO.SetActive(false);
        quitButtonGO.SetActive(false);
        painelCreditos.SetActive(true);

        //StartCoroutine("Abrircreditos");
    }

    public void FecharCreditos()
    {
        StartCoroutine("fecharCreditos");
      
    }

    public void SairJogo()
    {
        quitSound.Play();
        Debug.Log("Saiu");
        Application.Quit();
    }


    IEnumerator Spawn()
    {
        playButtonAni.Play("PlaySpawn");
        configButtonAni.Play("ConfigSpawn");
        quitButtonAni.Play("QuitSpawn");
        yield return new WaitForSeconds(1f);
        playButton.Select();
    }

    IEnumerator Play()
    {
        preto.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("CutScene");
    }



    IEnumerator fecharCreditos()
    {
        voltarButtonAni.Play("Despawn");
        enfeites.Play("Enfeites saindo");
        pressedBool = false;
        fundoConfigAni.Play("FundoConfigVolta");
        configButton.Select();
        yield return new WaitForSeconds(1f);
      
        
        playButtonGO.SetActive(true);
        quitButtonGO.SetActive(true);
        painelCreditos.SetActive(false);
    }

}
