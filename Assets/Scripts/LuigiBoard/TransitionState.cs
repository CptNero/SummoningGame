using UnityEngine;

public class TransitionState : IState {
    private Vector3 oldPosition;
    private Vector3 currentPosition;
    private Vector3 newPosition;

    private float interpolationTime = 0.1f;
    private float elapsedTime = 0.0f;

    private char? currentLetter;

    private IState stateAfterTransition;

    public TransitionState(LuigiBoardController controller, char? letter, Vector3 oldPosition, Vector3 newPosition,
                           IState stateAfterTransition) : base(controller) {
        this.oldPosition = oldPosition;
        this.newPosition = newPosition;
        this.stateAfterTransition = stateAfterTransition;
        this.currentLetter = letter;
    }

    public override void OnEnter() {
        if (currentLetter == ' ') {
            controller.audioSource.PlayOneShot(controller.spaceSfx);
        }
    }

    public override void Update() {
        var interpolationRatio = elapsedTime / interpolationTime;
        currentPosition = new Vector3(Mathf.SmoothStep(oldPosition.x, newPosition.x, interpolationRatio),
                                      Mathf.SmoothStep(oldPosition.y, newPosition.y, interpolationRatio),
                                      Mathf.SmoothStep(oldPosition.z, newPosition.z, interpolationRatio));

        controller.SetMarkerPosition(currentPosition);

        elapsedTime = (elapsedTime + Time.deltaTime) % (interpolationTime + Time.deltaTime);

        if (currentPosition == newPosition) {
            controller.SetState(stateAfterTransition);
        }
    }

    public override void OnExit() {}
}