using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Character attributes:")]
    public float PLAYER_BASE_SPEED = 1.0f;

    [Space]
    [Header("Character statistics:")]

    public Vector2 player_movement;
    public float player_speed;

    [Space]
    [Header("Weapon [1 No weapon] [2 Bow]: ")]
    int weapon = 0;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

    void Update()
    {
        HandleInputs();
        Move();
        Animate();
    }

    void HandleInputs() {
        player_movement.x = Input.GetAxisRaw("Horizontal");
        player_movement.y = Input.GetAxisRaw("Vertical");
        player_speed = Mathf.Clamp(player_movement.magnitude, 0.0f, 1.0f);
        player_movement.Normalize();

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            weapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            weapon = 2;
        }
    }

    void Move(){
     //   rb.velocity = player_movement * player_speed * PLAYER_BASE_SPEED;
        rb.MovePosition(rb.position + player_movement * player_speed * Time.fixedDeltaTime);
    }

    void Animate() {
        if (player_movement != Vector2.zero) {
            animator.SetFloat("Horizontal", player_movement.x);
            animator.SetFloat("Vertical", player_movement.y);
        }

        if (weapon == 1) {
            animator.SetInteger("Weapon", 1);
        }

        if (weapon == 2) {
            animator.SetInteger("Weapon", 2);
        }
      
        animator.SetFloat("Speed", player_movement.sqrMagnitude);

    }
}
