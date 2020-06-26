using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStetas : MonoBehaviour
{
    public static int Money;
    public int startMoney = 0;

    public static int ClassChangeFragment;
    public int startClassChangeFragment = 0;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds;

    public Text playerMoney;
    public Text playerFragment;
    public Text rounds;
    public Text playerLife;

    void Start()
    {
        Money = startMoney;
        ClassChangeFragment = startClassChangeFragment;
        Lives = startLives;

        Rounds = 0;
    }

    void Update()
    {
        playerMoney.text = "$" + Money.ToString();
        playerFragment.text = "￠" + ClassChangeFragment.ToString();
        rounds.text = Rounds.ToString();
        playerLife.text = "PLAYER LIFE :" + Lives.ToString();
    }
}
