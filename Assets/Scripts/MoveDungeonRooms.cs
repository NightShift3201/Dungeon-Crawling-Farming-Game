using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveDungeonRooms : MonoBehaviour
{
    
    public int sceneIndex;
    public TimeManager timeManager;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"){
            if(SceneManager.GetActiveScene().buildIndex == 0){
                timeManager.SaveTimeLeft();
            }

            SceneManager.LoadScene(sceneIndex,LoadSceneMode.Single);
        }
    }
}
