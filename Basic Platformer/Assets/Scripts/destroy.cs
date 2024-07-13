using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public bool onPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D other) {
        if(!onPlayer) {
            if (other.tag == "Player"){
                other.gameObject.GetComponent<playerController>().Die();
            }
            else if (other.tag == "Enemy") {
                Destroy(other.gameObject);
            }
        }
    }

    void OnTriggerExit2D (Collider2D other) {
        if (onPlayer) {
            if (other.tag == "projectile") {
                Destroy(other.gameObject);
            }
        }
    }
}
