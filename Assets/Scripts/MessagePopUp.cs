using System.Collections;
using UnityEngine;

public class MessagePopUp : MonoBehaviour
{
    public GameManager gamemane;
    Animator animator;
    public bool animOn;

    void Start()
    {
        animator = GetComponent<Animator>();
        animOn = false;
    }

    void Update()
    {
        if (!animOn)
            return;
    }

    /// <summary>
    /// OKボタンをクリック
    /// ウェーブスタート
    /// </summary>
    public void Ok()
    {
        PopAction();
        gamemane.GameStart();
    }

    /// <summary>
    /// NOボタンをクリック
    /// 最後に配置した防衛者のみ削除して配置を続ける状態にする
    /// </summary>
    public void No()
    {
        PopAction();
        gamemane.Cancel();
    }

    /// <summary>
    /// OK、NOの共通アクション
    /// </summary>
    void PopAction()
    {
        animOn = true;
        animator.SetBool("PopDown",true);
    }
}
