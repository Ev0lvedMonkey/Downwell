using UnityEngine;

public class GameSceneBootstrap : MonoBehaviour
{
    [SerializeField] private CharacterBootstrap _characterBootstrap;
    [SerializeField] private ClipView _clipView;

    private StreamBus _streamBus;

    private void Awake()
    {
        _streamBus = new();
        RegistryServices();
        _clipView.Init();
        _characterBootstrap.Init();
    }

    private void RegistryServices()
    {
        ServiceLocator.Inizialize();
        ServiceLocator.Current.Register(_clipView);   
        ServiceLocator.Current.Register(_streamBus);   
    }
}
