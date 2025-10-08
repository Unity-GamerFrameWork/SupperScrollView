using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public TMP_Text rankingText;
    public TMP_Text nickNameText;
    public TMP_Text skillCountText;
    public TMP_Text gameTimesText;

    private RectTransform mRectTrans;
    public void Init()
    { 
        mRectTrans = transform as RectTransform;
    }
    public void SetItemData(RankData rankData,int index)
    {
        rankingText.text = rankData.ranking.ToString("N0");
        nickNameText.text = rankData.nickName;
        skillCountText.text = rankData.skillCount.ToString("N0");
        gameTimesText.text = rankData.gameCount.ToString("N0");

        //动态修改滚动列表Item基于锚点的宽度或宽度，实现异步滚动列表,注意:尽量不要让第一个去动态放缩宽高
        if (index%2==0)
        {
            mRectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 400);
        }
        else
        {
            mRectTrans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 200);
        } 
    }
}
