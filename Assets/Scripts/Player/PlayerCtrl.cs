using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static MonsterCtrl;

public class PlayerCtrl : MonoBehaviour
{
    #region [Constants and Fields]
    PlayerAniCtrl m_animCtrl;
    SkillCtrl m_skillCtrl;
    [SerializeField]
    HUD_Ctrl m_hud;
    AttackAreaUnitFind[] m_attackAreas;    
    [SerializeField]
    GameObject m_attackAreaObj;
    [Space(10f)]
    [SerializeField, Header("[ 주인공 능력치 ]")]
    Status m_status;
    
    //[SerializeField]
    bool m_isCombo = false;
    int hash_Move;
    float m_speed = 4f;
    Vector3 m_dir;
   
    #endregion [Constants and Fields]

    #region [Public  Properties]
    PlayerAniCtrl.Motion GetMotion { get { return m_animCtrl.GetMotion; } }  //current Motion

    #endregion [Public Properties]

    #region [Animation Event Methods]

    void AnimEvent_AnimFinished()
    {
        m_animCtrl.Play(PlayerAniCtrl.Motion.Idle);
    }
    void AnimEvent_Attack()
    {
        var monList = m_attackAreas[0].MonList;
        var objList = m_attackAreas[0].ObjList;
        for (int i = 0; i < monList.Count; i++)
        {
            if (monList[i].gameObject.activeSelf)
            {
                var mon = monList[i].GetComponent<MonsterCtrl>();
                if (mon != null)
                {
                    mon.SetDamage(10f);
                }
            }
            else
            {
                m_attackAreas[0].ClearList(monList[i].gameObject); 
            }            
        }
        for (int i = 0; i < objList.Count; i++)
        {
            if (objList[i].gameObject.activeSelf)
            {
                var obj = objList[i].GetComponent<Box>();
                if (obj != null)
                {
                    obj.SetDamage(5f);
                }
            }
            else
            {
                m_attackAreas[0].ClearList(objList[i].gameObject);
            }
        }
    }

    // Combo Attack Frame
    void AnimEvent_AttackFinished()
    {
        m_isCombo = false;
        if(m_skillCtrl.CommandCount > 0)
        {
            m_skillCtrl.GetCommand();
            m_isCombo = true; 
            if(m_skillCtrl.CommandCount > 0)
            {
                m_skillCtrl.ClearKeyBuffer();
                m_isCombo = false;
            }
        }
        if(m_isCombo)
        {
            m_animCtrl.Play(m_skillCtrl.GetCombo());
        }
        else
        {
            m_skillCtrl.ResetCombo();
            m_animCtrl.Play(PlayerAniCtrl.Motion.Idle);            
        }
       
    }
    #endregion [Animation Event Methos]

    #region [Public Methods]
    public void AffectWind(Vector3 dir, float speed)
    {
        gameObject.transform.position += dir * speed * Time.deltaTime;
    }
    public void SetDamage(float damage)
    {
        // Hp Down
        m_status.hp -= Mathf.RoundToInt(damage);        
        m_hud.IsDamage(true, m_status.hp);
        
        m_animCtrl.Play(PlayerAniCtrl.Motion.Damage);

        // GameOver
        if (m_status.hp <= 0)
        {
            //GameOver();
        }
    }
    public void SetAttack()
    {
        if (GetMotion == PlayerAniCtrl.Motion.Idle || GetMotion == PlayerAniCtrl.Motion.Walk)
        {
            m_animCtrl.Play(PlayerAniCtrl.Motion.Attack1);
        }
        else
        {
            m_skillCtrl.AddCommand(KeyCode.Space);       
        }
    }
    public void SetDesh()
    {
        m_animCtrl.Play(PlayerAniCtrl.Motion.Desh);        
    }
    public void SetSkill1()
    {
        m_animCtrl.Play(PlayerAniCtrl.Motion.Skill1);
    }
    public void SetSkill2()
    {
        m_animCtrl.Play(PlayerAniCtrl.Motion.Skill2);
    }
    #endregion [Public Methods]

    #region [Methods]

    Vector3 GetPadAxis()
    {
        Vector3 padDir = MovePad.Instance.PadDir;
        Vector3 dir = Vector3.zero;
        if(padDir.x < 0f)   {  dir += Vector3.left * Mathf.Abs(padDir.x);  }
        if(padDir.x > 0f)   {  dir += Vector3.right * padDir.x;  }
        if(padDir.y < 0f)   {  dir += Vector3.back * Mathf.Abs(padDir.y);  }
        if(padDir.y > 0f)   {  dir += Vector3.forward * padDir.y;  }
        return dir;  
    }


    #endregion [Methods]

    #region [Unity Methods] 
    
    void Start()
    {
        m_animCtrl = GetComponent<PlayerAniCtrl>();
        m_skillCtrl = GetComponent<SkillCtrl>();
        m_hud = GetComponent<HUD_Ctrl>();
        m_attackAreas = m_attackAreaObj.GetComponentsInChildren<AttackAreaUnitFind>();        
        hash_Move = Animator.StringToHash("IsMove");
        m_hud.HpBarInit(m_status.hpMax);
    }

    void Update()
    {
        //Attack Combo
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetAttack();           
        }
        //Desh
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            SetDesh();  
        }
        //Skill 1
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SetSkill1();
        }
        //Skill 2
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetSkill2();
        }
        //Move Charactor
        m_dir = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        var padDir = GetPadAxis();
        if (padDir != Vector3.zero)
        {
            m_dir = padDir;           
        }
        if (m_dir != Vector3.zero)
        {
            m_animCtrl.SetBool(hash_Move, true);
            transform.forward = m_dir;
        }        
        else
        {
            m_animCtrl.SetBool(hash_Move, false);
        }
        transform.position += m_dir * m_speed * Time.deltaTime;
       
    }
    #endregion [Unity Methods]
    


}
