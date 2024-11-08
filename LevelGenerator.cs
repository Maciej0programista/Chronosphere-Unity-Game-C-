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


    public List<GameObject> movingPlatformChunks;
    public List<GameObject> rotatingObstacleChunks;
    public List<GameObject> gapJumpChunks;
    private int lastChunkType = -1; // Zmienna do śledzenia ostatniego typu chunka





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




        List<GameObject> availableChunks = new List<GameObject>(levelChunks);
        int currentChunkType;


        //zapewniamy różnorodność generowanych chunków, zapobiegając powtórzeniom tych samych chunków specjalnych pod rząd.

         do
         {

              currentChunkType = Random.Range(0, 4);

          }
           while (currentChunkType == lastChunkType && availableChunks.Count >1);


          lastChunkType = currentChunkType;







           // Dodawanie chunków z mniejszą szansą na pojawienie się

           if (currentChunkType == 1 && movingPlatformChunks.Count > 0)
           {


              availableChunks.AddRange(movingPlatformChunks);

           }


         else  if (currentChunkType == 2 && rotatingObstacleChunks.Count > 0)
         {

                availableChunks.AddRange(rotatingObstacleChunks);


         }

        else  if (currentChunkType == 3 && gapJumpChunks.Count > 0)


        {


              availableChunks.AddRange(gapJumpChunks);





         }


         // Debug.Log(availableChunks.Count);
            int randomIndex = Random.Range(0, availableChunks.Count);


        GameObject newChunk = Instantiate(availableChunks[randomIndex], transform);

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
