using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAni_Sword : MonoBehaviour {

    public GameObject m_Sword;

    float m_Timer = 0.0f;
    bool Attack = false;

    private void Update()
    {
        if (Attack)
        {
            m_Timer += Time.deltaTime * 7;

            float Angle = Mathf.Lerp(-60f, 60f, m_Timer);

            Quaternion q = Quaternion.AngleAxis(Angle, Vector3.forward);

            gameObject.transform.localRotation = q;

            if (m_Timer >= 1f)
            {
                m_Sword.SetActive(false);
                gameObject.transform.localRotation = Quaternion.AngleAxis(-60f, Vector3.forward);
            }
            if (m_Timer >= 3.0f)
            {
                m_Timer = 0.0f;
                Attack = false;
            }
        }        
    }

    public void Swing()
    {
        if (!Attack)
        {
            m_Sword.SetActive(true);
            Attack = true;
        }
    }

}
