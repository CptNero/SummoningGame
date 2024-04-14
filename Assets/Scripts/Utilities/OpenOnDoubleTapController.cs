using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOnDoubleTapController : MonoBehaviour
{
    private float doubleTapDelay = 0.3f;
    private bool firstTap = true;
    private float lastTimeTapped;

    public GameObject objectToOpen;

    public bool isOpen = false;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastTimeTapped + doubleTapDelay)
        {
            firstTap = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    private void OnMouseDown()
    { 
        if(firstTap)
        {
            lastTimeTapped = Time.time;
            firstTap = false;
        }
        else
        {
            OnDoubleTap();
            firstTap = true;
        }
 
    }

    private void OnDoubleTap()
    {
        // TODO: animation maybe?
        Open();
    }

    private void Open()
    {
        isOpen = true;
        objectToOpen.SetActive(isOpen);
    }
        
    private void Close()
    {
        isOpen = false;
        objectToOpen.SetActive(isOpen);
    }
}
