using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    public Text txt_HP;
    public Text txt_Defense;
    public Text txt_Protection;
    public Text txt_Speed;
    public Text txt_Strength;
    public Text txt_Dex;
    public Text txt_Accuracy;
    public Text txt_Luck;

    public Text txt_CurrentHealth;
    public Text txt_MinAttack;
    public Text txt_MaxAttack;

    public Text txt_Hit;
    public Text txt_Evasion;

    public Text txt_Crit;

    public Stats m_CurrentStats = null;

    private void Update()
    {
        if (m_CurrentStats == null)
            return;

        txt_HP.text = "Max HP: "                + string.Format("{0:0.#}",m_CurrentStats.BaseHealth     );
        txt_Defense.text = "Defense: "          + string.Format("{0:0.#}",m_CurrentStats.BaseDefense    );
        txt_Protection.text = "Protection: "    + string.Format("{0:0.#}",m_CurrentStats.BaseProtection );
        txt_Speed.text = "Speed: "              + string.Format("{0:0.#}",m_CurrentStats.BaseSpeed      );
        txt_Strength.text = "Strength: "        + string.Format("{0:0.#}",m_CurrentStats.BaseStrength   );
        txt_Dex.text = "Dex: "                  + string.Format("{0:0.#}",m_CurrentStats.BaseDex        );
        txt_Accuracy.text = "Accuracy: "        + string.Format("{0:0.#}",m_CurrentStats.BaseAccuracy   );
        txt_Luck.text = "Luck: "                + string.Format("{0:0.#}", m_CurrentStats.BaseLuck      );

        txt_CurrentHealth.text = "Current HP: " + string.Format("{0:0.#}", (m_CurrentStats.BaseHealth - m_CurrentStats.TotalDamage));

        float[] attack = m_CurrentStats.getBaseCloseAttackPower();

        txt_MinAttack.text = "Min: " + string.Format("{0:0.#}", attack[0]); 
        txt_MaxAttack.text = "Max: " + string.Format("{0:0.#}", attack[1]);

        txt_Hit.text = "Hit: " + string.Format("{0:0.#}", m_CurrentStats.getBaseAccuracy());
        txt_Evasion.text = "Evasion: " + string.Format("{0:0.#}", m_CurrentStats.getBaseEvasion());

        txt_Crit.text = "Crit: " + m_CurrentStats.getBaseCriticalChance();
    }

    public void GiveStats(Stats newStats)
    {
        m_CurrentStats = newStats;
    }
}
