using System.ComponentModel;
using UnityEngine;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ObjectPooler _objectPooler;
    [SerializeField] private ObstacleModel _obstacleModel;
    [SerializeField] private BoosterModel _busterModel;
    [SerializeField] private RoadModel _roadModel;
    [SerializeField] private CoinModel _coinModel;
    [SerializeField] private GameStateController _gameStateController;
    public override void InstallBindings()
    {
        Container.Bind<ObjectPooler>().FromInstance(_objectPooler).AsSingle();

        Container.Bind<ObstacleModel>().FromInstance(_obstacleModel).AsSingle();
        Container.Bind<BoosterModel>().FromInstance(_busterModel).AsSingle();
        Container.Bind<CoinModel>().FromInstance(_coinModel).AsSingle();
        Container.Bind<RoadModel>().FromInstance(_roadModel).AsSingle();

        Container.Bind<PlayerController>().FromInstance(_playerController).AsSingle();
        Container.Bind<GameStateController>().FromInstance(_gameStateController).AsSingle();

        Container.Bind<RoadFactory>().FromNew().AsSingle();
        Container.Bind<InteractableRoadObjectsFactory>().FromNew().AsSingle();
    }
}