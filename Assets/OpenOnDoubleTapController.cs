using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOnDoubleTapController : MonoBehaviour
{
    private float doubleTapDelay = 0.3f;
    private bool firstTap = true;
    private float lastTimeTapped;

    public GameObject paperToOpen;

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
            ClosePaper();
        }
    }

    private void OnMouseDown()
    {
        if(isOpen)
        {
            ClosePaper();
        }

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
        OpenPaper();
    }

    private void OpenPaper()
    {
        isOpen = true;
        paperToOpen.SetActive(isOpen);
    }
        
    private void ClosePaper()
    {
        isOpen = false;
        paperToOpen.SetActive(isOpen);
    }
}
