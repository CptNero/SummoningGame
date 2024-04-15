using UnityEngine;

public class DefaultState : IState {
    private Vector3 defaultPosition;

    public bool lastResult {get; set;}

    public string lastHint {get; set;}

    public DefaultState(LuigiBoardController controller, Vector3 position) : base(controller){
        defaultPosition = position;
    }

    public override void OnEnter() {
        controller.audioSource.PlayOneShot(controller.returnSfx);
        controller.SetMarkerPosition(defaultPosition);

        lastResult = false;
        lastHint = "";

        var bufferContents = new string(controller.charBuffer.ToArray());
        var sinner = controller.currentSinner;

        foreach (var sentence in sinner.dialogue) {
            foreach(var hint in sentence.hints) {
                var bufferContentsInLower = bufferContents.ToLower();
                var expressionInLower = hint.expression.ToLower();

                if (bufferContentsInLower.Equals(expressionInLower)) {
                    controller.SetText(controller.HighlightText(bufferContents, "green"));
                    lastResult = true;
                    lastHint = bufferContents;
                }

                if (!lastResult) {
                    lastHint = bufferContents;
                    controller.SetText(controller.HighlightText(bufferContents, "red"));
                    break;
                }
            }
        }

        if (!lastResult) {
            controller.SetText(controller.HighlightText(bufferContents, "red"));
        }

        controller.CallBroadcastHintResult(lastHint, lastResult);
        controller.charBuffer.Clear();
    }

    public override void Update() {}

    public override void OnExit() {}
}