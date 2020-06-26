using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [Header("Disp UI")]
    public GameObject ui;
    public Text upgradeCost;
    public Button upgradeButton;
    public Text classChangeCost;
    public Button classChangeButton;
    public Text upgradeCount;
    public GameObject list;
    public GameObject listchild;
    
    public GameObject prefab;//クラスチェンジボタンのプレハブ
    public GameObject element;

    private Node target;
    private bool dispOn;

    Defenders defenders;

    //クラスチェンジ時の選択肢の画像、テキスト
    public Sprite[] sprites;
    public string[] jobList;

    void Start()
    {
        dispOn = false;
        defenders = GameObject.Find("GameManager").GetComponent<Defenders>();
    }

    void Update()
    {
        if (!dispOn)
            return;

        transform.position = target.GetPlacementPosUI();

        upgradeCost.text = "$" + target.defenderBlueprint.upgradeCost;
        upgradeButton.interactable = true;
        classChangeCost.text = "DONE";
        classChangeButton.interactable = false;

        if (PlayerStetas.Money < target.defenderBlueprint.upgradeCost)
        {
            upgradeCost.text = "NEED $" + target.defenderBlueprint.upgradeCost;
            upgradeButton.interactable = false;
        }

        if (target.isUpdradedCount == 3)
        {
            if (target.defenderBlueprint.classChangeCost > 0)
            {
                upgradeCost.text = "DONE";
                upgradeButton.interactable = false;
            }

            if (target.defenderBlueprint.classChangeCost <= PlayerStetas.ClassChangeFragment && target.defenderBlueprint.classChangeCost > 0)
            {
                classChangeCost.text = "￠" + target.defenderBlueprint.classChangeCost;
                classChangeButton.interactable = true;
            }
            else
            {
                classChangeCost.text = "NEED ￠" + target.defenderBlueprint.classChangeCost;
            }
        }

        upgradeCount.text = "UPGRADE COUNT : " + target.isUpdradedCount;
    }

    /// <summary>
    /// ノードの表示
    /// </summary>
    /// <param name="_target"></param>
    public void SetTarget(Node _target)
    {
        target = _target;

        dispOn = true;

        ui.SetActive(true);
    }

    /// <summary>
    /// ノードの非表示
    /// </summary>
    public void Hide()
    {
        ui.SetActive(false);
        dispOn = false;
    }

    /// <summary>
    /// ディフェンダーのアップグレード
    /// </summary>
    public void Upgrade()
    {
        target.UpgradeDefender();
        PlacementManager.instance.DeselectedNode();
    }

    /// <summary>
    /// ディフェンダーのクラスチェンジ
    /// </summary>
    public void ClassChange()
    {
        int n1 = target.defenderBlueprint.classChangePrefab.Length;
        int n2 = target.defenderBlueprint.charNum;

        for (int i = 0; i < n1; i++)
        {
            element = Instantiate(prefab) as GameObject;
            element.GetComponent<ClassChange>().SetData(i, n2);
            if (n2 == 1)
            {
                element.transform.GetChild(0).GetComponent<Image>().sprite = sprites[i];
                element.transform.GetChild(1).GetComponent<Text>().text = jobList[i];
            }
            else if (n2 == 2)
            {
                element.transform.GetChild(0).GetComponent<Image>().sprite = sprites[3];
                element.transform.GetChild(1).GetComponent<Text>().text = jobList[3];
            }
            else if (n2 == 3)
            {
                element.transform.GetChild(0).GetComponent<Image>().sprite = sprites[4];
                element.transform.GetChild(1).GetComponent<Text>().text = jobList[4];
            }
            element.transform.SetParent(listchild.transform, false);
        }
        list.SetActive(true);

        defenders.NodeSet(target);
        PlacementManager.instance.DeselectedNode();
    }
}
