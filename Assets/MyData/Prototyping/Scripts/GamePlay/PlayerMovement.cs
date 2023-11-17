using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private float moveSpeedMultiplier = 1f;
    private Rigidbody2D rb;
    private Vector2 moveVector = Vector2.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //rb.MovePosition(Time.fixedDeltaTime * moveSpeed * moveVector + rb.position);
        rb.velocity = moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime * moveVector;
    }

    public void SetMoveSpeedMultiplier(float multiplier) => moveSpeedMultiplier = multiplier;

    /// <summary>
    /// Will be called from PlayerInput Component
    /// </summary>
    /// <param name="input">The input value and type defined in InputActionMap</param>
    private void OnMove(InputValue input)
    {
        moveVector = input.Get<Vector2>();
    }
}
