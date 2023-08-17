using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    #region Components
    public Core Core { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public BoxCollider2D PlayerCollier { get; private set; }
    #endregion

    #region State Variables
    public PlayerBaseState currentState;
    public PlayerBaseState previousState;
    public PlayerStateFactory states;
    public Transform DashDirectionIndicator;
    #endregion

    #region Other Variables
    [SerializeField]
    private PlayerData playerData;
    private Weapon primaryWeapon,
        secondaryWeapon;


    #endregion
    //-----------------------------//
    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        states = new PlayerStateFactory(this, playerData);
        primaryWeapon = transform.Find("PrimaryWeapon").GetComponent<Weapon>();
        secondaryWeapon = transform.Find("SecondaryWeapon").GetComponent<Weapon>();
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        PlayerCollier = GetComponent<BoxCollider2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
        DashDirectionIndicator.gameObject.SetActive(false);
        Initialize(states.GroundedState);
        states.PrimaryAttackState.SetWeapon(primaryWeapon);
        states.SecondaryAttackState.SetWeapon(secondaryWeapon);
    }

    private void Update()
    {
        currentState.LogicUpdateStates();
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdateStates();
    }
    #endregion

    #region Other Methods
    private void Initialize(PlayerBaseState initState)
    {
        currentState = initState;
        currentState.Enter();
    }

    public Vector2 DetermineCornerPosition()
    {
        RaycastHit2D xHit = Physics2D.Raycast(
            Core.CollisionDetection.WallCheck.position,
            transform.right,
            Core.CollisionDetection.WallCheckDistance,
            Core.CollisionDetection.WhatIsGround
        );
        float xDist = xHit.distance;
        // add some offset to make sure it collides with the ground
        Vector2 workspace = new((xDist + 0.01f) * Core.Movement.FacingDirection, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(
            Core.CollisionDetection.LedgeCheckHorizontal.position + (Vector3)(workspace),
            -transform.up,
            Core.CollisionDetection.WallCheckDistance,
            Core.CollisionDetection.WhatIsGround
        );
        float yDist = yHit.distance;
        if (!yHit) // somehow, the yHit sometimes return false, the possible cause could be ledgecheck is under the corner
        {
            yHit = Physics2D.Raycast(
                        Core.CollisionDetection.LedgeCheckHorizontal.position + (Vector3)(workspace),
                        transform.up,
                        Core.CollisionDetection.WallCheckDistance,
                        Core.CollisionDetection.WhatIsGround
                    );
            yDist = -yHit.distance;
        }

        workspace.Set(
            Core.CollisionDetection.WallCheck.position.x + xDist * Core.Movement.FacingDirection,
            Core.CollisionDetection.LedgeCheckHorizontal.position.y - yDist
        );
        return workspace;
    }
    #endregion
}
