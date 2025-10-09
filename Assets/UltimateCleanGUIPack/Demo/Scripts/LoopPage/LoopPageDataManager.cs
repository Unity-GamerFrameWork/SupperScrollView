using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopPageDataManager : MonoBehaviour
{

    public static LoopPageDataManager Instance;
    public List<PageData> PageDataList { get; private set; } = new List<PageData>();
    private void Awake()
    {
        Instance = this;
        AddPageData(3);
    }
    private void OnDisable()
    {
        Instance = null;
    }
    public void AddPageData(int count)
    {
        for (int i = 0; i < count; i++)
        {
            PageDataList.Add(new PageData
            {
                id = i + 1,
                title = $"Page_{i+1}",
            });

        }
    }
}
[Serializable]
public class PageData
{
    public int id;
    public string title;
}
