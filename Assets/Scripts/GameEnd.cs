using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    public string changeSceneName = "TitleScene";

    public SceneFader sceneFader;

    /// <summary>
    /// シーン遷移（タイトル）
    /// </summary>
    public void Title()
    {
        StartCoroutine(Delay(1));
    }
    /// <summary>
    /// シーン遷移（リトライ）
    /// </summary>
    public void Retry()
    {
        StartCoroutine(Delay(2));
    }

    IEnumerator Delay(int n)
    {
        yield return new WaitForSeconds(0.5f);
        if (n == 1)
        {
            sceneFader.FadeTo(changeSceneName);
        }
        else if(n == 2)
        {
            sceneFader.FadeTo(SceneManager.GetActiveScene().name);
        }
    }
}
