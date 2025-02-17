using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObstacleContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _containerNameText;
    [SerializeField] private string _containerName;
    [SerializeField] private ContainerType _type;
    [SerializeField] private List<ObstacleContainer> _possibleNextContainers = new();

    public string ContainerName => _containerName;
    public ContainerType Type => _type;
    public IReadOnlyList<ObstacleContainer> PossibleNextContainers => _possibleNextContainers;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _containerNameText.text = _containerName;
    }
}