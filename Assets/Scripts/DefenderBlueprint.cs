using UnityEngine;

[System.Serializable]
public class DefenderBlueprint
{
    public GameObject prefab;
    public int charNum;
    public int upgradeCost;
    public bool canClassChange;

    public GameObject[] classChangePrefab;
    public int classChangeCost;

}