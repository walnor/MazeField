using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayAttackLog : MonoBehaviour {

    public float[] m_Results = null;

    public Text m_Display;

    public void GiveAttackResults(float[] results)
    {
        m_Results = results;

        if (results[0] != -1)
        {
            m_Display.text = "Attack Results:\n" +
                             "Damage: " + string.Format("{0:0.#}", results[0]) +
                             "\nHit: " + string.Format("{0:0.#}", results[1]) + " vs Evade: " + string.Format("{0:0.#}", results[2]) + "\n" +
                             "Power: " + string.Format("{0:0.#}", results[3]) + " Accuracy: " + string.Format("{0:0.#}", results[4]) + "\n" +
                             "Crit Chance: " + string.Format("{0:0.#}", results[5]) + " Check: " + string.Format("{0:0.#}", results[6]);
        }
        else
        {
            m_Display.text = "Attack Results:\n" +
                             "Miss" +
                             "\nHit: " + string.Format("{0:0.#}", results[1]) + " vs Evade: " + string.Format("{0:0.#}", results[2]);
        }
    }
}
