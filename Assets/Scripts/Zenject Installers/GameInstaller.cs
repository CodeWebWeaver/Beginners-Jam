using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller {
    [SerializeField] private GameBootstrapper _gameBootstrapper;
    [SerializeField] private RandomServiceSettings _randomServiceSettings;
    [SerializeField] private SceneData _sceneData;
    [SerializeField] private UIManager _uiManager;
    public override void InstallBindings() {
        Container.Bind<GameBootstrapper>()
        .FromComponentInNewPrefab(_gameBootstrapper)
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<UIManager>()
            .FromComponentInNewPrefab(_uiManager)
            .AsSingle()
            .NonLazy();
        Container.Bind<ILoadingScreenService>().To<LoadingScreenService>().AsSingle();

        BindSavings();

        StateMachineInstall();

        Container.Bind<IDataSerializer>().To<JsonSerializer>().AsSingle();

        Container.Bind<IGameManager>().To<GameManager>().AsSingle();

        //Services
        Container.Bind<IAudioService>().To<FmodAudioService>().AsSingle();
        Container.Bind<IRandomService>().To<RandomService>().AsSingle().WithArguments(_randomServiceSettings);

        Container.Bind<ILevelProgressService>().To<LevelProgressService>().AsSingle();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        Container.Bind<ISceneTransitionManager>().To<SceneTransitionManager>().AsSingle();

        //Systems

        Container.Bind<LevelProgressSystem>().AsSingle().NonLazy();
        Container.Bind<AudioSystem>().AsSingle().NonLazy();
    }


    private void BindSavings() {
        Container.Bind<ISaveStorage>().To<PlayerPrefsStorage>().AsSingle();
        Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
    }

    private void StateMachineInstall() {
        Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();

        Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();

        Container.Bind<BootstrapState>().AsSingle().WithArguments(_sceneData);
        Container.Bind<LoadingLevelState>().AsSingle();
        Container.Bind<LoadingMainMenuState>().AsSingle().WithArguments(_sceneData);
        Container.Bind<MainMenuState>().AsSingle();
        Container.Bind<GameLoopState>().AsSingle();
        Container.Bind<PauseState>().AsSingle();
        Container.Bind<ExitState>().AsSingle();
    }
}
