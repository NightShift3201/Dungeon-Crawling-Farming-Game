using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject panel;
    public bool isActive = false;
    public GameObject hotbar;
    public GameObject equipmentScreen;

    public bool canOpenInventory = true;

    public bool onFarm;

    public PlantManager PlantManager;



    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0){
            onFarm = true;
        }
        else{
            onFarm = false;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(canOpenInventory && Input.GetKeyDown(KeyCode.E)){
            ShowInventory();

        }

    }

    public void ShowInventory(){
        isActive = panel.activeSelf;
        panel.SetActive(!isActive);
        if(hotbar)
            hotbar.SetActive(isActive);
        if(equipmentScreen)
            equipmentScreen.SetActive(!isActive);


    }

}
