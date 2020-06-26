using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explanation : ScrollRect
{
    private float pageWidth;
    private int prevPageIndex = 0;

    protected override void Awake()
    {
        base.Awake();

        GridLayoutGroup grid = content.GetComponent<GridLayoutGroup>();
        pageWidth = grid.cellSize.x + grid.spacing.x;
    }

    public void NextPage()
    {if (prevPageIndex < 1)
        {
            prevPageIndex++;
        }
        PageChange();
    }

    public void BackPage()
    {
        if (prevPageIndex > 0)
        {
            prevPageIndex--;
        }
        PageChange();
    }

    void PageChange()
    {
        float destX = -(prevPageIndex * pageWidth);
        content.anchoredPosition = new Vector2(destX, content.anchoredPosition.y);
    }
}
