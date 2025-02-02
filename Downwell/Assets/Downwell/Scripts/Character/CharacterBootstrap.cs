using UnityEngine;

public class CharacterBootstrap : MonoBehaviour
{
    [SerializeField] private CharacterInputController _characterInputController;
    [SerializeField] private ShooterInputController _shooterInputController;

    public void Init()
    {
        _characterInputController.Init();
        _shooterInputController.Init();
    }
}

