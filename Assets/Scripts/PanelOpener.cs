using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour
{

    public GameObject panel;
    public bool isActive;
    // Start is called before the first frame update

    public void OpenPanel(){
        if(panel!=null){
            isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
        if(!isActive){
            Time.timeScale = 0f;
        }
        else{
            Time.timeScale = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
