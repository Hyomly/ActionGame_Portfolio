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
    [SerializeField]
    TMP_Text[] m_missions;

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
   
    public void ShowMission(int stage)
    {
        var missionData = MissionTable.Instance.GetMissionData(stage);
        for (int i = 0; i < missionData.Mission.Length; i++)
        {
            m_missions[i].text = missionData.Mission[i].ToString();
        }
    }
    #endregion [Public Mathods]
    protected override void OnStart()
    {
        
        ShowMission(1);
    }
}
