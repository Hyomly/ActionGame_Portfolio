using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndArea : MonoBehaviour
{
    [SerializeField]
    GameObject m_bud;
    [SerializeField]
    GameObject m_bloom;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            print("end");
            m_bud.SetActive(false);
            m_bloom.SetActive(true);
        }
    }
    private void Start()
    {
        m_bud.SetActive(true);
        m_bloom.SetActive(false);
    }
}
