using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartLogo : MonoBehaviour
{
    Image image;
    public AnimationCurve curve;
    Color startcolor;

    float countDown;
    
    void Start()
    {
        image = GetComponent<Image>();
        image.fillAmount = 0;
        startcolor = image.color;

        countDown = 0f;
    }

    void Update()
    {
        countDown += Time.deltaTime;
        if (countDown >= 1)
        {
            image.fillAmount += (Time.deltaTime * 0.25f);

            if (image.fillAmount >= 1)
            {
                StartCoroutine(FadeOut());
            }
        }
    }
    IEnumerator FadeOut()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            image.color = new Color(startcolor.r, startcolor.g, startcolor.b, a);
            yield return 0;
        }
    }
}
