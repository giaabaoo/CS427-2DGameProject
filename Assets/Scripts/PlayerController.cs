using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public enum PlayerWeapon {
    no_weapon,
    bow,
    boomerang,
    spear
}

public class PlayerController : MonoBehaviour
{
    [Space]
    [Header("Character statistics:")]

    public string sceneToLoad;

    public Vector2 player_movement;
    public float player_speed;
    public PlayerState currentState;
    public PlayerWeapon currentWeapon;
    public FloatValue currentHealth;
    public Inventory playerInventory;
    public bool has_bow = false;
    public bool has_boom = false;
    public bool has_spear = true;
    public VectorValue startingPosition;


    [Space]
    [Header("References:")]
    public SpriteRenderer receivedItemSprite;
    public SignalSender playerHealthSignal;
    public GameObject arrowPrefab;
    public GameObject boomPrefab;
    public Transform firePoint;
    public SignalSender reduceMagic;



    public Rigidbody2D rb;
    public Animator animator;

    


    void Start() {
        currentState = PlayerState.walk;
        currentWeapon = PlayerWeapon.no_weapon;
        //transform.position = startingPosition.initialValue;
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
        if (currentState == PlayerState.interact) {
            return;
        }
        ChooseWeapons();

        player_movement = Vector2.zero;
        player_movement.x = Input.GetAxisRaw("Horizontal");
        player_movement.y = Input.GetAxisRaw("Vertical");
        player_speed = Mathf.Clamp(player_movement.magnitude, 5.0f, 10.0f);
        player_movement.Normalize();

        if (Input.GetKeyDown(KeyCode.Q) && currentState != PlayerState.attack && currentState != PlayerState.stagger) {
            animator.SetInteger("Skill", 1);
            
            if (currentWeapon == PlayerWeapon.no_weapon || currentWeapon == PlayerWeapon.spear) {
                StartCoroutine(NormalAttackCo());
                if (currentWeapon == PlayerWeapon.no_weapon)
                    SoundManager.PlaySound("punch");
                else
                    SoundManager.PlaySound("spear");
            }
            else if (currentWeapon == PlayerWeapon.bow) {
                SoundManager.PlaySound("bow_fire_arrow");
                StartCoroutine(BowAttackCo());

            }
            else if (currentWeapon == PlayerWeapon.boomerang) {
                SoundManager.PlaySound("boomerang");
                StartCoroutine(BoomAttackCo());
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

        if (Input.GetKeyDown(KeyCode.Alpha2) && has_bow) {
            currentWeapon = PlayerWeapon.bow;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && has_boom) {
            currentWeapon = PlayerWeapon.boomerang;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && has_spear) {
            currentWeapon = PlayerWeapon.spear;
        }

        if (currentWeapon == PlayerWeapon.no_weapon) {
            animator.SetInteger("Weapon", 0);
        }

        if (currentWeapon == PlayerWeapon.bow) {
            animator.SetInteger("Weapon", 1);
        }

        if (currentWeapon == PlayerWeapon.boomerang) {
            animator.SetInteger("Weapon", 2);
        }

         if (currentWeapon == PlayerWeapon.spear) {
            animator.SetInteger("Weapon", 3);
        }
    }

    private IEnumerator NormalAttackCo() {
        animator.SetBool("Attack", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(.1f);
        if (currentState != PlayerState.interact) {
            currentState = PlayerState.walk;
        }
    }

    private IEnumerator BowAttackCo() {
        animator.SetBool("Attack", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attack", false);
        
        yield return new WaitForSeconds(.3f);
        FireArrow();

        if (currentState != PlayerState.interact) {
            currentState = PlayerState.walk;
        }    
    }

    private IEnumerator BoomAttackCo() {
        animator.SetBool("Attack", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attack", false);
        
        yield return new WaitForSeconds(.3f);
        FireBoom();

        if (currentState != PlayerState.interact) {
            currentState = PlayerState.walk;
        }    
    }

    public void RaiseItem() {

        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                if (playerInventory.currentItem.isBow) {
                    currentState = PlayerState.interact;
                    has_bow = true;
                    receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
                }
                else if (playerInventory.currentItem.isBoom) {
                    has_boom = true;
                }
                else if (playerInventory.currentItem.isSpear) {
                    has_spear = true;
                }
            }
            else
            {
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    private void FireArrow() {
        if (playerInventory.currentMagic > 0){
            Vector2 temp = new Vector2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical"));
            Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ChooseProjectileDirection());
            playerInventory.ReduceMagic(arrow.magicCost);
            reduceMagic.Raise();
        }
        
    }

     private void FireBoom() {
        if (playerInventory.currentMagic > 0){
            Vector2 temp = new Vector2(animator.GetFloat("Horizontal"), animator.GetFloat("Vertical"));
            Boom boom = Instantiate(boomPrefab, transform.position, Quaternion.identity).GetComponent<Boom>();
            boom.Setup(temp, ChooseProjectileDirection());
            playerInventory.ReduceMagic(boom.magicCost);
            reduceMagic.Raise();
        }
        
    }

    Vector3 ChooseProjectileDirection() {
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

    public void Knock(float knockTime, float damage) {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();

        if (currentHealth.RuntimeValue > 0) {
            StartCoroutine(KnockCo(knockTime));
        }
        else {
            SceneManager.LoadScene(sceneToLoad);
            this.gameObject.SetActive(false);

        }
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
