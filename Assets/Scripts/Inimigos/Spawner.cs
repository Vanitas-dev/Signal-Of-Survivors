using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [System.Serializable] // Para exibir no Inspector do Unity
    public class SpawnPoint
    {
        public Transform spawnLocation; // Local específico para spawnar o inimigo
        public GameObject enemyPrefab; // Tipo de inimigo a ser spawnado
        public float spawnDelay; // Tempo entre os spawns para este ponto específico
        public float startSpawnDelay; // Tempo antes de começar a spawnar
        [HideInInspector] public float nextSpawnTime; // Armazena o próximo tempo de spawn para este ponto

        public void Initialize()
        {
            // Define o próximo tempo de spawn considerando o tempo de início
            nextSpawnTime = Time.time + startSpawnDelay; // O spawn só acontecerá após o startDelay
        }
    }

    [SerializeField] private List<SpawnPoint> spawnPoints; // Lista de pontos de spawn
    [SerializeField] private Transform towerTransform; // Referência à posição da torre no cenário

    void Start()
    {
        // Inicializa o próximo tempo de spawn para cada ponto de spawn
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.Initialize(); // Chama o método de inicialização
        }
    }

    void Update()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            // Apenas spawnar inimigos se o tempo atual for maior ou igual ao próximo tempo de spawn
            if (Time.time >= spawnPoint.nextSpawnTime)
            {
                SpawnEnemy(spawnPoint);
                spawnPoint.nextSpawnTime = Time.time + spawnPoint.spawnDelay; // Atualiza o próximo tempo de spawn
            }
        }
    }

    void SpawnEnemy(SpawnPoint chosenPoint)
    {
        if (towerTransform != null)
        {
            // Instancia o inimigo no ponto de spawn escolhido
            GameObject newEnemy = Instantiate(chosenPoint.enemyPrefab, chosenPoint.spawnLocation.position, Quaternion.identity);

            // Calcula a direção entre o inimigo e a torre
            float directionToTower = towerTransform.position.x - chosenPoint.spawnLocation.position.x;

            // Se a direção for positiva (torre à direita), o inimigo mantém sua escala normal
            // Se a direção for negativa (torre à esquerda), o inimigo inverte a escala no eixo X
            Vector3 enemyScale = newEnemy.transform.localScale;
            if (directionToTower < 0)
            {
                // Torre à direita, mantém a escala
                enemyScale.x = Mathf.Abs(enemyScale.x); // Garante que X seja positivo
            }
            else
            {
                // Torre à esquerda, inverte a escala
                enemyScale.x = -Mathf.Abs(enemyScale.x); // Garante que X seja negativo
            }

            // Aplica a nova escala ao inimigo
            newEnemy.transform.localScale = enemyScale;
        }
    }
}