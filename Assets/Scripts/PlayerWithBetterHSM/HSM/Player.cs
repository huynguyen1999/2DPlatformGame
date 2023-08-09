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
    public PlayerInventory Inventory { get; private set; }
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


    #endregion
    //-----------------------------//
    #region Unity Callback Functions
    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
        states = new PlayerStateFactory(this, playerData);
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        RB = GetComponent<Rigidbody2D>();
        PlayerCollier = GetComponent<BoxCollider2D>();
        InputHandler = GetComponent<PlayerInputHandler>();
        Inventory = GetComponent<PlayerInventory>();
        DashDirectionIndicator.gameObject.SetActive(false);
        Initialize(states.GroundedState);
        states.PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.Primary]);
        states.SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInputs.Secondary]);
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
        Vector2 workspace = new(xDist * transform.root.localScale.x, 0f);
        RaycastHit2D yHit = Physics2D.Raycast(
            Core.CollisionDetection.LedgeCheckHorizontal.position + (Vector3)(workspace),
            -transform.up,
            1f,
            Core.CollisionDetection.WhatIsGround
        );
        float yDist = yHit.distance;
        workspace.Set(
            Core.CollisionDetection.WallCheck.position.x + (xDist * transform.root.localScale.x),
            Core.CollisionDetection.LedgeCheckHorizontal.position.y - yDist
        );
        return workspace;
    }
    #endregion
}
