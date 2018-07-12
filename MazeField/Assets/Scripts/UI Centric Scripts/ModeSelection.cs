using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSelection : MonoBehaviour
{
    public InputInteractionController m_IIC;

    public Button[] Modes;
    public Text MainButtonText;

    public void HideButtons()
    {
        foreach (Button btn in Modes)
            btn.gameObject.SetActive(false);
    }

    public void ShowButtons()
    {
        foreach (Button btn in Modes)
            btn.gameObject.SetActive(true);
    }

    public void BtnPressAttack()
    {
        MainButtonText.text = "Attack";
        m_IIC.ModeSelection(0);
        HideButtons();
    }

    public void BtnPressDefend()
    {
        MainButtonText.text = "Defend";
        m_IIC.ModeSelection(1);
        HideButtons();
    }

    public void BtnPressRanged()
    {
        MainButtonText.text = "Ranged";
        m_IIC.ModeSelection(2);
        HideButtons();
    }

    public void BtnPressFlee()
    {
        MainButtonText.text = "Flee";
        m_IIC.ModeSelection(3);
        HideButtons();
    }
}
