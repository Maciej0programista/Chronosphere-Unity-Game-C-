using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.5f;
    public float rewindDuration = 5f;
    public float maxTimeEnergy = 100f;
    public float timeEnergyRegenRate = 10f;
    public float timeEnergyCostSlowdown = 5f;
    public float timeEnergyCostRewind = 50f;
    private List<GameState> snapshots = new List<GameState>();
    private float timeElapsedSinceSnapshot = 0f;
    private float snapshotInterval = 0.1f;
    private bool isRewinding = false;
    private bool isSlowedDown = false;
    private float currentTimeEnergy;

    void Start()
    {
        currentTimeEnergy = maxTimeEnergy;
    }

    void Update()
    {
        if (!isRewinding && !isSlowedDown)
        {
            currentTimeEnergy += timeEnergyRegenRate * Time.deltaTime;
            currentTimeEnergy = Mathf.Clamp(currentTimeEnergy, 0, maxTimeEnergy);
        }

        if (!isRewinding)
        {
            timeElapsedSinceSnapshot += Time.deltaTime;
            if (timeElapsedSinceSnapshot >= snapshotInterval)
            {
                snapshots.Add(new GameState(transform));
                timeElapsedSinceSnapshot = 0;
                if (snapshots.Count > rewindDuration / snapshotInterval)
                {
                    snapshots.RemoveAt(0);
                }
            }
        }

        if (Input.GetKey(KeyCode.T) && currentTimeEnergy > 0)
        {
            Time.timeScale = slowdownFactor;
            currentTimeEnergy -= timeEnergyCostSlowdown * Time.deltaTime;
            isSlowedDown = true;
        }
        else
        {
            Time.timeScale = 1f;
            isSlowedDown = false;
        }

        if (Input.GetKeyDown(KeyCode.R) && snapshots.Count > 0 && currentTimeEnergy >= timeEnergyCostRewind)
        {
            StartCoroutine(RewindTime());
            currentTimeEnergy -= timeEnergyCostRewind;
        }
    }

    System.Collections.IEnumerator RewindTime()
    {
        isRewinding = true;

        for (int i = snapshots.Count - 1; i >= 0; i--)
        {
            transform.position = snapshots[i].position;
            yield return new WaitForSeconds(snapshotInterval);
        }

        snapshots.Clear();
        isRewinding = false;
    }

    public float GetCurrentTimeEnergy()
    {
        return currentTimeEnergy;
    }
}

public struct GameState
{
    public Vector3 position;

    public GameState(Transform transform)
    {
        position = transform.position;
    }
}
