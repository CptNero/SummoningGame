using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    [SerializeField] GameObject hammer;
    Animator animator;
    [SerializeField] AudioSource audioEffectSource;
    ParticleSystem particleSys;
    
    public delegate void HammerWasClicked();

    public static event HammerWasClicked OnHammerWasClicked;

    bool animIsPlaying = false;
    float timerForStopAnim = 0;
    void Start()
    {
        animator = hammer.GetComponent<Animator>();
        particleSys = hammer.GetComponentInChildren<ParticleSystem>();
    }

    void OnMouseDown()
    {
        PlayHammerAnimation();
        PlayHammerParticleEffect();
        OnHammerWasClicked();
    }
    void Update()
    {
       StopHammerAnimationHandler();

    }

    public void PlayHammerAnimation()
    {

        if(!animIsPlaying)
        {
            animator.SetBool("IsSlam",true);
            PlaySoundHandler();
            animIsPlaying = true;
        }

    }

    public void StopHammerAnimationHandler()
    {
        if(animator.GetBool("IsSlam"))
        {
        if(timerForStopAnim > 1)
        {
            animator.SetBool("IsSlam",false);
            animIsPlaying = false;
            timerForStopAnim = 0;
            particleSys.Stop();
        }
        else
        {
            timerForStopAnim = timerForStopAnim + Time.deltaTime;
        }
        }
    }

    void PlaySoundHandler()
    {
        audioEffectSource.Play();
    }

    void PlayHammerParticleEffect()
    {
        particleSys.Play();
    }

}
