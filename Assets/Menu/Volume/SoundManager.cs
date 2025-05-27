using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] private float changeAmount = 0.1f; // A quantidade de alteração para cada tecla pressionada

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    // Método que será chamado quando o slider for alterado manualmente (pelo mouse ou teclado)
    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    // Atualiza o slider baseado no valor salvo
    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    // Salva o valor atual do slider
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

    void Update()
    {
        // Verifica se as setas direita ou esquerda são pressionadas
        if (Input.GetKeyDown(KeyCode.P))
        {
            volumeSlider.value += changeAmount; // Aumenta o valor do slider
            ChangeVolume(); // Atualiza o volume e salva a configuração
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            volumeSlider.value -= changeAmount; // Diminui o valor do slider
            ChangeVolume(); // Atualiza o volume e salva a configuração
        }

        // Certifica-se de que o valor do slider não ultrapasse os limites
        volumeSlider.value = Mathf.Clamp(volumeSlider.value, 0f, 1f);
    }
}