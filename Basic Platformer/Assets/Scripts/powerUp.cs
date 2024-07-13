using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    // float pUp;
    public bool pUpDJ;
    public bool pUpS;

    // Start is called before the first frame update
    void Start()
    {
        // pUp = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            playerController pUp = other.gameObject.GetComponent<playerController>();
            if (pUpDJ) {
                pUp.DoubleJump(true);
                Destroy(gameObject);
            }
            else if (pUpS) {
                pUp.Shoot(true);
                Destroy(gameObject);
            }
        }
    }
}
