using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log : Enemy
{
    private Rigidbody2D rb;
    public Animator anim;
    public Transform target;

    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance() {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius) {

            if (currentState == EnemyState.idle || currentState == EnemyState.walk 
                && currentState != EnemyState.stagger) {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                rb.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("wake_up", true);
            }
            
        }

        else if (Vector3.Distance(target.position, transform.position) > chaseRadius) {
                anim.SetBool("wake_up", false);
            }
    }

    private void SetAnimFloat(Vector2 setVector) {
        anim.SetFloat("Horizontal", setVector.x);
        anim.SetFloat("Vertical", setVector.y);
    }
    private void changeAnim(Vector2 direction) {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
            if (direction.x > 0) {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0) {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y)) {
             if (direction.y > 0) {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0) {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    private void ChangeState(EnemyState newState) {
        if (currentState != newState) {
            currentState = newState;
        }
    }
}
