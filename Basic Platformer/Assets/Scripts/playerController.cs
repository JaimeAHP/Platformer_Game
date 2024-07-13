using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    // Movement variables
    public float maxSpeed;
    public int levelReset;

    //Jumping variables
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    //Double jump variables
    bool dJumpPUp = false;
    // bool isDJump = true;
    float dJump = 0;
    public float doubleJHeight;

    //Attacking variables
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float ttk;
    public float attackRate;
    float nextAttack = 0f;

    //Health variables
    public float health;
    bool playable;

    //Shooting variables
    public Transform shootPoint;
    public GameObject projectile;
    public float fireRate = 0.5f;
    public float ammo;
    float maxAmmo = 4;
    float nextShoot = 0f;
    bool shootPUp = false;

    //HUD variables
    // public Image hearts;
    public Animator myHearts;

    Rigidbody2D myRB;
    Animator myAnim;
    bool fRight;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 3f;
        myRB = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        // theHearts = GetComponent<Image>();
        // myHearts = heartsAnim.GetComponent<Animator>();
        fRight = true;
        playable = true;
                
    }

    //Update is called once per frame
    void Update()
    {
        if (playable){
            if (Time.time >= nextAttack) {
                if (Input.GetKeyDown(KeyCode.J) && playable){
                    Attack();
                    nextAttack = Time.time + 1f/attackRate;
                }
            }

            if (Time.time >= nextShoot && ammo > 0 && shootPUp) {
                if (Input.GetKeyDown(KeyCode.K) && playable){
                    nextShoot = Time.time + fireRate;
                    if (fRight) {
                        // GameObject insProjectile = Instantiate(projectile, shootPoint.position, Quaternion.identity);
                        // insProjectile.transform.localScale =  new Vector3(2f, 2f, 1f);
                        Instantiate(projectile, shootPoint.position, Quaternion.Euler(new Vector3(0,0,0)));
                        ammo -= 1;
                    }
                    else if (!fRight) {
                        // GameObject insProjectile = Instantiate(projectile, shootPoint.position, Quaternion.identity);
                        // insProjectile.transform.localScale =  new Vector3(-2f, 2f, 1f);
                        Instantiate(projectile, shootPoint.position, Quaternion.Euler(new Vector3(0,-180,0)));
                        ammo -= 1;
                    }
                }
            }

            if(grounded && Input.GetKeyDown(KeyCode.W)) {
                grounded = false;
                myAnim.SetBool("isGrounded", grounded); 
                myRB.AddForce((new Vector2(0f, 1f)) * jumpHeight);

                // myAnim.SetBool("isDJump", false);
            //     else{
            //         grounded = false;
            //         myAnim.SetBool("isGrounded", grounded); 
            //     }
            }

            if (Input.GetKeyDown(KeyCode.Space) && !grounded && dJump == 1 && dJumpPUp) { 
                // Debug.Log("dJump");
                myAnim.SetBool("isDJump", dJumpPUp);
                myRB.velocity = Vector2.zero;
                myRB.AddForce((new Vector2(0f, 1f)) * doubleJHeight);
                dJump = 0;
            }
            
        }
    }

    // Update is called at set times
    void FixedUpdate()
    {
        if (playable) {
            //check if grounded
            grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius, groundLayer);
            myAnim.SetBool("isGrounded", grounded);
            if (grounded) {
                myAnim.SetBool("isDJump", false);
                dJump = 1;
            }

            myAnim.SetFloat("vSpeed", myRB.velocity.y);

            float move = Input.GetAxis("Horizontal");
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

    void Attack() {
        myAnim.SetTrigger("Attack");
        Invoke("EnemyDie", ttk);
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        // foreach(Collider2D enemy in hitEnemies){
        //     Ienemy.GetComponent<enemy>().Die();
        //     // Debug.Log("enemy die");
        // }
    }

    // void Shoot() {
    //     myAnim.SetTrigger("Shoot");
    //     ammo -= 1;
    //     Instantiate(projectile, shootPoint.position, Quaternion.Euler(new Vector3(0,0,0)));
    // }

    void EnemyDie() {
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        // foreach(Collider2D enemy in hitEnemies){
        //     enemy.GetComponent<enemy>().Die();
        //     // Debug.Log("enemy die");
        // }
        Collider2D hitEnemies = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);

        if(hitEnemies != null && hitEnemies.CompareTag("Enemy")){
            Rigidbody2D enemyBody = hitEnemies.GetComponent<Rigidbody2D>();
            if (enemyBody != null) {
                enemyBody.GetComponent<enemyHealth>().Die();
                if (health == 2){
                    health += 1;
                    myHearts.SetBool("healthChange", false);
                    // myHearts.SetFloat("health", 0f);
                }
                else if (health == 1){
                    health += 1;
                    myHearts.SetBool("healthChange", true);
                    myHearts.SetFloat("health", 0f);
                }

                if (ammo <= 4) {
                    ammo += 2;
                    if (ammo > 4) {
                        ammo = maxAmmo;
                    }
                }
            }
        }
    }

    void flip() {
        fRight = !fRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void DamageTaken(float damageNum, float pushForce){
        // Debug.Log(health);
        // Debug.Log(damageNum);
        health = health - damageNum;
        // Debug.Log(health);
        if (health>0f) {
            myAnim.SetTrigger("Damage");
            if (health == 2){
                myHearts.SetBool("healthChange", true);
                myHearts.SetFloat("health", 0f);
            }
            else if (health == 1){
                myHearts.SetBool("healthChange", true);
                myHearts.SetFloat("health", 0.5f);
            }

            Vector2 pushDirection = new Vector2(1f, 1f).normalized;
            myRB.velocity = Vector2.zero;
            myRB.AddForce(pushDirection*pushForce, ForceMode2D.Impulse);
        }
        else if (health == 0){
            Die();
        }
    }

    public void Die() {
        myHearts.SetBool("healthChange", true);
        myHearts.SetFloat("health", 1f);
        playable = false;
        myAnim.SetBool("isDJump", false);
        myAnim.SetBool("isGrounded", true);
        myAnim.SetTrigger("Dead");
        Destroy(myRB);
        Invoke("End", 2f);
    }

    void End() {
        SceneManager.LoadScene(levelReset, LoadSceneMode.Single);
    }

    public void DoubleJump(bool pUpDJ) {
        // Debug.Log(pUpDJ);
        // Debug.Log(dJumpPUp);
        dJumpPUp = pUpDJ;
        // Debug.Log(dJumpPUp);
    }

    public void Shoot(bool pUpS) {
        shootPUp = pUpS;
        ammo = 4;
    }

    void OnDrawGizmosSelected(){
        if(attackPoint == null){
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
