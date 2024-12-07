using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : SingletonMonobehaviour<UIManager>
{
    #region [Constants and Fields]

    [SerializeField]
    TMP_Text m_timerText;
    [SerializeField]
    TMP_Text m_coinCountText;

    #endregion [Constants and Fields]
    #region [Public Mathods]
    public void ShowTimer( int minute, int second )
    {
        m_timerText.text = minute.ToString("00") + ":" + second.ToString("00");
    }
    public void ShowCoins( int coinCount)
    {
        m_coinCountText.text = coinCount.ToString();
    }
    protected override void OnStart()
    {
        TableLoader.Instance.LoadTable("Mission");
    }
    #endregion [Public Mathods]
   

}
