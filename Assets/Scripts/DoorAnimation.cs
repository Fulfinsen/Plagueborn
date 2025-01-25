using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoorAnimation : MonoBehaviour
{
    [SerializeField] int rayLenght = 5;
    [SerializeField] LayerMask layerMaskInteract;
    [SerializeField] string excludeLayerName = null;

    MyDoorController rayCastedObj;
    
    [SerializeField] KeyCode openDoorKey = KeyCode.E;
    [SerializeField] Image crosshair = null;
    bool isCrosshairActive;
    bool doOnce;
    
    const string interactableTag = "InteractiveObject";

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        if (Physics.Raycast(transform.position, fwd, out hit, rayLenght, mask))
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if(!doOnce)
                {
                    rayCastedObj = hit.collider.gameObject.GetComponent<MyDoorController>();
                    CrosshairChange(true);
                }
                isCrosshairActive = true;
                doOnce = true;

                if (Input.GetKeyDown(openDoorKey))
                {
                    rayCastedObj.PlayAnimation();
                }
            }
        }
        else
        {
            if (isCrosshairActive)
            {
                CrosshairChange(false); 
                doOnce = false;    
            }
        }
    }

    void CrosshairChange(bool state)
    {
        if (state && !doOnce)
        {
            crosshair.color = Color.red;
        }
        else
        {
            crosshair.color = Color.white;
            isCrosshairActive = false;
        }
    }
}
