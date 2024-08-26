using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GazeInteractable : MonoBehaviour
{
    public UnityEvent OnHoverEnter;
    public UnityEvent OnHoverExit;
    public UnityEvent OnSelectEnter;
    public UnityEvent OnSelectExit;

    private void Start()
    {
        OnHoverEnter.AddListener(HandleHoverEnter);
        OnHoverExit.AddListener(HandleHoverExit);
        OnSelectEnter.AddListener(HandleSelectEnter);
        OnSelectExit.AddListener(HandleSelectExit);
    }
    private void OnDestroy()
    {
        OnHoverEnter.RemoveListener(HandleHoverEnter);
        OnHoverExit.RemoveListener(HandleHoverExit);
        OnSelectEnter.RemoveListener(HandleSelectEnter);
        OnSelectExit.RemoveListener(HandleSelectExit);
    }
    private void HandleHoverEnter() { }
    private void HandleHoverExit() { }
    private void HandleSelectEnter() { }
    private void HandleSelectExit() { }
}




