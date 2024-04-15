using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    [SerializeField] GameObject drawerUI;
    [SerializeField] SoundEffects soundEffects;
    public AudioClip openDrawer;
    public AudioClip closeDrawer;

    public event EventHandler<OnDrawerInteractionEventArgs> OnDrawerInteraction;
    public class OnDrawerInteractionEventArgs: EventArgs
    {
        public AudioClip audioClip;
    }

    void OnMouseDown()
    {
        if(!drawerUI.activeSelf)
        {
        drawerUI.SetActive(true);
        OnDrawerInteraction?.Invoke(this,new OnDrawerInteractionEventArgs{audioClip = openDrawer});
        //soundEffects.audioPlayer.PlayOneShot(soundEffects.fiokOpen);
        }
    }
    void Update()
    {
        if(drawerUI.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                OnDrawerInteraction?.Invoke(this,new OnDrawerInteractionEventArgs{audioClip = closeDrawer});
                drawerUI.SetActive(false);
            }
        }
    }
}
