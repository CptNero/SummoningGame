using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    [SerializeField] GameObject drawerUI;
    bool drawerUIOpen = false;
    void OnMouseDown()
    {
        if(!drawerUI.activeSelf)
        {
        drawerUI.SetActive(true);
        drawerUIOpen = true;
        }
    }
    void Update()
    {
        if(drawerUI.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                drawerUI.SetActive(false);
            }
        }
    }
}
