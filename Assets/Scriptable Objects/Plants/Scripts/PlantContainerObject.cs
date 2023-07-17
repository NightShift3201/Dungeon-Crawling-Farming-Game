using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlantContainer", menuName = "PlantContainer")]
public class PlantContainerObject : ScriptableObject
{
    public List<Plant> Container = new List<Plant>();
}
