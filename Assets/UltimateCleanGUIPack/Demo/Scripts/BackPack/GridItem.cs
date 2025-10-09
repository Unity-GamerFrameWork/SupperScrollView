using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridItem : MonoBehaviour
{
    public GameObject iconRoot;
    public GameObject unLockRoot;
    public TMP_Text countText;
    public Image iconImage;
    public void Init()
    { 

    }
    public void SetItemData(BackPackData itemData,int itemIndex)
    {
        iconRoot.SetActive(itemData.isUnLock);
        unLockRoot.SetActive(!itemData.isUnLock);
        countText.text = $"x{itemData.count.ToString("N0")}";
        if (itemData.isUnLock)
        {
            iconImage.sprite = Resources.Load<Sprite>($"Item/{itemData.id}");
        }
    }
}
