using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundDisp : MonoBehaviour
{
    Text roundText;
    public AnimationCurve curve;

    float timer;
    public float countTime = 4f;

    void OnEnable()
    {
        roundText = GetComponent<Text>();
        timer = 0;
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (PlayerStetas.Rounds == 10)
        {
            roundText.text = "FINAL ROUND";
        }
        else
        {
            roundText.text = "ROUND : " + PlayerStetas.Rounds.ToString();
        }

        timer += Time.deltaTime;

        if (timer >= 4f)
        {
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            roundText.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }
    }

    IEnumerator FadeOut()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            roundText.color = new Color(0f, 0f, 0f, a);
            yield return 0;
        }

        this.gameObject.SetActive(false);
    }
}
