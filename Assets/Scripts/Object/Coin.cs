using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    MoveTween m_moveTween;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameManager.Instance.PulsCoin();
        }
    }
    public void Drop(Vector3 form)
    {
        Vector3 randPos = Random.insideUnitCircle * 0.5f;
        randPos.y = 0;
        var dir = randPos - transform.position;
        var to = dir.normalized * 0.5f;
        m_moveTween.Play(form, to, 0.2f);
    }

    private void Start()
    {
        m_moveTween = GetComponent<MoveTween>();
    }
}
