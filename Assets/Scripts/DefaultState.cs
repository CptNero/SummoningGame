using UnityEngine;

public class DefaultState : IState {
    private Vector3 defaultPosition;

    public DefaultState(LuigiBoardController controller, Vector3 position) : base(controller){
        defaultPosition = position;
    }

    public override void OnEnter() {
        controller.audioSource.PlayOneShot(controller.returnSfx);
        controller.SetMarkerPosition(defaultPosition);

        var bufferContents = new string(controller.charBuffer.ToArray());
        Debug.Log(bufferContents);
        controller.charBuffer.Clear();
    }

    public override void Update() {}

    public override void OnExit() {}
}