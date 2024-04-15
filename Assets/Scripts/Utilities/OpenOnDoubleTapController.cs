using System;
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
    public AudioClip openAudioClip;
    public AudioClip closeAudioClip;

    public event EventHandler<OnInteractionEventArgs> OnInteraction;
    public class OnInteractionEventArgs: EventArgs
    {
        public AudioClip audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastTimeTapped + doubleTapDelay)
        {
            firstTap = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isOpen)
            {
            PlayCloseSound();
             Close();
            }
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
        PlayOpenSound();
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

    private void PlayOpenSound()
    {
        OnInteraction?.Invoke(this,new OnInteractionEventArgs{audioClip = openAudioClip});
    }
    private void PlayCloseSound()
    {
        OnInteraction?.Invoke(this,new OnInteractionEventArgs{audioClip = closeAudioClip});
    }
}
