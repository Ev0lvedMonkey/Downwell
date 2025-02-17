using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleСontainerController : MonoBehaviour
{
    [SerializeField] private ObstacleСontainerConfig _config;

    private Vector3 _currentSpawnPosition;

    private void Awake()
    {
        GenerateLevel(10);
    }

    public void GenerateLevel(int levelLength)
    {
        List<ObstacleContainer> levelContainers = new();
        _currentSpawnPosition = Vector3.zero;

        ObstacleContainer emptyContainer = SpawnContainer(ContainerType.Empty);
        levelContainers.Add(emptyContainer);

        List<ObstacleContainer> availableContainers = _config.AllContainers
            .Where(c => c.Type != ContainerType.Empty && c.Type != ContainerType.Finish)
            .ToList();

        ObstacleContainer treasureContainer = availableContainers.FirstOrDefault(c => c.Type == ContainerType.Treasure);
        if (treasureContainer == null)
        {
            Debug.LogError("No Treasure container found in the configuration!");
            return;
        }
        availableContainers.Remove(treasureContainer);

        int treasureIndex = UnityEngine.Random.Range(1, levelLength - 1);
        for (int i = 1; i < levelLength - 1; i++)
        {
            ObstacleContainer selectedContainer = i == treasureIndex
                ? treasureContainer
                : availableContainers[UnityEngine.Random.Range(0, availableContainers.Count)];

            levelContainers.Add(SpawnContainer(selectedContainer));
        }

        ObstacleContainer finishContainer = SpawnContainer(ContainerType.Finish);
        levelContainers.Add(finishContainer);
    }

    private ObstacleContainer SpawnContainer(ContainerType type)
    {
        ObstacleContainer containerPrefab = _config.AllContainers.FirstOrDefault(c => c.Type == type);
        if (containerPrefab == null)
        {
            Debug.LogError($"Container of type {type} not found in the configuration!");
            return null;
        }
        return SpawnContainer(containerPrefab);
    }

    private ObstacleContainer SpawnContainer(ObstacleContainer containerPrefab)
    {
        ObstacleContainer newContainer = Instantiate(containerPrefab, _currentSpawnPosition, Quaternion.identity, transform);
        _currentSpawnPosition.y -= _config.SpawnStep;
        return newContainer;
    }
}
