using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    //Attacking variables
    public LayerMask attackLayer;
    public Transform attackPoint;
    public Transform detectPoint;
    public float interval;
    public float start;
    public float attackRange = 0.5f;
    public float detectRange = 1f;

    public float attackRate;
    float nextAttack = 0f;

    public float damageNum;
    public float pushForce;
    bool attacking;
    // float nextAttack;

    Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        // nextAttack = 0f;
        // InvokeRepeating("Attack", start, interval);
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D detectPlayer = Physics2D.OverlapCircle(detectPoint.position, detectRange, attackLayer);

        if(detectPlayer != null && detectPlayer.CompareTag("Player")){
            // Rigidbody2D playerBody = hitPlayer.GetComponent<Rigidbody2D>();
            // if (playerBody != null) {
            //     playerBody.GetComponent<playerController>().DamageTaken(damageNum, pushForce);
            // }
            attacking = true;
        }
        else {
            attacking = false;
        }
        // else if (detectPlayer == null || !detectPlayer.CompareTag("Player")) {
        //     attacking = false;
        // }

        if (attacking){
            if (Time.time >= nextAttack) {
                Attack();
                nextAttack = Time.time + 1f/attackRate;
            }
        }
    }

    void Attack() {
        myAnim.SetTrigger("Attack");
        // attacking = true;
        Collider2D hitPlayer = Physics2D.OverlapCircle(attackPoint.position, attackRange, attackLayer);
        
        if(hitPlayer != null && hitPlayer.CompareTag("Player")){
            Rigidbody2D playerBody = hitPlayer.GetComponent<Rigidbody2D>();
            if (playerBody != null) {
                playerBody.GetComponent<playerController>().DamageTaken(damageNum, pushForce);
            }
        }
    }

    // void OnTriggerStay2D(Collider2D other) {
    //     if (other.tag == "Player") {
            
    //     }
    // }

    // void OnTriggerEnter2D (Collider2D other){
    //     if(attacking) {
    //         if(other.tag = "Player" && nextAttack<Time.time) {


    //         }
    //     }
    // }

    // public void Die() {
    //     Destroy(gameObject);      
    // }

    void OnDrawGizmosSelected(){
        if(attackPoint == null || detectPoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(detectPoint.position, detectRange);
    }
}
