using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;
using System;

public class LoopPageWindow : MonoBehaviour
{
    public LoopListView2 loopListView;
    private List<PageData> mPageDataList = new List<PageData>();

    public Button nextBtn;
    public Button lastBtn;
    private void Start()
    {
        //RefreshListView();
        AutoRefreshListView();
        InvokeRepeating("OnNextButtonClick", 2, 2);
        nextBtn.onClick.AddListener(OnNextButtonClick);
        lastBtn.onClick.AddListener(OnLastButtonClick);
    }
    private void OnDisable()
    {
        loopListView.mOnSnapItemFinished -= OnSnapItemFinished;
        loopListView.mOnSnapNearestChanged -= OnSnapChanged;
    }
    #region 手动
    /// <summary>
    /// 刷新数据
    /// </summary>
    /// <param name="resPos"></param>
    public void RefreshListView(bool resPos = true)
    {
        mPageDataList = LoopPageDataManager.Instance.PageDataList;
        if (!loopListView.ListViewInited)
        {
            LoopListViewInitParam config = new LoopListViewInitParam();
            config.mSmoothDumpRate = 0.1f;
            config.mSnapFinishThreshold = 0.1f;//item发生改变,但没有到一半,拉回的速度
            config.mSnapVecThreshold = 800f;//item没发生改变，没有到一半,返回的过程
            loopListView.InitListView(mPageDataList.Count, OnGetItemByIndex, config);
            loopListView.mOnSnapItemFinished += OnSnapItemFinished;
            loopListView.mOnSnapNearestChanged += OnSnapChanged;
        }
        else
        {
            //组合使用，刷新数据
            //榜单数据发生变化,重新设置最新的数据，数据增减必须调用此接口,否则会出现item索引与数据不一致和一切其他显示的Bug
            loopListView.SetListItemCount(mPageDataList.Count, resPos); //resPos:true,每次更新数据的时候会重置位置到索引0,false时维持当前位置
            //榜单数据发生变化，可能增加或减少，刷新当前显示的Item数量，避免数据与Item数据索引不一致产生的Bug
            loopListView.RefreshAllShownItem();
        }

    }

    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        if (index < 0 || index >= mPageDataList.Count)
        {
            return null;
        }
        PageData pageData = mPageDataList[index];

        if (pageData == null)
        {
            return null;
        }

        LoopListViewItem2 item = listView.NewListViewItem("LoopPageItem");

        LoopPageItem itemScript = item.GetComponent<LoopPageItem>();
        if (!item.IsInitHandlerCalled)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init();
        }
        itemScript.SetItemData(pageData, index);

        return item;
    }
    #endregion


    #region 自动
    public void AutoRefreshListView(bool resPos = true)
    {
        mPageDataList = LoopPageDataManager.Instance.PageDataList;
        if (!loopListView.ListViewInited)
        {
            LoopListViewInitParam config = new LoopListViewInitParam();
            config.mSmoothDumpRate = 0.1f;
            config.mSnapFinishThreshold = 0.1f;//item发生改变,但没有到一半,拉回的速度
            config.mSnapVecThreshold = 800f;//item没发生改变，没有到一半,返回的过程
            loopListView.InitListView(-1, OnAutoGetItemByIndex, config);
            loopListView.mOnSnapItemFinished += OnSnapItemFinished;
            loopListView.mOnSnapNearestChanged += OnSnapChanged;
        }
        else
        {
            //组合使用，刷新数据
            //榜单数据发生变化,重新设置最新的数据，数据增减必须调用此接口,否则会出现item索引与数据不一致和一切其他显示的Bug
            loopListView.SetListItemCount(-1, resPos); //resPos:true,每次更新数据的时候会重置位置到索引0,false时维持当前位置
            //榜单数据发生变化，可能增加或减少，刷新当前显示的Item数量，避免数据与Item数据索引不一致产生的Bug
            loopListView.RefreshAllShownItem();
        }

    }

    LoopListViewItem2 OnAutoGetItemByIndex(LoopListView2 listView, int index)
    {
        int itemIndex = 0;
        if (index >= 0)
        {
            itemIndex = index % mPageDataList.Count;
        }
        else
        {
            itemIndex = mPageDataList.Count + ((index + 1) % mPageDataList.Count) - 1;
        }
        PageData pageData = mPageDataList[itemIndex];

        if (pageData == null)
        {
            return null;
        }

        LoopListViewItem2 item = listView.NewListViewItem("LoopPageItem");

        LoopPageItem itemScript = item.GetComponent<LoopPageItem>();
        if (!item.IsInitHandlerCalled)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init();
        }
        itemScript.SetItemData(pageData, itemIndex);

        return item;
    }


    #endregion

    public void OnNextButtonClick()
    {
        loopListView.SetSnapTargetItemIndex(loopListView.CurSnapNearestItemIndex + 1);
        

    }
    public void OnLastButtonClick()
    {
        loopListView.SetSnapTargetItemIndex(loopListView.CurSnapNearestItemIndex - 1);
       
    }
    /// <summary>
    /// 分页滚动完成回调
    /// </summary>
    /// <param name="view"></param>
    /// <param name="item"></param>
    private void OnSnapItemFinished(LoopListView2 view, LoopListViewItem2 item)
    {
        Debug.Log("OnSnapItemFinished");
    }
    /// <summary>
    /// 分页改变的回调
    /// </summary>
    /// <param name="view"></param>
    /// <param name="item"></param>
    private void OnSnapChanged(LoopListView2 view, LoopListViewItem2 item)
    {
        Debug.Log("OnSnapChanged");
        //左右两边隐藏按钮
        //nextBtn.gameObject.SetActive(loopListView.CurSnapNearestItemIndex != LoopPageDataManager.Instance.PageDataList.Count-1);
        //lastBtn.gameObject.SetActive(loopListView.CurSnapNearestItemIndex != 0);
    }
}
