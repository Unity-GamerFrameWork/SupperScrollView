using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperScrollView;
/// <summary>
/// 参考GridViewTopToBottomDemo
/// </summary>
public class BackPackWindow : MonoBehaviour
{
    public LoopGridView loopGridView;
    private List<BackPackData> mBackPackDataList = new List<BackPackData>(); 

    private void Awake()
    {
        
    }
    private void Start()
    {
        OnRefreshGridView();
    }
    private void OnRefreshGridView()
    {
        mBackPackDataList = BackPackDataManager.Instance.BackPackDataList;
        if (!loopGridView.ListViewInited)
        {
            loopGridView.InitGridView(mBackPackDataList.Count, OnGetItemByRowColumn);
        }
        else
        {
            loopGridView.SetListItemCount(mBackPackDataList.Count,false);
            loopGridView.RefreshAllShownItem();
        }
    }

    LoopGridViewItem OnGetItemByRowColumn(LoopGridView gridView, int index, int row, int column)
    {
        if (index < 0 || index >= mBackPackDataList.Count)
        {
            return null;
        }


        BackPackData itemData = mBackPackDataList[index];
        if (itemData == null)
        {
            return null;
        }
        
        LoopGridViewItem item = gridView.NewListViewItem("GridItem");
    
        GridItem itemScript = item.GetComponent<GridItem>();
       
        if (item.IsInitHandlerCalled == false)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init();
        }
     
        itemScript.SetItemData(itemData, index);
        return item;
    }
}
