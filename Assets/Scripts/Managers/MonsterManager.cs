using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SingletonMonobehaviour<MonsterManager>
{
    [SerializeField]
    PlayerCtrl m_player;
    [SerializeField]
    GameObject m_monsterPrefab;
    int m_removeCount = 0;
    int m_monsterCount = 0;
   
    GameObjectPool<MonsterCtrl> m_slimePool;

    public void CreateMonster(Transform[] spawnPos)
    {
        m_monsterCount = spawnPos.Length - 1;
        for(int i = 0; i < spawnPos.Length-1; i++)
        {
            var mon = m_slimePool.Get();            
            mon.transform.position = spawnPos[i + 1].transform.position;
            mon.transform.parent.gameObject.SetActive(true);
            
        }
    }
    public void RemoveMonster(MonsterCtrl mon)
    {
        mon.transform.parent.gameObject.SetActive(false);
        m_removeCount++;
        if(m_removeCount == m_monsterCount)
        {
            BattleAreaManager.Instance.EndBattle();
        }
    }
   
    // Start is called before the first frame update
    protected override void OnStart()
    {
        // Monster Pooling
        m_slimePool = new GameObjectPool<MonsterCtrl>(5, () =>
        {
            var obj = Instantiate(m_monsterPrefab);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            var mon = obj.GetComponentInChildren<MonsterCtrl>();
            mon.InitMonster(m_player);
            return mon;
        });
        
        
    }

   
}
