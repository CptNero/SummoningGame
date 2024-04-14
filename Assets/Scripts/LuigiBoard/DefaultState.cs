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
        var sinner = controller.currentSinner;

        foreach (var sentence in sinner.dialogue) {
            foreach(var hint in sentence.hints) {
                var bufferContentsInLower = bufferContents.ToLower();
                var expressionInLower = hint.expression.ToLower();

                if (bufferContentsInLower.Equals(expressionInLower)) {
                    Debug.Log("Bingo:");
                    break;
                } else {
                    Debug.Log($"Wrong: {bufferContentsInLower} != {expressionInLower}");
                }
            }
        }

        controller.charBuffer.Clear();
    }

    public override void Update() {}

    public override void OnExit() {}
}