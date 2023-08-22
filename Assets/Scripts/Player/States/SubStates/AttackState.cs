using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weapon;
    private float previousGravityScale;
    private float lastAttackTime;
    private int inputIndex;

    public PlayerAttackState(
        Player currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        CombatInputs input,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState)
    {
        inputIndex = (int)input;
    }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        core.Movement.SetVelocity(Vector2.zero);
        previousGravityScale = context.RB.gravityScale;
        context.RB.gravityScale = 1f;
        weapon.Enter();
        weapon.OnExit += HandleExitWeapon;
    }

    public override void Exit()
    {
        base.Exit();
        core.Movement.SetVelocityX(0f);
        context.RB.gravityScale = previousGravityScale;
        lastAttackTime = Time.time;
        weapon.OnExit -= HandleExitWeapon;
    }
    public override void LogicUpdate()
    {
        weapon.CurrentInput = context.InputHandler.AttackInputs[inputIndex];
    }
    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
    }
    private void HandleExitWeapon()
    {
        isAbilityDone = true;
    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
        this.weapon.SetCore(core);
    }

    public void CheckIfShouldFlip()
    {
        if (xInput != 0)
        {
            core.Movement.CheckIfShouldFlip(xInput);
        }
    }
}
