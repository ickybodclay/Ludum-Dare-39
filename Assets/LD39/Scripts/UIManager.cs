using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image m_PlayerHealthBar;
    public Image m_EnemyHealthBar;

    public Color healthyColor;
    public Color dangerColor;

    public void SetPlayerHealth(float amount) {
        if (amount <= 0.3f) {
            m_PlayerHealthBar.color = dangerColor;
        }
        else {
            m_PlayerHealthBar.color = healthyColor;
        }

        m_PlayerHealthBar.fillAmount = amount;

    }

    public void SetEnemyHealth(float amount) {
        if (amount <= 0.3f) {
            m_EnemyHealthBar.color = dangerColor;
        }
        else {
            m_EnemyHealthBar.color = healthyColor;
        }

        m_EnemyHealthBar.fillAmount = amount;
    }
}
