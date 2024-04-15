using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Text controls
    public TextMeshProUGUI dayTextMesh;

    public TextMeshProUGUI dialogueTextMesh;

    public TextMeshProUGUI responseTextMesh;

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

        public int currentDialogueIdx = 0;

        public SinnerDataModel.Emotion currentEmotion = SinnerDataModel.Emotion.Neutral;

        public string GetNextDialogue() {
            if (!(currentDialogueIdx < data.dialogue.Count)) {
                return "";
            }

            var dialogue = data.dialogue[currentDialogueIdx];
            var dialogueText = dialogue.text;

            for (var idx = 0; idx < data.dialogue[currentDialogueIdx].hints.Count; idx++) {
                dialogueText = dialogueText.Replace($"({idx})", data.dialogue[currentDialogueIdx].hints[idx].expression);
            }

            OnSinnerChangeEmotion(dialogue.emotion);

            currentDialogueIdx += 1;

            return dialogueText;
        }
    }

    internal class GameState {
        public uint currentDay = 1;

        public uint currentMonth = 1;
        public List<SinnerDataModel> sinners;

        public int currentSinnerIdx = -1;

        public SinnerState sinnerState;

        public void SetNextSinner() {
            if (!(currentSinnerIdx + 1 < sinners.Count)) {
                return;
            }

            currentSinnerIdx += 1;
            sinnerState = new SinnerState(sinners[currentSinnerIdx]);

            OnSinnerChange(sinnerState.data.assetName);
        }
    }

    internal GameState gameState {get; private set;}

    void SetDayText(uint dayIdx) {
        dayTextMesh.SetText("Day " + dayIdx.ToString());
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
        responseTextMesh.text = response;
        StartCoroutine(MakeTextDisappear());
    }

    void OnEnable() {
        LuigiBoardController.OnBroadcastHintResult += SetResult;
    }

    void OnDisable() {
        LuigiBoardController.OnBroadcastHintResult -= SetResult;
    }

    // Start is called before the first frame update
    void Awake() {
        gameState = new() {
            sinners = SinnerDataModel.LoadSinnersFromJson()
        };
    }

    void Start() {
        gameState.SetNextSinner();
        SetDialogueText(gameState.sinnerState.GetNextDialogue());
        SetDayText(gameState.currentDay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            SetDialogueText(gameState.sinnerState.GetNextDialogue());
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            gameState.SetNextSinner();
            SetDialogueText(gameState.sinnerState.GetNextDialogue());
        }
    }
}
