using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputInteractionController : MonoBehaviour {

    [HideInInspector] public List<Interactable> selectedObjects = new List<Interactable>();

    public float ClickRadious = 0.3f;

    public StatDisplay m_DisplayStats = null;
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.timeScale == 1.0f)
                Time.timeScale = 0.0f;
            else
                Time.timeScale = 1.0f;
        }

        {
            Vector3 mousePos = Input.mousePosition;

            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;

            Debug.DrawLine(mousePos, mousePos + (Vector3.up * ClickRadious));
            Debug.DrawLine(mousePos, mousePos + (Vector3.down * ClickRadious));
            Debug.DrawLine(mousePos, mousePos + (Vector3.left * ClickRadious));
            Debug.DrawLine(mousePos, mousePos + (Vector3.right * ClickRadious));
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;

            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;

            Debug.Log(mousePos);

            Collider2D enemyCheck = Physics2D.OverlapCircle(mousePos, ClickRadious);


            if (!HandleEnemy(enemyCheck))
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePos, ClickRadious);

                SelectUnits(colliders);
            }

        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;

            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0;

            foreach (Interactable i in selectedObjects)
            {
                i.DirectPosition(mousePos);
            }
        }
		
	}

    private bool HandleEnemy(Collider2D target)
    {
        if (target == null)
            return false;
        if (selectedObjects.Count == 0)
            return false;

        Attackable attack = target.gameObject.GetComponent<Attackable>();

        if (attack != null && attack.isEnemyAffiliated(0))
        {
            foreach (Interactable i in selectedObjects)
            {
                i.Target(attack.SelectAsTarget());
            }

            if(m_DisplayStats != null)
                m_DisplayStats.GiveStats(target.GetComponent<Enemy>().m_Char.m_Stats);

            return true;
        }
        return false;
    }

    private void SelectUnits(Collider2D[] colliders)
    {
        foreach (Collider2D c in colliders)
        {
            Interactable other = c.gameObject.GetComponent<Interactable>();
            if (other != null)
            {
                selectedObjects.Clear();
                break;
            }

        }

        if (colliders.Length > 0)
        {
            foreach (Collider2D c in colliders)
            {
                Interactable other = c.gameObject.GetComponent<Interactable>();

                if (other != null)
                {
                    other.Select();
                    if (!selectedObjects.Contains(other))
                    {
                        selectedObjects.Add(other);

                        if (m_DisplayStats != null)
                            m_DisplayStats.GiveStats(c.gameObject.GetComponent<Player>().m_Char.m_Stats);
                    }
                }
            }
        }
    }

    public void ModeSelection(int index)
    {
        foreach (Player p in selectedObjects)
            p.ModeSelection(index);
    }
}
