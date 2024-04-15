using System;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Sinner : MonoBehaviour
{
    private SpriteLibrary spriteLibrary;

    private SpriteRenderer spriteRenderer;

    internal SinnerDataModel data {get; set;}

    // Start is called before the first frame update

    void OnEnable() {
        GameController.OnSinnerChangeEmotion += ChangeSinnerSprite;

        spriteLibrary = GetComponent<SpriteLibrary>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnDisable() {
        GameController.OnSinnerChangeEmotion -= ChangeSinnerSprite;
    }

    void Start()
    {
    }

    void ChangeSinnerSprite(SinnerDataModel.Emotion emotion) {
        var emotionName = emotion.ToString();
        const string category = "Emotions";

        var sprite = spriteLibrary.GetSprite(category, emotionName);;

        spriteRenderer.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
