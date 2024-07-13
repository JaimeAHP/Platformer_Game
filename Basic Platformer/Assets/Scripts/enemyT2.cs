using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyT2 : MonoBehaviour
{
    //Attacking variables
    public LayerMask attackLayer;
    public Transform attackPoint;
    public Transform detectPoint;
    public Transform rangePoint;
    public float interval;
    public float start;
    public float attackRange = 0.5f;
    public float detectRange = 1f;
    public float detectRangeX = 1f;
    public float detectRangeY = 1f;

    //Movement variables
    public float maxSpeed;
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public bool move;

    public float attackRate;
    float nextAttack = 0f;

    public float damageNum;
    public float pushForce;
    bool attacking;
    // float nextAttack;

    Animator myAnim;
    Rigidbody2D myRB;
    bool fRight;

    // Start is called before the first frame update
    void Start()
    {
        // nextAttack = 0f;
        // InvokeRepeating("Attack", start, interval);
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        fRight = false;        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D rangePlayer = Physics2D.OverlapBox(rangePoint.position, new Vector2(detectRangeX, detectRangeY), 0f, attackLayer);
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
        else if (!attacking){
            if(rangePlayer != null && rangePlayer.CompareTag("Player") && move){
                // Rigidbody2D playerBody = hitPlayer.GetComponent<Rigidbody2D>();
                // if (playerBody != null) {
                //     playerBody.GetComponent<playerController>().DamageTaken(damageNum, pushForce);
                // }
                
                Vector3 direction = (rangePlayer.transform.position - transform.position).normalized;

                float move = direction.x;
                myAnim.SetFloat("speed", Mathf.Abs(move));
                myRB.velocity = new Vector2 (move*maxSpeed, myRB.velocity.y);

                if (move > 0 && !fRight){
                    flip();
                }
                else if (move < 0 && fRight){
                    flip();
                }
            }
        }
    }

    void FixedUpdate(){
        grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, groundLayer);
        myAnim.SetBool("isGrounded", grounded);
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

    void flip() {
        fRight = !fRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
        if(attackPoint == null || detectPoint == null || rangePoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireCube(rangePoint.position, new Vector2 (detectRangeX, detectRangeY));
        Gizmos.DrawWireSphere(detectPoint.position, detectRange);
    }
}
