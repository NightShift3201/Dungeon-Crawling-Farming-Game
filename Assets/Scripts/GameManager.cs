using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panel;
    public bool isActive = false;
    public GameObject hotbar;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            if(panel!=null){
                isActive = panel.activeSelf;
                panel.SetActive(!isActive);
                if(hotbar)
                    hotbar.SetActive(isActive);
            }

        }

    }
}
