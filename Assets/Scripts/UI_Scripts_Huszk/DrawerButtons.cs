using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawerButton : MonoBehaviour
{
    [SerializeField] CircleHandler circleHandler;
    SpriteRenderer buttonEffect;
    AudioSource audioSource;
    [SerializeField] AudioClip click;
    Color color;
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
        buttonEffect = GetComponent<SpriteRenderer>();
        color = buttonEffect.color;
        color.a = 0f;
        buttonEffect.color = color;
    }
    void OnMouseDown()
    {
        circleHandler.currentlyPressedButton = gameObject;
        audioSource.PlayOneShot(click);
        Debug.Log("Click");
    }
    
    void Update()
    {
        if(circleHandler.currentCircle.button == gameObject)
        {
            color.a = 0.4f;
            buttonEffect.color = color;
        }
        if(circleHandler.currentCircle.button != gameObject)
        {
            color.a = 0f;
            buttonEffect.color = color;
        }
        
    }
}
