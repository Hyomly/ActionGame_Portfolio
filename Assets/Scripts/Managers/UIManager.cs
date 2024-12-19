using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : SingletonMonobehaviour<UIManager>
{
    
    #region [Constants and Fields]

    [SerializeField]
    TMP_Text m_timerText;
    [SerializeField]
    TMP_Text m_coinCountText;
    [SerializeField]
    TMP_Text[] m_missions;    
    [SerializeField]
    Slider[] m_skillSliders;

    Dictionary<Motion, Slider> m_skillTimers = new Dictionary<Motion, Slider>();

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
    public void InitSlider(Motion skill,float coolTime )
    {
        m_skillTimers[skill].maxValue = coolTime;
        m_skillTimers[skill].value = m_skillTimers[skill].maxValue;
    }
    public void ShowCoolTime(Motion skill, float curTime)
    {
        m_skillTimers[skill].value = curTime;
        if(curTime <= 0.1f)
        {
            m_skillTimers[skill].value = 0f;
        }
    }
    #endregion [Public Mathods]
    protected override void OnStart()
    {
        ShowMission(1);
        int skillNum = 0;
        for (int i = (int)Motion.Desh; i <= (int)Motion.Skill2; i++)
        {
            
            var skill = (Motion)i;
            m_skillTimers.Add(skill, m_skillSliders[skillNum]);
            skillNum++;
        }
    }
}
