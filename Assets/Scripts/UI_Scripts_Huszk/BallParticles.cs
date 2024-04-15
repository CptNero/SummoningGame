using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallParticles : MonoBehaviour
{
    [SerializeField] GameController gameController;
    ParticleSystem particles;
    void Start()
    {
        particles = GetComponentInChildren<ParticleSystem>();
    }
    void PlayParticleEffectHandler(bool sinnerIsSentToRightCircle)
    {
        if(sinnerIsSentToRightCircle)
        {
        particles.Play();
        }
    }
}
