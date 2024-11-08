using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> levelChunks;
    public Transform playerTransform;
    private List<GameObject> spawnedChunks = new List<GameObject>();
    public float chunkSize = 20f;
    private int chunksAhead = 3;
    private int lastChunkIndex = -1;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "EndlessScene")
        {
            for (int i = 0; i < chunksAhead; i++)
            {
                SpawnChunk();
            }
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "EndlessScene")
        {
            if (playerTransform.position.z > spawnedChunks[1].transform.position.z + chunkSize / 2f)
            {
                DespawnChunk();
                SpawnChunk();
            }
        }
    }

    void SpawnChunk()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, levelChunks.Count);
        } while (randomIndex == lastChunkIndex && levelChunks.Count > 1);

        lastChunkIndex = randomIndex;

        GameObject newChunk = Instantiate(levelChunks[randomIndex], transform);

        if (spawnedChunks.Count > 0)
        {
            Vector3 lastChunkPos = spawnedChunks[spawnedChunks.Count - 1].transform.position;
            newChunk.transform.position = lastChunkPos + new Vector3(0f, 0f, chunkSize);
        }
        else
        {
            newChunk.transform.position = Vector3.zero;
        }

        spawnedChunks.Add(newChunk);
    }

    void DespawnChunk()
    {
        Destroy(spawnedChunks[0]);
        spawnedChunks.RemoveAt(0);
    }
}
