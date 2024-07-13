using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isProjectile : MonoBehaviour
{
    Rigidbody2D myRB;
    float speed;

    void Awake() {
        // speed = boss
        speed = Random.Range(5f, 10f);
        myRB = GetComponent<Rigidbody2D>();
        myRB.AddForce(new Vector2(-1,2)*speed, ForceMode2D.Impulse);
            // Debug.Log(transform.localScale.x);
            // Debug.Log(transform.localRotation.y);
            // if(transform.localRotation.y == -1){
            //     // Debug.Log("shoot left");
            // }
            // else if (transform.localRotation.y == 0){
            //     // Debug.Log("shoot right");
            //     myRB.AddForce(new Vector2(1,0)*speed, ForceMode2D.Impulse);
            // }        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
