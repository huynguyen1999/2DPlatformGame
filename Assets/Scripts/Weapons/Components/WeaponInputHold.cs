using UnityEngine;

public class WeaponInputHold : WeaponComponent
{
    private Animator anim;
    private bool input;

    private void HandleCurrentInputChange(bool newInput)
    {
        input = newInput;
        SetAnimatorParameters();
    }
    private void SetAnimatorParameters()
    {
        anim.SetBool("Hold", input);
    }
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponentInChildren<Animator>();
        weapon.OnCurrentInputChange += HandleCurrentInputChange;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        weapon.OnCurrentInputChange -= HandleCurrentInputChange;
    }
}