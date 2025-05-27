using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escada : MonoBehaviour
{
    [SerializeField] private Transform climb; // Ponto de destino no topo da escada
    [SerializeField] private Transform descend; // Ponto de destino na base da escada
    [SerializeField] private KeyCode climbKey = KeyCode.E; // Tecla para subir/descer
    private bool isPlayerOnLadder; // Verifica se o jogador está na escada
    private GameObject player;
    [SerializeField] private GameObject pressUpText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou no trigger tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            pressUpText.SetActive(true);
            isPlayerOnLadder = true;
            player = other.gameObject;
            Debug.Log("Pressione 'E' para usar a escada.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica se o jogador saiu da área da escada
        if (other.CompareTag("Player"))
        {
            pressUpText.SetActive(false);
            isPlayerOnLadder = false;
            player = null;
        }
    }

    private void Update()
    {
        // Verifica se o jogador está na escada e se a tecla para usar a escada foi pressionada
        if (isPlayerOnLadder && Input.GetKeyDown(climbKey))
        {
            if (player.transform.position.y < transform.position.y)
            {
                Teleport(climb); // Subir
            }
            else
            {
                Teleport(descend); // Descer
            }
        }
    }

    private void Teleport(Transform destination)
    {
        if (destination != null)
        {
            // Teletransporta o jogador para o ponto de destino
            player.transform.position = destination.position;
            Debug.Log("Teletransportado!");
        }
        else
        {
            Debug.LogWarning("Nenhum ponto de teletransporte foi atribuído!");
        }
    }
}
