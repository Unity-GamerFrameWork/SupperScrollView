using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GamerFrameWork.RedDotSystem
{
    public class RedDotItem : MonoBehaviour
    {
        public RedDotDefine redKey;
        public GameObject redDotObj;
        public Text countText;
        private void OnEnable()
        {
            RedDotSystem.Instance.UpdateRedDotState(redKey);
        }
        private void Start()
        {
            RedDotSystem.Instance.RegisterRedDotChangeEvent(redKey, OnRedDotStateChangeEvent);
            RedDotSystem.Instance.UpdateRedDotState(redKey);
        }

        private void OnDestory()
        {
            RedDotSystem.Instance.UnRegisterRedDotChangeEvent(redKey, OnRedDotStateChangeEvent);
        }
        /// <summary>
        /// 红点状态改变事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="active"></param>
        /// <param name="count"></param>
        public void OnRedDotStateChangeEvent(RedDotType type, bool active, int count)
        {
            redDotObj.SetActive(active);
            if (type != RedDotType.Normal)
            {
                countText.text = count.ToString();
            }
            countText.gameObject.SetActive(type != RedDotType.Normal);    
        }
    }
}

