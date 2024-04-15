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
    [SerializeField] GameObject board; 
    [SerializeField] GameObject paper;
    [SerializeField] GameObject guide;
    [SerializeField] GameObject docs;  
    [SerializeField] GameObject hammer;

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
        board.SetActive(false);
        paper.SetActive(false);
        guide.SetActive(false);
        docs.SetActive(false);
        hammer.SetActive(false);
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
                board.SetActive(true);
                guide.SetActive(true);
                paper.SetActive(true);
                docs.SetActive(true);
                hammer.SetActive(true);
            }
        }
    }
}
