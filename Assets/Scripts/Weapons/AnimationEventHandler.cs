using System;
using UnityEngine;

/// <summary>
/// The weapon animation event
/// </summary>
public class AnimationEventHandler : MonoBehaviour
{
    public event Action OnFinish;
    public event Action OnStartMovement;
    public event Action OnStopMovement;
    public event Action OnAttackAction;
    public event Action OnSpawnProjectile;
    public event Action OnDraw;
    public event Action<AttackPhases> OnEnterAttackPhase;
    private void AnimationFinishedTrigger() => OnFinish?.Invoke();
    private void StartMovementTrigger() => OnStartMovement?.Invoke();
    private void StopMovementTrigger() => OnStopMovement?.Invoke();
    private void AttackActionTrigger() => OnAttackAction?.Invoke();
    private void EnterAttackPhase(AttackPhases phase) => OnEnterAttackPhase?.Invoke(phase);
    private void SpawnProjectileTrigger() => OnSpawnProjectile?.Invoke();
    private void DrawTrigger() => OnDraw?.Invoke();
}
