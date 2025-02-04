using UnityEngine;

public class CharacterBootstrap : MonoBehaviour
{
    [SerializeField] private CharacterInputController _characterInputController;
    [SerializeField] private ShooterInputController _shooterInputController;
    [SerializeField] private CharacterHealth _characterHealth;

    public void Init()
    {
        _characterHealth.Init();
        _characterInputController.Init();
        _shooterInputController.Init();
    }
}

