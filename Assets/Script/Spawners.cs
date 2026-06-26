using System.Collections;
using UnityEngine;

public class Spawners : MonoBehaviour
{

    [Header("Frutas")]
    [SerializeField] private GameObject[] fruits;

    [Header("Bomba")]
    [SerializeField] private GameObject bombPrefab;
    [Header("Configuração do Spawn")]
    [SerializeField] private float spawnDelay = 1f;

    [SerializeField] private float minX = -8f;

    [SerializeField] private float maxX = 8f;

    [SerializeField] private float spawnY = 10f;

    [Header("Probabilidades")]
    [Range(0, 100)]
    [SerializeField] private int bombChance = 10;

    private bool canSpawn = false;

    private float lastSpawnX;

    private Coroutine spawnRoutine;

    private void Start()
    {
        Debug.Log("Spawner carregado.");
    }

    public void StartSpawn()
    {
        if (spawnRoutine != null)
            return;

        Debug.Log("Spawn iniciado.");

        canSpawn = true;

        spawnRoutine =
            StartCoroutine(SpawnRoutine());
    }

    public void StopSpawn()
    {
        canSpawn = false;

        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);

            spawnRoutine = null;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnItem();

            yield return new WaitForSeconds(
                spawnDelay
            );
        }
    }
private void SpawnItem()
{
    if (fruits == null || fruits.Length == 0)
    {
        Debug.LogError("Nenhuma fruta atribuída ao Spawner.");
        return;
    }

    if (bombPrefab == null)
    {
        Debug.LogError("Nenhuma bomba atribuída ao Spawner.");
        return;
    }

    GameObject prefabToSpawn;

    int randomChance = Random.Range(0, 100);

    // Decide se nasce bomba ou fruta
    if (randomChance < bombChance)
    {
        prefabToSpawn = bombPrefab;
    }
    else
    {
        prefabToSpawn =
            fruits[
                Random.Range(
                    0,
                    fruits.Length
                )
            ];
    }

    float randomX;

    do
    {
        randomX =
            Random.Range(
                minX,
                maxX
            );

    } while (
        Mathf.Abs(
            randomX -
            lastSpawnX
        ) < 2f
    );

    lastSpawnX = randomX;

    Vector3 spawnPosition =
        new Vector3(
            randomX,
            spawnY,
            0f
        );

    Instantiate(
        prefabToSpawn,
        spawnPosition,
        Quaternion.identity
    );
}
}