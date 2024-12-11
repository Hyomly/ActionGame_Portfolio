using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTable : SingletonMonobehaviour<MissionTable>
{

    public class Data
    {
        public int Stage;
        public string[] Mission = new string[3];    
    }
    public Dictionary<int, Data> m_tableData = new Dictionary<int, Data>();
    public Data GetMissionData(int stage)
    {
        return m_tableData[stage];
    }
    public void LoadData()
    {
        m_tableData.Clear();
        TableLoader.Instance.LoadTable("Mission");
        for (int i = 0; i < TableLoader.Instance.Count; i++) 
        {
            Data data = new Data();
            data.Stage = TableLoader.Instance.GetInteger("Stage", i);
            
            for(int j= 0; j < data.Mission.Length; j++)
            {
                data.Mission[j] = TableLoader.Instance.GetString("Mission" + (j + 1), i);
            }
            m_tableData.Add(data.Stage, data);
        }
        TableLoader.Instance.Clear();
    }
   protected override void OnStart()
    {
        LoadData();        
    }
}
