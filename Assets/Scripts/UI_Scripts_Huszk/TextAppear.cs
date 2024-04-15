using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextAppear : MonoBehaviour
{
   public TextMeshProUGUI textComponent;
    public float revealSpeed = 0.05f;

    private string fullText;
    private string currentText;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        fullText = textComponent.text;
        StartCoroutine(RevealText());
    }

    IEnumerator RevealText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textComponent.text = currentText;
            yield return new WaitForSeconds(revealSpeed);
        }
    }
}
