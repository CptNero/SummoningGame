using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Sinner : MonoBehaviour
{
    private SpriteLibrary spriteLibrary;

    private SpriteRenderer spriteRenderer;

    internal SinnerDataModel data {get; set;}

    private SinnerDataModel.Emotion currentEmotion = SinnerDataModel.Emotion.Neutral;

    // Start is called before the first frame update

    void Start()
    {
        spriteLibrary = GetComponent<SpriteLibrary>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void ChangeSinnerSprite(SinnerDataModel.Emotion emotion) {
        currentEmotion = emotion;

        var emotionName = emotion.ToString();
        const string category = "Emotions";

        var sprite = spriteLibrary.GetSprite(category, emotionName);;

        spriteRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home)) {
            var emotionCount = Enum.GetNames(typeof(SinnerDataModel.Emotion)).Length;
            var nextEmotion = ((int)currentEmotion + 1) % emotionCount;
            ChangeSinnerSprite((SinnerDataModel.Emotion)nextEmotion);
        }
    }
}
