using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class GazeInteractor : MonoBehaviour
{
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private LayerMask gazeLayer;
    [SerializeField] private bool ETonly = false;   // scene with only ET or with controllers? 
    [SerializeField] private bool controllersOnly = false;
    [SerializeField] private GameObject rightEye;
    [SerializeField] private GameObject leftEye;

    private GazeInteractable hoveredInteractable;
    private GazeInteractable selectedInteractable;
    private GazeInteractable currentInteractable;

    private Stopwatch stopwatchET;
    private TimeSpan elapsedTimeET;
    private const float selectionTime = 0.6f;

    private void Start()
    {
        if (ETonly)
        {
            elapsedTimeET = TimeSpan.Zero;
            stopwatchET = new Stopwatch();
        }
    }

    void FixedUpdate()
    {
        ExecuteHovering();
        if (ETonly)
        {
            ExecuteETSelection();
        }
        else
        {
            ExecuteButtonSeletion();
        }
    }

    private void ExecuteHovering()
    {
        if (!controllersOnly)    // ignore this calculation for scenes with controllers only
        {
            Vector3 averagePosition = (rightEye.transform.position + leftEye.transform.position) / 2.0f;
            Quaternion averageRotation = Quaternion.Slerp(rightEye.transform.rotation, leftEye.transform.rotation, 0.5f);
            this.transform.position = averagePosition;
            this.transform.rotation = averageRotation;
        }
       
        Vector3 rayDirection = transform.forward * rayDistance;
        if (Physics.Raycast(transform.position, rayDirection, out RaycastHit hit, rayDistance, gazeLayer))
        {
            currentInteractable = hit.collider.GetComponent<GazeInteractable>();
            if (hoveredInteractable != null && hoveredInteractable != currentInteractable)     // hoveredInteractable is different from what we are currently interacting with
            {
                hoveredInteractable.OnHoverExit.Invoke();
                hoveredInteractable = currentInteractable;
                hoveredInteractable.OnHoverEnter.Invoke();

                if (ETonly)
                {
                    stopwatchET = Stopwatch.StartNew();
                    elapsedTimeET = TimeSpan.Zero;
                }
            }
            else if (hoveredInteractable == null)     // start new hovering 
            {
                hoveredInteractable = currentInteractable;
                hoveredInteractable.OnHoverEnter.Invoke();

                if (ETonly)
                {
                    stopwatchET = Stopwatch.StartNew();
                    elapsedTimeET = TimeSpan.Zero;
                }
            }
        }
        else if (hoveredInteractable != null)    // if we are not hovering now but we used to
        {
            hoveredInteractable.OnHoverExit.Invoke();
            hoveredInteractable = null;

            if (ETonly)
            {
                stopwatchET.Reset();
                elapsedTimeET = TimeSpan.Zero;
            }
        }
    }

    private void ExecuteButtonSeletion()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            if(selectedInteractable == null && hoveredInteractable != null)
            {
                selectedInteractable = hoveredInteractable;
                selectedInteractable.OnSelectEnter.Invoke();
                selectedInteractable.GetComponent<AudioSource>().Play();
            }
        }
        else if (selectedInteractable != null)
        {
            selectedInteractable.OnSelectExit.Invoke();
            selectedInteractable = null;
        }
    }
    private void ExecuteETSelection()
    {
        elapsedTimeET = stopwatchET.Elapsed;
      
        if (elapsedTimeET.TotalSeconds >= selectionTime)
        {
            if (selectedInteractable == null && hoveredInteractable != null)
            {
                selectedInteractable = hoveredInteractable;
                selectedInteractable.OnSelectEnter.Invoke();

                selectedInteractable.GetComponent<AudioSource>().Play();

                stopwatchET = Stopwatch.StartNew();
                elapsedTimeET = TimeSpan.Zero;
            }
        }
        else if (selectedInteractable != null)
        {
            selectedInteractable.OnSelectExit.Invoke();
            selectedInteractable = null;

            stopwatchET = Stopwatch.StartNew();
            elapsedTimeET = TimeSpan.Zero;
        }
    }

}
