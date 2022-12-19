using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private string runAnimation;
    [SerializeField] private string idleAnimation;
    private PlayerController playerController;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Awake() {
        playerController = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate() {
        if(playerController.Velocity.x == 0)
        {
            animator.Play(idleAnimation);
            return;
        }

        spriteRenderer.flipX = playerController.Velocity.x < 0;
        animator.Play(runAnimation);
    }
}
