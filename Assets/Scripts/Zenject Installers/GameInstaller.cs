using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private GameBootstrapper _gameBootstrapper;
    public override void InstallBindings() {
        Container.Bind<GameBootstrapper>()
        .FromComponentInNewPrefab(_gameBootstrapper)
        .AsSingle()
        .NonLazy();

        Container.Bind<IDataSerializer>().To<JsonSerializer>().AsSingle();
        Container.Bind<ISaveStorage>().To<PlayerPrefsStorage>().AsSingle();
        Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();


        //Services
        Container.Bind<IAudioService>().To<FmodAudioService>().AsSingle();
        
        StateMachineInstall();
    }

    private void StateMachineInstall() {
        Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();

        Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();

        Container.Bind<BootstrapState>().AsSingle();
        Container.Bind<GameLoopState>().AsSingle(); 
        Container.Bind<ExitState>().AsSingle();
    }
}
