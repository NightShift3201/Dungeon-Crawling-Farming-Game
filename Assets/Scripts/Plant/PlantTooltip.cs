using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlantTooltip : MonoBehaviour
{
    public GameObject tooltip;
    public Tilemap tilemap; // Reference to your Tilemap component
    public TilemapCollider2D tilemapCollider; // Reference to your TilemapCollider2D component

    private bool isHovering;
    private float hoverTime;
    private const float hoverDelay = 0.5f;
    private Vector3Int previousCellPosition;


    public PlantManager PlantManager;

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

        if (tilemapCollider.OverlapPoint(mousePosition2D))
        {
            Vector3Int cellPosition = tilemap.WorldToCell(mousePosition);
            TileBase tile = tilemap.GetTile(cellPosition);

            if (tile != null && cellPosition == previousCellPosition)
            {
                if (!isHovering)
                {
                    hoverTime += Time.deltaTime;
                    if (hoverTime >= hoverDelay)
                    {
                        isHovering = true;
                        ShowToolTip(cellPosition);
                    }
                }
            }
            else
            {
                ResetHover();
            }

            previousCellPosition = cellPosition;
        }
        else
        {
            ResetHover();
            HideToolTip();
        }
    }

    void ResetHover()
    {
        isHovering = false;
        hoverTime = 0f;
        HideToolTip();
    }

    void ShowToolTip(Vector3Int cellPosition)
    {
        Vector3 worldPosition = tilemap.CellToWorld(cellPosition) + new Vector3(2.5f, 0f, 0f);
        Vector3 tooltipPosition = Camera.main.WorldToScreenPoint(worldPosition);

        RectTransform tooltipRectTransform = tooltip.GetComponent<RectTransform>();

        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(tooltipRectTransform.parent.GetComponent<RectTransform>(), tooltipPosition, null, out anchoredPosition);

        tooltipRectTransform.anchoredPosition = anchoredPosition;
        Plant plant = PlantManager.FindPlant(cellPosition);
        tooltip.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = plant.name +" Plant";
        if(plant.harvestable){
            tooltip.transform.GetChild(1).gameObject.SetActive(false);
            tooltip.transform.GetChild(2).gameObject.SetActive(true);
            tooltip.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = plant.timeToNextStage +" minutes to next stage";
            tooltip.transform.GetChild(3).gameObject.SetActive(false);
            tooltip.transform.GetChild(4).gameObject.SetActive(false);
        }
        else if(plant.timeWatered<=0){
            tooltip.transform.GetChild(1).gameObject.SetActive(true);
            tooltip.transform.GetChild(2).gameObject.SetActive(false);
            tooltip.transform.GetChild(3).gameObject.SetActive(false);
            tooltip.transform.GetChild(4).gameObject.SetActive(false);
        }
        else{
            tooltip.transform.GetChild(1).gameObject.SetActive(false);
            tooltip.transform.GetChild(2).gameObject.SetActive(true);
            tooltip.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = plant.timeToNextStage +" minutes to next stage";
            tooltip.transform.GetChild(3).gameObject.SetActive(true);
            tooltip.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "watered for "+plant.timeWatered +" minutes";
            tooltip.transform.GetChild(4).gameObject.SetActive(true);
            tooltip.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Stage " + plant.stageIndex;
        }
        //needs water
        //harvestable
        //time finished
        //amount of water
        //stage
        //expected harvest
        tooltip.SetActive(true);
    }

    public GameTimestamp PlantHarvest(){
        return null;
    }



    void HideToolTip(){
        tooltip.SetActive(false);
    }
}
