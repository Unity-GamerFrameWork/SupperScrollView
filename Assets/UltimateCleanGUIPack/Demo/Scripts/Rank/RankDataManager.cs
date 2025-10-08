using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankDataManager : MonoBehaviour
{
    public static RankDataManager Instance;
    public List<RankData> RankDataList { get; private set; } = new List<RankData>();
    private void Awake()
    {
        Instance = this;
        AddRankData(100);
    }
    private void OnDisable()
    {
        Instance = null;
    }
    public void AddRankData(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = RankDataList.Count;
            RankDataList.Add(new RankData
            {
                ranking = index,
                nickName = $"User_{index}",
                skillCount = UnityEngine.Random.Range(0, 1000),
                gameCount = UnityEngine.Random.Range(0,5000)
            });
        }        
    }
}
[Serializable]
public class RankData
{
    public int ranking; //排名
    public string nickName;//名称
    public int skillCount;//击杀数
    public int gameCount;//游戏次数
    
}
