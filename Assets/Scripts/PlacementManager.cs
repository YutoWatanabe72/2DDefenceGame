using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public static PlacementManager instance;

    /// <summary>
    /// インスタンスの生成
    /// </summary>
    void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }

    public GameObject placedEffect;
    public GameObject upgradeEffect;
    public GameObject classChangeEffect;

    private DefenderBlueprint defenderToPlacement;
    private Node selectedNode;

    public NodeUI nodeUI;

    /// <summary>
    /// すでにディフェンダーが設置されているか判断
    /// </summary>
    public bool CanPlacement { get { return defenderToPlacement != null; } }

    /// <summary>
    /// ノードの選択
    /// </summary>
    /// <param name="node"></param>
    public void SelectNode(Node node)
    {
        if(selectedNode == node)
        {
            DeselectedNode();
            return;
        }

        selectedNode = node;
        defenderToPlacement = null;

        nodeUI.SetTarget(node);
    }

    /// <summary>
    /// ノードUIの削除
    /// </summary>
    public void DeselectedNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    /// <summary>
    /// ノードのデータ更新
    /// </summary>
    /// <param name="defender"></param>
    public void SelectDefenderToPlacemant(DefenderBlueprint defender)
    {
        defenderToPlacement = defender;
        DeselectedNode();
    }

    /// <summary>
    /// ノードのデータ取得
    /// </summary>
    /// <returns></returns>
    public DefenderBlueprint GetDefenderToPlacement()
    {
        return defenderToPlacement;
    }

    /// <summary>
    /// 選択の解除
    /// </summary>
    public void ResetSelectDefender()
    {
        defenderToPlacement = null;
    }
}
