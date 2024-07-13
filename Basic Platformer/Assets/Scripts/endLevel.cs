using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endLevel : MonoBehaviour
{
    public int sceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")){
            CompleteLevel();
        }
    }

    void CompleteLevel(){
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
}
