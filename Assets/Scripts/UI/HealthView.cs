using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _currentHealth;
    [SerializeField] private TMP_Text _maxHealth;
    [SerializeField] private Image _bar;

    public void SetName(string name)
    {
        _name.text = name;
    }

    public void SetHealth(float value)
    {
        if (value < 0)
            value = 0;

        _currentHealth.text = value.ToString();

        _bar.fillAmount = (float)(value / Convert.ToSingle(_maxHealth.text));
    }

    public void SetMaxHealth(float maxValue)
    {
        _maxHealth.text = maxValue.ToString();
    }
}
