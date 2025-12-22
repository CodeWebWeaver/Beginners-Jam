using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private GameBootstrapper _gameBootstrapper;
    [SerializeField] private RandomServiceSettings _randomServiceSettings;
    public override void InstallBindings() {
        Container.Bind<GameBootstrapper>()
        .FromComponentInNewPrefab(_gameBootstrapper)
        .AsSingle()
        .NonLazy();

        //Services
        Container.Bind<IDataSerializer>().To<JsonSerializer>().AsSingle();
        Container.Bind<ISaveStorage>().To<PlayerPrefsStorage>().AsSingle();
        Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();

        Container.Bind<IAudioService>().To<FmodAudioService>().AsSingle();
        
        StateMachineInstall();


        Container.Bind<IRandomService>().To<RandomService>().AsSingle().WithArguments(_randomServiceSettings);
    }

    private void StateMachineInstall() {
        Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();

        Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();

        Container.Bind<BootstrapState>().AsSingle();
        Container.Bind<GameLoopState>().AsSingle(); 
        Container.Bind<ExitState>().AsSingle();
    }
}
