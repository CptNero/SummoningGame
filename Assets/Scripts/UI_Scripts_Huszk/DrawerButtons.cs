using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerButton : MonoBehaviour
{
    [SerializeField] CircleHandler circleHandler;
    void OnMouseDown()
    {
        circleHandler.currentlyPressedButton = gameObject;
        Debug.Log("Click");
    }
    

}
