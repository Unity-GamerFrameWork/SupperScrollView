using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackDataManager : MonoBehaviour
{
    public static BackPackDataManager Instance;
    public List<BackPackData> BackPackDataList { get; private set; } = new List<BackPackData>();
    private void Awake()
    {
        Instance = this;
        AddBackPackData(200);
    }
    private void OnDisable()
    {
        Instance = null;
    }
    public void AddBackPackData(int count)
    {
        for (int i = 0; i < count; i++)
        {
            BackPackData data = new BackPackData();
            if (i <= 22)
            {
                data.id = i + 1;
                data.count = UnityEngine.Random.Range(1, 99);
                data.isUnLock = true;
            }
            BackPackDataList.Add(data);
        }
    }
}
[Serializable]
public class BackPackData
{
    public int id;
    public int count;
    public bool isUnLock;
}
