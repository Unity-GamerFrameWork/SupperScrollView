<details>
<summary>
# 无限滚动解决方案
UltimateCleanGUIPack:有一套完成的UI模板
## 介绍
1. 什么是无限滚动?
<br>无限滚动是指滚动列表中的Item对象，支持无限回收在利用的循环滚动列表。
2. 作用
<br>利用对象回收利用的机制，提升在进行大批量数据滚动显示时的性能。
## 核心API
```InitListView```:初始化滚动列表
<img width="1423" height="559" alt="image" src="https://github.com/user-attachments/assets/a3879397-18ca-4f04-aceb-4a76f618e664" />

```SetListItemCount```:重新设置滚动列表个数:数据增减必须调用此接口,否则会出现item索引与数据不一致和一起其他显示的Bug
<img width="983" height="187" alt="image" src="https://github.com/user-attachments/assets/51ebb5d0-2eeb-4658-90c8-2d8dee035f36" />

```RefreshAllShownItem```:更新所有可见的item最新数据
<img width="750" height="43" alt="image" src="https://github.com/user-attachments/assets/3866bcc1-c731-4d0d-a42e-b90974d03528" />

```MovePanelToItemIndex```:直接瞬移到目标索引Item处
<img width="1066" height="121" alt="image" src="https://github.com/user-attachments/assets/ce1630ef-093b-4eea-932a-747b4431d3e0" />

```SetSnapTargetItemIndex```:以缓动动画的形式平滑滚动到目标索引Item处
<img width="1211" height="344" alt="image" src="https://github.com/user-attachments/assets/19180397-cb2c-41e0-a117-c976f140487d" />

## 具体的使用方法
1.找到要使用的模板(横纵-ListView,网格-Grid,或者其他一些功能)
2.复制对应的loopListView,设置对应的item,和Padding
<img width="938" height="272" alt="image" src="https://github.com/user-attachments/assets/77f68d25-09ef-4ae7-b379-4ac7ece38a57" />
3.代码
```
using SuperScrollView;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankWindow : MonoBehaviour
{
    public LoopListView2 loopListView;
    private List<RankData> mRankList;
    private void Awake()
    {
        RefreshListView();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RankDataManager.Instance.AddRankData(50);
            RefreshListView();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            RankDataManager.Instance.AddRankData(50);
            RefreshListView(false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            MoveItemToIndex(50);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            MoveItemToIndexScroll(50);
        }


    }
    /// <summary>
    /// 刷新数据
    /// </summary>
    /// <param name="resPos"></param>
    public void RefreshListView(bool resPos=true)
    {
        mRankList = RankDataManager.Instance.RankDataList;
        if (!loopListView.ListViewInited)
        {
            //设置配置
            //LoopListViewInitParam config = new LoopListViewInitParam();
            //config.mDistanceForRecycle0 = 200;//上方回收item的距离
            //config.mDistanceForNew0 = 180; //上方创建item的距离

            //config.mDistanceForRecycle1 = 200;//下方回收item的距离
            //config.mDistanceForNew1 = 180; //下方创建item的距离
            //loopListView.InitListView(mRankList.Count, OnGetItemByIndex, config);
            //初始化滚动列表接口只允许调用一次
            loopListView.InitListView(mRankList.Count, OnGetItemByIndex);
        }
        else
        {
            //组合使用，刷新数据
            //榜单数据发生变化,重新设置最新的数据，数据增减必须调用此接口,否则会出现item索引与数据不一致和一切其他显示的Bug
            loopListView.SetListItemCount(mRankList.Count,resPos); //resPos:true,每次更新数据的时候会重置位置到索引0,false时维持当前位置
            //榜单数据发生变化，可能增加或减少，刷新当前显示的Item数量，避免数据与Item数据索引不一致产生的Bug
            loopListView.RefreshAllShownItem();
        }
       
    }
    /// <summary>
    /// 以瞬移的形式，直接硬切到指定目标索引Item处
    /// </summary>
    /// <param name="index"></param>

    public void MoveItemToIndex(int index)
    {
        loopListView.MovePanelToItemIndex(index, 0);//跳到index的item，并且在item的基础上向下偏移0
        loopListView.RefreshAllShownItem();
    }
    /// <summary>
    /// 以滚动的形式,滚动到目标索引Item处
    /// 注意:需要开启ItemSnapEnable
    /// </summary>
    /// <param name="index"></param>
    public void MoveItemToIndexScroll(int index)
    {
        loopListView.SetSnapTargetItemIndex(50,30000);
        loopListView.RefreshAllShownItem();
        //滚动完成的回调
        loopListView.mOnSnapItemFinished = (listView, item) => {

            loopListView.RefreshAllShownItem();
        };
        
    }


    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= mRankList.Count)
        {
            return null;
        }
        RankData rankData = mRankList[index];
      
        if (rankData == null)
        {
            return null;
        }
    
        LoopListViewItem2 item = listView.NewListViewItem("RankItem");
        
        RankItem itemScript = item.GetComponent<RankItem>();
        if (!item.IsInitHandlerCalled)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init();
        }
        itemScript.SetItemData(rankData, index);
    
        return item;
    }

}

```
</summary>
</summary>details>


