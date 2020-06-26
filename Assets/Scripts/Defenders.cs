using UnityEngine;

public class Defenders : MonoBehaviour
{
    [Header("Defender's Setting")]
    public DefenderBlueprint villager;
    public DefenderBlueprint swordsman;
    public DefenderBlueprint rover;
    public DefenderBlueprint reaper;
    public DefenderBlueprint knight;
    public DefenderBlueprint pirate;

    public GameObject classChangeList;
    [SerializeField]
    private Transform classChangeListParent;

    PlacementManager placementManager;
    private Node target;

    void Start()
    {
        placementManager = PlacementManager.instance;
    }

    /// <summary>
    /// ノードデータの更新
    /// </summary>
    /// <param name="_target"></param>
    public void NodeSet(Node _target)
    {
        target = _target;
    }

    public void Villager()
    {
        placementManager.SelectDefenderToPlacemant(villager);
    }

    public void Swordsman()
    {
        placementManager.SelectDefenderToPlacemant(swordsman);
        target.ClassChangeDefender(0);
        ListOff();
    }

    public void Rover()
    {
        placementManager.SelectDefenderToPlacemant(rover);
        target.ClassChangeDefender(1);
        ListOff();
    }

    public void Reaper()
    {
        placementManager.SelectDefenderToPlacemant(reaper);
        target.ClassChangeDefender(2);
        ListOff();
    }

    public void Knight()
    {
        placementManager.SelectDefenderToPlacemant(knight);
        target.ClassChangeDefender(0);
        ListOff();
    }

    public void Pirate()
    {
        placementManager.SelectDefenderToPlacemant(pirate);
        target.ClassChangeDefender(0);
        ListOff();
    }

    public void ListOff()
    {
        classChangeList.SetActive(false);
        foreach(Transform c in classChangeListParent)
        {
            Destroy(c.gameObject);
        }
    }
}
