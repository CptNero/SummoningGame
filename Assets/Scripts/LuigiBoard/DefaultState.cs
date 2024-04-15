using UnityEngine;

public class DefaultState : IState {
    private Vector3 defaultPosition;

    public bool lastResult {get; set;}

    public string lastHint {get; set;}

    public DefaultState(LuigiBoardController controller, Vector3 position) : base(controller){
        defaultPosition = position;
    }

    private string defaultResponseIfWrong = "huh?";

    public override void OnEnter() {
        controller.audioSource.PlayOneShot(controller.returnSfx);
        controller.SetMarkerPosition(defaultPosition);

        lastResult = false;
        lastHint = "";

        var bufferContents = new string(controller.charBuffer.ToArray());
        var sinner = controller.currentSinner;

        // search in dialogue
        var response = "";
        foreach (var sentence in sinner.dialogue) {
            foreach(var hint in sentence.hints) {
                var bufferContentsInLower = bufferContents.ToLower();
                var expressionInLower = hint.expression.ToLower();

                if (bufferContentsInLower.Equals(expressionInLower)) {
                    controller.SetDialogueText(controller.HighlightDialogueText(bufferContents, "green"));
                    lastResult = true;
                    lastHint = bufferContents;
                }

                if (!lastResult) {
                    lastHint = bufferContents;
                    controller.SetDialogueText(controller.HighlightDialogueText(bufferContents, "red"));
                    break;
                }
            }
        }

        // search in evidence
        foreach(var keywordWithResponse in sinner.keywordsWithResponses)
        {
            var bufferContentsInLower = bufferContents.ToLower();
            var expressionInLower = keywordWithResponse.keyword.ToLower();

            if (bufferContentsInLower.Equals(expressionInLower))
            {
                controller.SetEvidenceText(controller.HighlightEvidenceText(bufferContents, "green"));
                lastResult = true;
                lastHint = bufferContents;
                response = keywordWithResponse.response;
            }

            if (!lastResult)
            {
                lastHint = bufferContents;
                controller.SetEvidenceText(controller.HighlightEvidenceText(bufferContents, "red"));
                response = defaultResponseIfWrong;
                break;
            }
        }

        if (!lastResult) {
            controller.SetDialogueText(controller.HighlightDialogueText(bufferContents, "red"));
            controller.SetEvidenceText(controller.HighlightEvidenceText(bufferContents, "red"));
        }

        controller.CallBroadcastHintResult(lastHint, response, lastResult);
        controller.charBuffer.Clear();
    }

    public override void Update() {}

    public override void OnExit() {}
}