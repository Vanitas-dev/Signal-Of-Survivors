using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    [SerializeField] private Transform target; // O objeto que o Canvas seguirá
    [SerializeField] private Canvas worldSpaceCanvas; // Referência ao Canvas em World Space

    private void Update()
    {
        // Atualiza a posição do Canvas para a posição do GameObject alvo
        if (worldSpaceCanvas != null && target != null)
        {
            worldSpaceCanvas.transform.position = target.position;
        }
    }
}