using GamerFrameWork.RedDotSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class RedDotNormalStoreDemo : MonoBehaviour
{
    public GameObject storeWindow;
    public Button storeButton;
    public Button goldTabButton;
    public Button diamondTabButton;
    public Button closeButton;

    private void Awake()
    {
        storeButton.onClick.AddListener(OnStoreButtonClick);
        goldTabButton.onClick.AddListener(OnGoldTabButtonClick);
        diamondTabButton.onClick.AddListener(OnDiamondTabButtonClick);
        closeButton.onClick.AddListener(OnStoreCloseButtonClick);

        //自定义红点逻辑使用演示,以非继承的形式表示红点的使用方式
        RedDotTreeNode storeMainRoot = new RedDotTreeNode { node = RedDotDefine.StoreRoot, logicHander = OnStoreRedDotLogicHandler };
        RedDotTreeNode store_Gold_Node = new RedDotTreeNode { parentNode = RedDotDefine.StoreRoot, node = RedDotDefine.Store_Gold, logicHander = OnStoreGoldRedDotLogicHandler };
        RedDotTreeNode store_Diamond_Node = new RedDotTreeNode { parentNode = RedDotDefine.StoreRoot, node = RedDotDefine.Store_Diamond, logicHander = OnStoreDiamondRedDotLogicHandler };

        RedDotSystem.Instance.InitlizateRedDotTree(new List<RedDotTreeNode> { storeMainRoot, store_Gold_Node, store_Diamond_Node });

    }
    public void OnStoreRedDotLogicHandler(RedDotTreeNode redNode)
    {
        if (RedDotDataMgr.Store_Gold_isRead && RedDotDataMgr.Store_Diamond_isRead)//如果都读了
        {
            redNode.redDotActive = false;
        }
        else
        {
            redNode.redDotActive = true;
        }
    }
    public void OnStoreGoldRedDotLogicHandler(RedDotTreeNode redNode)
    {
        redNode.redDotActive = !RedDotDataMgr.Store_Gold_isRead;
    }
    public void OnStoreDiamondRedDotLogicHandler(RedDotTreeNode redNode)
    {
        redNode.redDotActive = !RedDotDataMgr.Store_Diamond_isRead;
    }

    #region 按钮事件
    public void OnStoreButtonClick()
    {
        storeWindow.SetActive(true);
    }
    public void OnGoldTabButtonClick()
    {
        RedDotDataMgr.Store_Gold_isRead = true;
        RedDotSystem.Instance.UpdateRedDotState(RedDotDefine.Store_Gold);
    }
    public void OnDiamondTabButtonClick()
    {
        RedDotDataMgr.Store_Diamond_isRead = true;
        RedDotSystem.Instance.UpdateRedDotState(RedDotDefine.Store_Diamond);
    }
    public void OnStoreCloseButtonClick()
    {
        storeWindow.SetActive(false);
    }
    #endregion

}
