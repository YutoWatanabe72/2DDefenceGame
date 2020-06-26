using UnityEngine;

public class NodesSetActive : MonoBehaviour
{
    public GameObject[] nodes;

    void Start()
    {
        NodesSet();
    }

    /// <summary>
    /// ゲーム開始時にすべてのノードを表示
    /// </summary>
    void NodesSet()
    {
        foreach(var node in nodes)
        {
            node.SetActive(false);
            node.SetActive(true);
        }
    }

}
