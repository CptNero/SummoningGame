using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddTextToPaperController : MonoBehaviour
{
    public GameObject paper;

    private void Start()
    {
        var text = this.gameObject.GetComponent<TextMeshPro>();
        text.text = "test";
        Debug.Log(text.text);
    }
}
