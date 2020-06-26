using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    //ディフェンダープレハブ、ノードUIの表示位置を調整
    public Vector3 defenderPositionOffset;
    public Vector3 UIPositionOffset;

    [HideInInspector]
    public GameObject defender;
    public GameObject startDefender;
    [HideInInspector]
    public DefenderBlueprint defenderBlueprint;
    [HideInInspector]
    public int isUpdradedCount = 0;
    [HideInInspector]
    public int classChangedCount = 0;

    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    public Color setCollor;

    [HideInInspector]
    public bool set;

    GameManager gamemane;
    Defenders defenders;
    PlacementManager placementManager;

    public AnimationCurve curve;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        set = false;
        gamemane = GameObject.Find("GameManager").GetComponent<GameManager>();
        defenders = GameObject.Find("GameManager").GetComponent<Defenders>();
        placementManager = PlacementManager.instance;
    }

    void Update()
    {
        if (gamemane.gameIsStart)
        {
            if (!set)
            {
                gameObject.SetActive(false);
                return;
            }
            StartCoroutine(NodeHide());
        } 
    }

    /// <summary>
    /// 防衛者の場所を表示する場所を調整
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPlacementPos()
    {
        return transform.position + defenderPositionOffset;
    }

    /// <summary>
    /// アップグレード、クラスチェンジをするUIを表示する場所の位置調整
    /// </summary>
    /// <returns></returns>
    public Vector3 GetPlacementPosUI()
    {
        return transform.position + UIPositionOffset;
    }

    /// <summary>
    /// マウスボタンを押したとき
    /// </summary>
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        placementManager.ResetSelectDefender();

        if (defender != null)
        {
            placementManager.SelectNode(this);
            return;
        }

        if (!placementManager.CanPlacement)
        {
            PlacementDefender();
        }
    }

    /// <summary>
    /// 防衛者の配置
    /// </summary>
    void PlacementDefender()
    {
        GameObject _defender = Instantiate(startDefender, GetPlacementPos(), Quaternion.identity);
        defender = _defender;
        rend.material.color = setCollor;
        set = true;
        defenders.Villager();
        defenderBlueprint = placementManager.GetDefenderToPlacement();

        GameObject effect = Instantiate(placementManager.placedEffect, GetPlacementPos(), Quaternion.identity);
        Destroy(effect, 5f);

        gamemane.DefenderSelect(this);
        gamemane.defendersNum++;
    }

    /// <summary>
    /// アップグレード
    /// </summary>
    public void UpgradeDefender()
    {
        if (PlayerStetas.Money < defenderBlueprint.upgradeCost)
            return;
        Debug.Log(defenderBlueprint.upgradeCost);

        PlayerStetas.Money -= defenderBlueprint.upgradeCost;

        GameObject effect = Instantiate(placementManager.upgradeEffect, GetPlacementPos(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpdradedCount++;
        defender.GetComponent<Defender>().SetDamage();
        Debug.Log(defenderBlueprint.prefab);

    }

    /// <summary>
    /// クラスチェンジ
    /// </summary>
    public void ClassChangeDefender(int n)
    {
        if (PlayerStetas.ClassChangeFragment < defenderBlueprint.classChangeCost)
            return;

        PlayerStetas.ClassChangeFragment -= defenderBlueprint.classChangeCost;

        Destroy(defender);

        GameObject _defender = Instantiate(defenderBlueprint.classChangePrefab[n], GetPlacementPos(), Quaternion.identity);
        defender = _defender;
        defenderBlueprint = placementManager.GetDefenderToPlacement();

        GameObject effect = Instantiate(placementManager.classChangeEffect, GetPlacementPos(), Quaternion.identity);
        Destroy(effect, 5f);

        isUpdradedCount = 0;
        classChangedCount++;
    }

    /// <summary>
    /// マウスカーソルがノード上になった時
    /// </summary>
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!placementManager.CanPlacement)
            return;

        rend.material.color = hoverColor;
    }

    /// <summary>
    /// マウスカーソルがノード上から離れた時
    /// </summary>
    private void OnMouseExit()
    {
        if (!set)
        {
            rend.material.color = startColor;
        }
        else
        {
            rend.material.color = setCollor;
        }
    }

    /// <summary>
    /// キャラ配置リセット
    /// </summary>
    public void DefenderDel()
    {
        Destroy(defender);
        set = false;
        rend.material.color = startColor;
        gamemane.defendersNum--;
    }

    /// <summary>
    /// キャラが配置されていないノードをゲーム開始時に見えなくし、
    /// ゲーム中に使えないようにする
    /// </summary>
    /// <returns></returns>
    IEnumerator NodeHide()
    {
        if (!set)
        {
            float t = 1f;

            while (t > 0f)
            {
                t -= Time.deltaTime;
                float a = curve.Evaluate(t);
                rend.material.color = new Color(startColor.r, startColor.g, startColor.b, a);
                yield return 0;
            }
        }
    }
}
