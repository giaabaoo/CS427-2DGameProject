using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    walk,
    attack,
    interact
}

public enum PlayerWeapon {
    no_weapon,
    bow
}

public class PlayerController : MonoBehaviour
{
    [Space]
    [Header("Character statistics:")]

    public Vector2 player_movement;
    public float player_speed;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator animator;

    public PlayerState currentState;
    public PlayerWeapon currentWeapon;

    void Start() {
        currentState = PlayerState.walk;
        currentWeapon = PlayerWeapon.no_weapon;
        //animator = GetComponent<Animator>();
      //  rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        HandleInputs();
       // Move();
        //Animate();
    }

    void HandleInputs() {
        player_movement = Vector2.zero;
        player_movement.x = Input.GetAxisRaw("Horizontal");
        player_movement.y = Input.GetAxisRaw("Vertical");
        player_speed = Mathf.Clamp(player_movement.magnitude, 5.0f, 10.0f);
        player_movement.Normalize();

        if (Input.GetKeyDown(KeyCode.Q) && currentState != PlayerState.attack) {
            animator.SetInteger("Skill", 1);
            StartCoroutine(AttackCo());
        }

        else if (Input.GetKeyDown(KeyCode.W) && currentState != PlayerState.attack) {
            animator.SetInteger("Skill", 2);
            StartCoroutine(AttackCo());
        }

        else if (currentState == PlayerState.walk) {
            UpdateAnimationAndMove();
        }


      /*  if (Input.GetKeyDown(KeyCode.Alpha1)) {
            weapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            weapon = 2;
        }*/
    }

    private IEnumerator AttackCo() {
        animator.SetBool("Attack", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(.1f);
        currentState = PlayerState.walk;
    }

    void Move(){
        rb.MovePosition(rb.position + player_movement * player_speed * Time.fixedDeltaTime);
    }

    void UpdateAnimationAndMove() {
        if (player_movement != Vector2.zero) {
            Move();
            animator.SetFloat("Horizontal", player_movement.x);
            animator.SetFloat("Vertical", player_movement.y);
        }

    /*    if (weapon == 1) {
            animator.SetInteger("Weapon", 1);
        }

        if (weapon == 2) {
            animator.SetInteger("Weapon", 2);
        }*/
      
        animator.SetFloat("Speed", player_movement.sqrMagnitude);

    }
}
