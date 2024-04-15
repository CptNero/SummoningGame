using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Text controls
    public TextMeshProUGUI dayTextMesh;

    public TextMeshProUGUI dialogueTextMesh;

    public TextMeshProUGUI responseTextMesh;
    public CircleHandler circleHandler;

    public Hammer hammer;

    // Events
    public delegate void ChangeSinner(string sinnerName);
    public delegate void ChangeSinnerEmotion(SinnerDataModel.Emotion emotion);

    public delegate void ChangeDialogueText(string text);

    public static event ChangeDialogueText OnDialogueTextChange;

    public static event ChangeSinner OnSinnerChange;

    public static event ChangeSinnerEmotion OnSinnerChangeEmotion;

    // Game state
    internal class SinnerState {
        public SinnerState(SinnerDataModel data) {
            this.data = data;
        }

        public SinnerDataModel data;

        public string currentDialogue = "";

        public int currentDialogueIdx = -1;

        public SinnerDataModel.Emotion currentEmotion = SinnerDataModel.Emotion.Neutral;

        public string GetNextDialogue() {
            if (!(currentDialogueIdx + 1 < data.dialogue.Count)) {
                return "";
            }

            currentDialogueIdx += 1;

            var dialogue = data.dialogue[currentDialogueIdx];
            var dialogueText = dialogue.text;

            for (var idx = 0; idx < data.dialogue[currentDialogueIdx].hints.Count; idx++) {
                dialogueText = dialogueText.Replace($"({idx})", data.dialogue[currentDialogueIdx].hints[idx].expression);
            }

            OnSinnerChangeEmotion(dialogue.emotion);

            Debug.Log(currentDialogueIdx);

            return dialogueText;
        }

        public string GetPreviousDialogue() {
            if (!(currentDialogueIdx > 0)) {
                return "";
            }

            currentDialogueIdx -= 1;

            var dialogue = data.dialogue[currentDialogueIdx];
            var dialogueText = dialogue.text;

            for (var idx = 0; idx < data.dialogue[currentDialogueIdx].hints.Count; idx++) {
                dialogueText = dialogueText.Replace($"({idx})", data.dialogue[currentDialogueIdx].hints[idx].expression);
            }

            OnSinnerChangeEmotion(dialogue.emotion);

            Debug.Log(currentDialogueIdx);

            return dialogueText;
        }
    }

    internal class GameState {
        public uint currentDay = 1;

        public uint currentMonth = 1;
        public List<SinnerDataModel> sinners;

        public int currentSinnerIdx = -1;

        public SinnerState sinnerState;

        public HashSet<string> evidenceFound;

        public void SetNextSinner() {
            if (!(currentSinnerIdx + 1 < sinners.Count)) {
                return;
            }

            currentSinnerIdx += 1;
            sinnerState = new SinnerState(sinners[currentSinnerIdx]);

            OnSinnerChange(sinnerState.data.assetName);
        }

        public SinnerDataModel GetCurrentSinner()
        {
            return sinners[currentSinnerIdx];
        }
    }

    internal GameState gameState {get; private set;}

    void SetEvidenceFoundText() {
        dayTextMesh.SetText($"Evidence found:  {gameState.evidenceFound.Count} / {gameState.sinnerState.data.hintCount}");
    }

    void SetDialogueText(string text) {
        dialogueTextMesh.SetText(text);

        if (OnDialogueTextChange != null) {
            OnDialogueTextChange(text);
        }
    }

    System.Collections.IEnumerator MakeTextDisappear()
    {
        yield return new WaitForSeconds(4);
        responseTextMesh.text = "";
    }

    void SetResult(string hint, string response, bool result) {
        if (result) {
            responseTextMesh.text = response;
            StartCoroutine(MakeTextDisappear());
            gameState.evidenceFound.Add(hint);
            SetEvidenceFoundText();
        }
    }

    void Judgement() {
        if (gameState.evidenceFound.Count != gameState.sinnerState.data.hintCount) {
            return;
        }

        var selectedCircleIdx = int.Parse(circleHandler.currentlyPressedButton.name.ToCharArray().Last().ToString());
        if (selectedCircleIdx == gameState.sinnerState.data.correctLayer) {
            Debug.Log("You were correct!");
        } else {
            Debug.Log("Wrong");
        }

        gameState.SetNextSinner();
        gameState.evidenceFound.Clear();
        SetEvidenceFoundText();
        SetDialogueText(gameState.sinnerState.GetNextDialogue());
    }

    void OnEnable() {
        LuigiBoardController.OnBroadcastHintResult += SetResult;
        Hammer.OnHammerWasClicked += Judgement;
    }

    void OnDisable() {
        LuigiBoardController.OnBroadcastHintResult -= SetResult;
        Hammer.OnHammerWasClicked -= Judgement;
    }

    // Start is called before the first frame update
    void Awake() {
        gameState = new() {
            sinners = SinnerDataModel.LoadSinnersFromJson(),
            evidenceFound = new(),
        };
    }

    void Start() {
        gameState.SetNextSinner();
        SetDialogueText(gameState.sinnerState.GetNextDialogue());
        SetEvidenceFoundText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            SetDialogueText(gameState.sinnerState.GetPreviousDialogue());
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            SetDialogueText(gameState.sinnerState.GetNextDialogue());
        }
    }
}
