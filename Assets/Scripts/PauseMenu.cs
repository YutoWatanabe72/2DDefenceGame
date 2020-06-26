using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public string changeSceneName = "TitleScene";

    public SceneFader sceneFader;

    /// <summary>
    /// ポーズボタン押印時、ポーズメニューを表示
    /// </summary>
    public void Pause()
    {
        StartCoroutine(OnDelay());
    }

    public void Continue()
    {
        StartCoroutine(OffDelay(0));
    }

    public void Retry()
    {
        StartCoroutine(OffDelay(1));
    }

    public void Title()
    {
        StartCoroutine(OffDelay(2));
    }

    /// <summary>
    /// 時間差でポーズ画面表示
    /// </summary>
    /// <returns></returns>
    IEnumerator OnDelay()
    {
        yield return new WaitForSeconds(0.4f);
        ui.SetActive(!ui.activeSelf);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 時間差でポーズ画面をフェードアウト
    /// </summary>
    /// <param name="n">シーン番号</param>
    /// <returns></returns>
    IEnumerator OffDelay(int n)
    {
        Time.timeScale = 1f;
        ui.GetComponent<Animator>().SetTrigger("Off");
        yield return new WaitForSeconds(1.5f);
        ui.SetActive(!ui.activeSelf);
        if (n == 1)
        {
            sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        }
        else if (n == 2)
        {
            sceneFader.FadeTo(changeSceneName);
        }

    }
}
