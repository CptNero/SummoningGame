using UnityEngine;

public class OnLetterState : IState {
    private readonly float timeToStay = 2.0f;
    private float elapsedTime = 0.0f;

    public OnLetterState(LuigiBoardController controller) : base(controller) {}

    public override void OnEnter() {
        controller.audioSource.PlayOneShot(controller.RandomLetterSfx());
    }

    public override void Update() {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > timeToStay) {
            controller.SetState(new TransitionState(controller,
                                                    null,
                                                    controller.GetMarkerPosition(),
                                                    controller.markerDefaultPosition,
                                                    controller.defaultState));
        }
    }

    public override void OnExit() {
        elapsedTime = 0.0f;
    }
}