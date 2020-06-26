using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public string loadScene1 = "MainScene";
    [SerializeField]
    GameObject explanationPanel;

    public SceneFader sceneFader;

    /// <summary>
    /// ゲームスタート
    /// </summary>
    public void GameStart()
    {
        sceneFader.FadeTo(loadScene1);
    }

    public void ExplanationOn()
    {
        explanationPanel.SetActive(true);
    }

    public void PageOff()
    {
        explanationPanel.SetActive(false);
    }

    /// <summary>
    /// ゲームスタート
    /// </summary>
    public void End()
    {
        Application.Quit();
    }
}
