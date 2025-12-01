using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    [Header("Fish Prefab")]
    [SerializeField] private FishController fishPrefab;

    [Header("Spawn Area")]
    [SerializeField] private float surfaceY = 0f;
    [SerializeField] private float maxDepthDistance = 20f;
    [SerializeField] private float horizontalSpawnRange = 8f;

    [Header("Spawn Settings")]
    [SerializeField] private int fishCount = 30;
    [SerializeField] private float maxValueMultiplier = 4f;

    private List<FishController> activeFish = new List<FishController>();

    private void Start()
    {
        SpawnFishField();
    }

    public void SpawnFishField()
    {
        ClearExistingFish();

        for (int i = 0; i < fishCount; i++)
        {
            SpawnSingleFish();
        }
    }

    private void ClearExistingFish()
    {
        if (activeFish.Count > 0)
        {
            for (int i = 0; i < activeFish.Count; i++)
            {
                if (activeFish[i] != null)
                {
                    Destroy(activeFish[i].gameObject);
                }
            }
            activeFish.Clear();
        }
    }

    private void SpawnSingleFish()
    {
        float depthDistance = Random.Range(0f, maxDepthDistance);
        float yPosition = surfaceY - depthDistance;
        float xPosition = Random.Range(-horizontalSpawnRange, horizontalSpawnRange);
        Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0f);

        FishController newFish = Instantiate(fishPrefab, spawnPosition, Quaternion.identity, transform);
        ApplyValueMultiplier(newFish, depthDistance);
        activeFish.Add(newFish);
    }

    private void ApplyValueMultiplier(FishController fish, float depthDistance)
    {
        if (maxDepthDistance <= 0f)
        {
            fish.SetValueMultiplier(1f);
            return;
        }

        float depthPercent = depthDistance / maxDepthDistance;

        if (depthPercent < 0f)
        {
            depthPercent = 0f;
        }
        else if (depthPercent > 1f)
        {
            depthPercent = 1f;
        }

        float multiplier = Mathf.Lerp(1f, maxValueMultiplier, depthPercent);
        fish.SetValueMultiplier(multiplier);
    }
}
