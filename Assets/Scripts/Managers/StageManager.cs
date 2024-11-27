using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonDontDestroy<StageManager>
{
    [SerializeField]
    List<GameObject> m_stages = new List<GameObject>();
    [SerializeField]
    List<GameObject> m_battleAreas = new List<GameObject>();

    public void SetStage(int stageIdx)
    {
        LoadScene.Instance.LoadSceneAsync(SceneState.Game);
        var stage = Instantiate(m_stages[stageIdx-1]);
        stage.transform.position = Vector3.zero;
        var battleArea = Instantiate(m_battleAreas[stageIdx-1]);
        battleArea.transform.position = Vector3.zero;
        var area = battleArea.GetComponentsInChildren<BattleAreaCtrl>();
        for(int i = 0; i < area.Length+1; i++)
        {
            BattleAreaManager.Instance.AddList(area[i+1]);
        }
    }
        
}
