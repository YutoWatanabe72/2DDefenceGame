using UnityEngine;

public class ClassChange : MonoBehaviour
{

    Defenders defenders;

    int charNum;
    int orderNum;

    /// <summary>
    /// データのセット
    /// </summary>
    /// <param name="num1">キャラ番号</param>
    /// <param name="num2">ボタン表示順</param>
    public void SetData(int num1, int num2)
    {
        orderNum = num1;
        charNum = num2;
    }

    void Start()
    {
        defenders = GameObject.Find("GameManager").GetComponent<Defenders>();
    }

    /// <summary>
    /// ディフェンダーのプレハブ変更
    /// </summary>
    public void ClassToChange()
    {
        switch (charNum)
        {
            case 1:
                switch (orderNum)
                {
                    case 0:
                        defenders.Swordsman();
                        break;
                    case 1:
                        defenders.Rover();
                        break;
                    case 2:
                        defenders.Reaper();
                        break;

                    default:
                        break;
                }
                break;

            case 2:
                defenders.Knight();
                break;
            case 3:
                defenders.Pirate();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// キャンセルボタン
    /// </summary>
    public void Cancel()
    {
        defenders.ListOff();
    }
}
