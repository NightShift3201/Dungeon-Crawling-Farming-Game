using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject inventoryParent;
    public int farmSceneIndex;
    public float transportTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            inventoryParent.GetComponent<PanelOpener>().OpenPanel();
        }

        if(Input.GetKey(KeyCode.LeftShift)){
            transportTime-=Time.deltaTime;
            if(transportTime<=0f)
                SceneManager.LoadScene(farmSceneIndex,LoadSceneMode.Single);
        }
        else{
            transportTime =3f;
        }
    }
}
