using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public float lastDashTime = Mathf.NegativeInfinity;
    public bool isWaitingDirectionInput;
    private Vector2 startPosition;
    public Vector2 dashDirection;
    public Vector2 dashDirectionInput;
    public Vector2 lastAfterImagePosition;
    public bool dashInputStop;
    public float previousGravityScale;

    public PlayerDashState(
        Player currentContext,
        PlayerStateFactory states,
        PlayerData playerData,
        string animBoolName,
        bool isRootState = false
    )
        : base(currentContext, states, playerData, animBoolName, isRootState) { }

    public override void Enter(object data = null)
    {
        base.Enter(data);
        core.Movement.SetVelocity(Vector2.zero);
        isWaitingDirectionInput = true;
        startPosition = context.transform.position;
        dashDirection = context.transform.right;
        Time.timeScale = playerData.directionSelectTimeScale;
        startTime = Time.unscaledTime;
        context.DashDirectionIndicator.gameObject.SetActive(true);
        previousGravityScale = context.RB.gravityScale;
        context.RB.gravityScale = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        lastDashTime = Time.time;
        context.RB.gravityScale = previousGravityScale;
        core.Movement.SetVelocity(Vector2.zero);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isWaitingDirectionInput) // select dash direction
        {
            core.Movement.SetVelocity(Vector2.zero);
            dashDirectionInput = context.InputHandler.RawMovementInput;
            dashInputStop = context.InputHandler.DashInputStop;
            if (dashDirectionInput != Vector2.zero)
            {
                dashDirection = dashDirectionInput;
                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                context.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);
            }
            if (dashInputStop || Time.unscaledTime >= startTime + playerData.directionSelectionTime)
            {
                isWaitingDirectionInput = false;
                context.DashDirectionIndicator.gameObject.SetActive(false);
                Time.timeScale = 1f;
                startTime = Time.time;
                core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
            }
        }
        else // while dashing
        {
            core.Movement.SetVelocity(playerData.dashVelocity, dashDirection);
            CheckIfPlaceAfterImage();
            float dashedDistance = Vector2.Distance(
                startPosition,
                (Vector2)context.transform.position
            );
            if (
                dashedDistance >= playerData.dashDistance
                || Time.time >= startTime + playerData.dashDuration
            )
            {
                isAbilityDone = true;
            }
            context.Anim.SetFloat("yVelocity", core.Movement.CurrentVelocity.y);
            context.Anim.SetFloat("xVelocity", Mathf.Abs(core.Movement.CurrentVelocity.x));
        }
    }

    private void CheckIfPlaceAfterImage()
    {
        float afterImageDistance = Vector2.Distance(
            context.transform.position,
            lastAfterImagePosition
        );
        if (afterImageDistance >= playerData.afterImageDistance)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAfterImagePosition = context.transform.position;
    }

    public override void InitializeSubState() { }

    public override void CheckSwitchStates() { }

    public bool CanDash()
    {
        return Time.time > lastDashTime + playerData.dashCoolDown;
    }
}
