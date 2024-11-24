using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    int m_boxHp = 10;

    public void SetDamage( float damage )
    {
        m_boxHp -= (int)damage;
        if( m_boxHp <= 0 )
        {
            gameObject.SetActive( false );
        }
    }
    
}
