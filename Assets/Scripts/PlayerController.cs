using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    walk,
    attack,
    interact,
    stagger,
    idle
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
    public PlayerState currentState;
    public PlayerWeapon currentWeapon;
    public FloatValue currentHealth;

    [Space]
    [Header("References:")]
    public GameObject arrowPrefab;
    public Transform firePoint;


    public Rigidbody2D rb;
    public Animator animator;

    


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
        ChooseWeapons();

        player_movement = Vector2.zero;
        player_movement.x = Input.GetAxisRaw("Horizontal");
        player_movement.y = Input.GetAxisRaw("Vertical");
        player_speed = Mathf.Clamp(player_movement.magnitude, 5.0f, 10.0f);
        player_movement.Normalize();

        if (Input.GetKeyDown(KeyCode.Q) && currentState != PlayerState.attack && currentState != PlayerState.stagger) {
            animator.SetInteger("Skill", 1);
            
            if (currentWeapon == PlayerWeapon.no_weapon) {
                StartCoroutine(NormalAttackCo());
            }
            else if (currentWeapon == PlayerWeapon.bow) {
                StartCoroutine(BowAttackCo());
            }
        }

        else if (Input.GetKeyDown(KeyCode.W) && currentState != PlayerState.attack && currentState != PlayerState.stagger) {
            animator.SetInteger("Skill", 2);
             if (currentWeapon == PlayerWeapon.no_weapon) {
                StartCoroutine(NormalAttackCo());
            }
        }



        else if (currentState == PlayerState.walk || currentState == PlayerState.idle) {
            UpdateAnimationAndMove();
        }


     
    }

    void ChooseWeapons() {
         if (Input.GetKeyDown(KeyCode.Alpha1)) {
            currentWeapon = PlayerWeapon.no_weapon;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            currentWeapon = PlayerWeapon.bow;
        }

        if (currentWeapon == PlayerWeapon.no_weapon) {
            animator.SetInteger("Weapon", 0);
        }

        if (currentWeapon == PlayerWeapon.bow) {
            animator.SetInteger("Weapon", 1);
        }
    }

    private IEnumerator NormalAttackCo() {
        animator.SetBool("Attack", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(.1f);
        currentState = PlayerState.walk;
    }

    private IEnumerator BowAttackCo() {
        animator.SetBool("Attack", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attack", false);
        
        yield return new WaitForSeconds(.3f);
        FireArrow();

        currentState = PlayerState.walk;
    }

    private void FireArrow() {
        Vector2 temp = new Vector2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical"));
        Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(temp, ChooseArrowDirection());
    }

    Vector3 ChooseArrowDirection() {
        float temp = Mathf.Atan2(animator.GetFloat("Vertical"), animator.GetFloat("Horizontal"))*Mathf.Rad2Deg;
        return new Vector3(0,0, temp);
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
      
        animator.SetFloat("Speed", player_movement.sqrMagnitude);

    }

    public void Knock(float knockTime) {
        StartCoroutine(KnockCo(knockTime));
    }

    public IEnumerator KnockCo(float knockTime) {
        if (rb != null) {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rb.velocity = Vector2.zero;
        }
    }
}
