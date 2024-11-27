using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : SingletonDontDestroy<StageSelect>
{
    public void SetStage(int stageIdx)
    {
        LoadScene.Instance.LoadSceneAsync(SceneState.Game);
        StageManager.Instance.SetStage(stageIdx);

    }
}
