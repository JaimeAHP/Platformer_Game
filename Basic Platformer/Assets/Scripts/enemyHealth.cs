using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float health;
    // public bool isProjectile;
    // public float speed;
    Animator myAnim;
    Rigidbody2D myRB;
    Collider2D myCD;

    // void Awake() {
    //     if (isProjectile) {
    //         myRB = GetComponent<Rigidbody2D>();
    //         myRB.AddForce(new Vector2(-1,1)*speed, ForceMode2D.Impulse);
    //         // Debug.Log(transform.localScale.x);
    //         // Debug.Log(transform.localRotation.y);
    //         // if(transform.localRotation.y == -1){
    //         //     // Debug.Log("shoot left");
    //         // }
    //         // else if (transform.localRotation.y == 0){
    //         //     // Debug.Log("shoot right");
    //         //     myRB.AddForce(new Vector2(1,0)*speed, ForceMode2D.Impulse);
    //         // }
    //     }
        
    // }

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody2D>();
        myCD = GetComponent<Collider2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage() {
        health -= 1;
        if (health == 0) {
            Destroy(myRB);
            Destroy(myCD);
            // Debug.Log("Die");
            myAnim.SetTrigger("isDead");
            Invoke("Die", 1f);
        }
        // else {
        //     Debug.Log(health);
        // }
    }

    public void Die() {
        Destroy(gameObject);      
    }

    public float getHealth() {
        return health;
    }

    // void End() {
    //     Destroy(gameObject);
    // }
}
