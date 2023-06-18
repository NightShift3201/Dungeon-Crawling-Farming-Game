using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LeaveDungeon : MonoBehaviour
{
    public float transportTime = 3f;
    public int farmSceneIndex = 1;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = 3f;
        slider.value=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift)){
            transportTime-=Time.deltaTime;
            setTime(3f-transportTime);
            if(transportTime<=0f)
                SceneManager.LoadScene(farmSceneIndex,LoadSceneMode.Single);
        }
        else{
            transportTime =3f;
            setTime(0f);
        }
        
    }


    public void setTime(float time){
        slider.value=time;
    }


}
