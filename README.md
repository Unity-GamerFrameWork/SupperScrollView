<details>
<summary><h1>无限滚动解决方案</h1></summary>
1.UltimateCleanGUIPack:有一套完成的UI模板</br>
2.SuperScrollView:无限滚动解决方案</br>
    
## 一、介绍
1. 什么是无限滚动?  
无限滚动是指滚动列表中的Item对象，支持无限回收在利用的循环滚动列表。
2. 作用  
利用对象回收利用的机制，提升在进行大批量数据滚动显示时的性能。

---

## 二、核心API

```InitListView```: 初始化滚动列表  
<img width="1423" height="559" alt="image" src="https://github.com/user-attachments/assets/a3879397-18ca-4f04-aceb-4a76f618e664" />

```SetListItemCount```: 重新设置滚动列表个数。  
数据增减必须调用此接口，否则会出现 item 索引与数据不一致和其他显示 Bug。  
<img width="983" height="187" alt="image" src="https://github.com/user-attachments/assets/51ebb5d0-2eeb-4658-90c8-2d8dee035f36" />

```RefreshAllShownItem```: 更新所有可见的 item 最新数据  
<img width="750" height="43" alt="image" src="https://github.com/user-attachments/assets/3866bcc1-c731-4d0d-a42e-b90974d03528" />

```MovePanelToItemIndex```: 直接瞬移到目标索引 Item 处  
<img width="1066" height="121" alt="image" src="https://github.com/user-attachments/assets/ce1630ef-093b-4eea-932a-747b4431d3e0" />

```SetSnapTargetItemIndex```: 以缓动动画的形式平滑滚动到目标索引 Item 处  
<img width="1211" height="344" alt="image" src="https://github.com/user-attachments/assets/19180397-cb2c-41e0-a117-c976f140487d" />

---

## 三、具体的使用方法

1. 找到要使用的模板（横向/纵向 ListView、网格 Grid 等）  
2. 复制对应的 LoopListView，设置对应的 item 和 Padding  
   <img width="938" height="272" alt="image" src="https://github.com/user-attachments/assets/77f68d25-09ef-4ae7-b379-4ac7ece38a57" />
3. 代码示例：

```csharp
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

    public void RefreshListView(bool resPos = true)
    {
        mRankList = RankDataManager.Instance.RankDataList;
        if (!loopListView.ListViewInited)
        {
            loopListView.InitListView(mRankList.Count, OnGetItemByIndex);
        }
        else
        {
            loopListView.SetListItemCount(mRankList.Count, resPos);
            loopListView.RefreshAllShownItem();
        }
    }

    public void MoveItemToIndex(int index)
    {
        loopListView.MovePanelToItemIndex(index, 0);
        loopListView.RefreshAllShownItem();
    }

    public void MoveItemToIndexScroll(int index)
    {
        loopListView.SetSnapTargetItemIndex(50, 30000);
        loopListView.RefreshAllShownItem();

        loopListView.mOnSnapItemFinished = (listView, item) =>
        {
            loopListView.RefreshAllShownItem();
        };
    }

    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= mRankList.Count)
            return null;

        RankData rankData = mRankList[index];
        if (rankData == null)
            return null;

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
</details>
