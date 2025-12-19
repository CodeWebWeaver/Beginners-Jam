using Zenject;
//Used for test scene only!!!
public class TestSceneInstaller : MonoInstaller {
    public override void InstallBindings() {
        Container.Bind<IUiManager>().To<UiManager>().FromComponentInHierarchy().AsSingle();
    }
}