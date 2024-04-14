
public class IState {
    protected LuigiBoardController controller;

    public IState(LuigiBoardController controller) {
        this.controller = controller;
    }

    public virtual void OnEnter() {}

    public virtual void Update() {}

    public virtual void OnExit() {}
}