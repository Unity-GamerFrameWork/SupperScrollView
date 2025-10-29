using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GamerFrameWork.RedDotSystem
{
    public enum RedDotType
    {
        Normal,//只显示红点
        RedDotNodeNum,//节点数字红点,子节点有几个红点,就显示几个数字
        RedDotDataNum,//红点数据个数,根据数据的个数,显示红点数量
    }
    public class RedDotSystem 
    {
        private static RedDotSystem _instance;
        public static RedDotSystem Instance { get { if (_instance == null) _instance = new RedDotSystem();return _instance; } }
        /// <summary>
        /// 红点字典
        /// </summary>
        private Dictionary<RedDotDefine, RedDotTreeNode> mRedDotLogicDic = new Dictionary<RedDotDefine, RedDotTreeNode> ();

        public void InitlizateRedDotTree(List<RedDotTreeNode> nodeList)
        {
            foreach (var item in nodeList)
            {
                mRedDotLogicDic.Add(item.node,item);
            }
        }
        /// <summary>
        /// 更新红点状态
        /// </summary>
        /// <param name="redKey"></param>
        public void UpdateRedDotState(RedDotDefine redKey)
        {
            if (redKey == RedDotDefine.None)
            {
                return;
            }
            RedDotTreeNode redDotNode = null;
            if (mRedDotLogicDic.TryGetValue(redKey, out redDotNode))
            {
                redDotNode.RefreshRedDotState();
                UpdateRedDotState (redDotNode.parentNode);
            }
        }
        /// <summary>
        /// 注册红点状态改变事件
        /// </summary>
        public void RegisterRedDotChangeEvent(RedDotDefine redKey,System.Action<RedDotType,bool,int> changeEvent)
        {
            RedDotTreeNode redDotNode = null;
            if (mRedDotLogicDic.TryGetValue(redKey, out redDotNode))
            {
                redDotNode.OnRedDotActiveChange += changeEvent;
            }
            else
            {
                Debug.LogError($"key:{redKey} not exits,please check key define is error");
            }
        }
        /// <summary>
        /// 移除红点状态改变事件监听
        /// </summary>
        /// <param name="redKey"></param>
        /// <param name="changeEvent"></param>
        public void UnRegisterRedDotChangeEvent(RedDotDefine redKey, System.Action<RedDotType, bool, int> changeEvent)
        {
            RedDotTreeNode redDotNode = null;
            if (mRedDotLogicDic.TryGetValue(redKey, out redDotNode))
            {
                redDotNode.OnRedDotActiveChange -= changeEvent;
            }
            else
            {
                Debug.LogError($"key:{redKey} not exits,please check key define is error");
            }
        }
        /// <summary>
        /// 获取子节点红点个数
        /// </summary>
        /// <param name="redKey"></param>
        /// <returns></returns>
        public int GetChildNodeRedDotCount(RedDotDefine redKey)
        {
            int childRedDotCount = 0;
            ComputeChildRedDotCount(redKey,ref childRedDotCount);
            return childRedDotCount;
            
        }
        /// <summary>
        /// 计算子节点红点个数
        /// </summary>
        /// <param name="redKey"></param>
        /// <param name="childRedDotCount"></param>
        private void ComputeChildRedDotCount(RedDotDefine redKey,ref int childRedDotCount)
        {
            foreach (var item in mRedDotLogicDic.Values)
            {
                if (item.parentNode == redKey)
                {
                    item.RefreshRedDotState();
                    if (item.redDotActive)
                    {
                        childRedDotCount += item.redDotCount;
                        if (item.redDotType != RedDotType.RedDotDataNum)
                        {
                            ComputeChildRedDotCount(item.node,ref childRedDotCount);
                        }
                    }
                }
            }
        }

    }
}

