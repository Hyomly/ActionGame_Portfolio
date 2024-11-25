using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : SingletonMonobehaviour<GameManager>
{
    #region [Constants and Fields]
    [SerializeField]
    PlayerCtrl m_player;
    [SerializeField]
    float m_time = 30f;
    float m_curTime;
    int m_minute;
    int m_second;
    int m_stageCoins;

    List<BattleAreaCtrl> m_battleAreas = new List<BattleAreaCtrl>();
    #endregion [Constants and Fields]
    


    #region [Public Mathods]
    public void PulsCoin()
    {
        m_stageCoins++;
        UIManager.Instance.ShowCoins(m_stageCoins);
    }

    

    #endregion [Public Mathods]

    #region [Mathods]

    IEnumerator CoTimer()
    {
        while (m_curTime > 0)
        {
            m_curTime -= Time.deltaTime;
            m_minute = (int)m_curTime / 60;
            m_second = (int)m_curTime % 60;
            UIManager.Instance.ShowTimer(m_minute, m_second);
            yield return null;

            if (m_curTime <= 0)
            {
                Debug.Log("시간 종료");
                m_curTime = 0;
                yield break;
            }
        }
    }
    #endregion [Mathods]
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_curTime = m_time;
        StartCoroutine(CoTimer());
    }

}