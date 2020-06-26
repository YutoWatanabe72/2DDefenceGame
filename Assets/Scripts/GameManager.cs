using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Disp UI")]
    public GameObject Message;
    public GameObject StartText;
    public GameObject allDeleteButton;
    public GameObject titleButton;
    public GameObject pauseButton;
    public GameObject gameOver;
    public GameObject gameClear;

    //設置されているディフェンダーの数
    [HideInInspector]
    public int defendersNum;
    private Node[] Defenders = new Node[8];

    [HideInInspector]
    public bool gameIsStart;
    public static bool GameIsEnd;

    WaveSpawner waveSpawner;
    public MessagePopUp messagePop;

    void Start()
    {
        gameIsStart = false;
        GameIsEnd = false;

        waveSpawner = GetComponent<WaveSpawner>();
        defendersNum = 0;
        StartText.SetActive(false);
        allDeleteButton.SetActive(true);
        titleButton.SetActive(true);
    }

    void Update()
    {
        if (GameIsEnd)
            return;

        if (gameIsStart)
        {
            if(PlayerStetas.Lives <= 0)
            {
                EndGame();
            }
            return;
        }

        if (defendersNum == 8)
        {
            Message.SetActive(true);
        }
    }

    public void GameStart()
    {
        gameIsStart = true;
        waveSpawner.waveStart = true;
        allDeleteButton.SetActive(false);
        titleButton.SetActive(false);
        pauseButton.SetActive(true);
        StartText.SetActive(true);
        StartCoroutine(MessageOFF(6f));
    }

    public void EndGame()
    {
        GameIsEnd = true;
        gameOver.SetActive(true);
    }

    public void GameClear()
    {
        GameIsEnd = true;
        gameClear.SetActive(true);
    }

    public void Cancel()
    {
        Defenders[defendersNum - 1].DefenderDel();
        StartCoroutine(MessageOFF(1f));
    }

    /// <summary>
    /// ゲーム開始時のポップアップの非表示
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    IEnumerator MessageOFF(float n)
    {
        yield return new WaitForSeconds(n);
        Message.SetActive(false);
        Animator messagepopAnim = messagePop.GetComponent<Animator>();
        messagepopAnim.SetBool("PopDown", false);
    }

    /// <summary>
    /// 設置されたノードを配列に保管
    /// </summary>
    /// <param name="node"></param>
    public void DefenderSelect(Node node)
    {
        Defenders[defendersNum] = node;
    }

    /// <summary>
    /// 配列に保管されているノードデータを削除
    /// </summary>
    public void AllDelete()
    {
        int count = defendersNum;
        for(int i = 0; i < count; i++)
        {
            Defenders[defendersNum - 1].DefenderDel();
        }
    }
}
