using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Transform do jogador
    [SerializeField] private Transform turret; // Transform da turret
    [SerializeField] private Transform currentTarget; // Alvo atual que a câmera deve seguir
    [SerializeField] private bool inTurret; // Indica se o jogador está controlando a turret

    // Limites da câmera em X e Y
    [SerializeField] public float minX, maxX;
    [SerializeField] public float minY, maxY;

    private void Start()
    {
        currentTarget = player; // A câmera começa seguindo o jogador
    }

    private void FixedUpdate()
    {
        // Atualiza o alvo atual com base no estado (se o jogador está na turret ou não)
        if (inTurret)
        {
            currentTarget = turret; // A câmera segue a turret
        }
        else
        {
            currentTarget = player; // A câmera segue o jogador
        }

        // Pega a posição desejada da câmera (usando Lerp para suavizar)
        Vector3 desiredPosition = Vector2.Lerp(transform.position, currentTarget.position, 0.1f);

        // Limita a posição desejada dentro dos limites
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Aplica a posição limitada à câmera
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // Função para ativar a câmera na turret
    public void EnterTurret()
    {
        inTurret = true; // Ativa o controle da turret
    }

    // Função para voltar a seguir o jogador
    public void ExitTurret()
    {
        inTurret = false; // Volta a seguir o jogador
    }
}
