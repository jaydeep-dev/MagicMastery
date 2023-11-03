using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimationController : MonoBehaviour
{
    private int xMoveHash = Animator.StringToHash("xMove");
    private int yMoveHash = Animator.StringToHash("yMove");
    private int isWalkingHash = Animator.StringToHash("IsWalking");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnMove(InputValue input)
    {
        var moveVector = input.Get<Vector2>();

        animator.SetBool(isWalkingHash, moveVector != Vector2.zero);
        animator.SetFloat(xMoveHash, moveVector.x);
        animator.SetFloat (yMoveHash, moveVector.y);
    }
}
