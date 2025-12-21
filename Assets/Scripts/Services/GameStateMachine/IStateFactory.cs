using Zenject;

public interface IStateFactory {
    T CreateState<T>() where T : IState;
}

public class StateFactory : IStateFactory {
    private readonly DiContainer _container;

    public StateFactory(DiContainer container) {
        _container = container;
    }

    public T CreateState<T>() where T : IState {
        return _container.Resolve<T>();
    }
}
