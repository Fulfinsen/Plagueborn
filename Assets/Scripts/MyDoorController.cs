using System;
using UnityEngine;

public class MyDoorController : MonoBehaviour
{
    Animator doorAnim;
    
    bool doorOpen = false;

    void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!doorOpen)
        {
            doorAnim.Play("DoorOpen", 0, 0.0f );
            doorOpen = true;
        }
        else
        {
            doorAnim.Play("DoorClose", 0, 0.0f );
            doorOpen = false;
        }
    }
}
