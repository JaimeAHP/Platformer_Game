using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileController : MonoBehaviour
{
    Rigidbody2D myRB;
    public float speed;

    // Start is called before the first frame update
    void Awake() {
        myRB = GetComponent<Rigidbody2D>();
        // Debug.Log(transform.localScale.x);
        // Debug.Log(transform.localRotation.y);
        if(transform.localRotation.y == -1){
            // Debug.Log("shoot left");
            myRB.AddForce(new Vector2(-1,0)*speed, ForceMode2D.Impulse);
        }
        else if (transform.localRotation.y == 0){
            // Debug.Log("shoot right");
            myRB.AddForce(new Vector2(1,0)*speed, ForceMode2D.Impulse);
        }
    }

    // void OnCollisionEnter2D(Collider2D collision) {
    //     if (collision.gameObject.CompareTag("Enemy")) {

    //     }
    // }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            other.gameObject.GetComponent<enemyHealth>().TakeDamage();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
