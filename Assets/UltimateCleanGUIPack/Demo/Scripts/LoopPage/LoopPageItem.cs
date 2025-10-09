using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoopPageItem : MonoBehaviour
{
    public TMP_Text titleText;
    public void Init()
    { 

    }
    public void SetItemData(PageData pageData,int itemIndex)
    { 
        titleText.text = pageData.title;    
    }
}
