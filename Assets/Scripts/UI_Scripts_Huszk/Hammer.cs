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
    
    bool animIsPlaying = false;
    float timerForHammerBase = 0;
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

    //Not in use I couldn't figure out how to get info when the animation ends
     bool AnimatorIsPlaying()
    {
        //return animator.GetCurrentAnimatorStateInfo(0).IsName("Base_Layer.Slam");
        return animator.GetCurrentAnimatorStateInfo(0).length >
           animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    void PlayHammerParticleEffect()
    {
        particleSys.Play();
    }

}
