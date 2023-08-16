using System;
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnFinish;
    public event Action OnStartMovement;
    public event Action OnStopMovement;
    private void AnimationFinishedTrigger()
    {
        Debug.Log("animation finished trigger");
        OnFinish?.Invoke();
    }
    private void StartMovementTrigger()
    {
        OnStartMovement?.Invoke();
    }
    private void StopMovementTrigger()
    {
        OnStopMovement?.Invoke();
    }
}
