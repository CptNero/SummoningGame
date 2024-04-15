using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource audioPlayer;
    [SerializeField] OpenDrawer tableDrawer;
    [SerializeField] OpenOnDoubleTapController docs;
    [SerializeField] OpenOnDoubleTapController luigy;
    [SerializeField] OpenOnDoubleTapController paper;
    [SerializeField] OpenOnDoubleTapController guide;
    [SerializeField] OpenOnDoubleTapController guide2;
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        tableDrawer.OnDrawerInteraction += PlaySound;
        docs.OnInteraction += PlaySound2;
        luigy.OnInteraction += PlaySound2;
        paper.OnInteraction += PlaySound2;
        guide.OnInteraction += PlaySound2;
        guide2.OnInteraction += PlaySound2;
    }

    private void PlaySound(object sender, OpenDrawer.OnDrawerInteractionEventArgs e)
    {
        audioPlayer.PlayOneShot(e.audioClip);
    }
    private void PlaySound2(object sender, OpenOnDoubleTapController.OnInteractionEventArgs e)
    {
        audioPlayer.PlayOneShot(e.audioClip);
    }



}
