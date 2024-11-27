using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class TitleCtrl : MonoBehaviour, IPointerClickHandler 
{
    
    public void OnPointerClick(PointerEventData eventData)
    {
        LoadScene.Instance.LoadSceneAsync(SceneState.Stage);
    }

}

