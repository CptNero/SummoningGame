using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellCircleButton : MonoBehaviour
{

    public string text;

    private string currentToolTipText = "";
    private GUIStyle guiStyleFore;
    private GUIStyle guiStyleBack;

    void Start()
    {
        guiStyleFore = new GUIStyle();
        guiStyleFore.normal.textColor = Color.white;
        guiStyleFore.alignment = TextAnchor.UpperCenter;
        guiStyleFore.wordWrap = true;
        guiStyleBack = new GUIStyle();
        guiStyleBack.normal.textColor = Color.black;
        guiStyleBack.alignment = TextAnchor.UpperCenter;
        guiStyleBack.wordWrap = true;
    }

    // Text on hover
    void OnMouseEnter()
    {

        currentToolTipText = text;
    }

    void OnMouseExit()
    {
        currentToolTipText = "";
    }

    void OnGUI()
    {
        if (currentToolTipText != "")
        {
            var x = Event.current.mousePosition.x;
            var y = Event.current.mousePosition.y;
            GUI.Label(new Rect(x - 149, y - 60, 300, 60), currentToolTipText, guiStyleBack);
            GUI.Label(new Rect(x - 150, y - 60, 300, 60), currentToolTipText, guiStyleFore);
        }
    }


    // On tap
    private void OnMouseUpAsButton()
    {
        HellCircleWasChosen();
    }

    private void HellCircleWasChosen()
    {
        // TODO: actual behaviour
        Debug.Log(text + " circle was chosen");
    }
}
