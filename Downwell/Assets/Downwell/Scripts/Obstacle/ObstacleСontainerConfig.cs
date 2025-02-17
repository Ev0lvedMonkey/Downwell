using System.Collections.Generic;
using UnityEngine;

public enum ContainerType
{
    Empty,
    Finish,
    Treasure,
    Other
}

[CreateAssetMenu(fileName = "NewObstacleСontainerConfig", menuName = "ScriptableObjects/ObstacleСontainerController")]
public class ObstacleСontainerConfig : ScriptableObject
{
    [SerializeField] private List<ObstacleContainer> _allContainers = new();
    [SerializeField] private float _spawnStep = 10f;

    public float SpawnStep => _spawnStep;
    public IReadOnlyList<ObstacleContainer> AllContainers => _allContainers;

}