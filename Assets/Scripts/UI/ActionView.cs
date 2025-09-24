using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionView : MonoBehaviour
{
    [SerializeField] private TMP_Text _textMiss;
    [SerializeField] private Vector3 _offVector;
    [SerializeField] private Vector3 _velocityVector;
    [SerializeField] private Vector3 _alphaVector;
    [Space]
    [SerializeField] private TMP_Text _textStandartDamage;
    [SerializeField] private Vector3 _offVectorStandartDamage;
    [SerializeField] private TMP_Text _textPoisonDamage;
    [SerializeField] private Vector3 _offVectorPoisonDamage;

    private Camera _camera;

    public void Init()
    {
        _camera = Camera.main;
    }

    public void ViewTextMiss(Vector3 pos)
    {
        pos = _camera.WorldToScreenPoint(pos);
        _textMiss.transform.position = pos + _offVector;
        _textMiss.gameObject.SetActive(true);

        StartCoroutine(TextAnim(_textMiss));
    }

    public void ViewDamageType(Vector3 pos, DamageData damageData)
    {
        pos = _camera.WorldToScreenPoint(pos);

        if (damageData.Damage + damageData.DamageWeapon != 0)
        {
            _textStandartDamage.text = (damageData.Damage + damageData.DamageWeapon).ToString();
            _textStandartDamage.transform.position = pos + _offVectorStandartDamage;
            _textStandartDamage.gameObject.SetActive(true);
            StartCoroutine(TextAnim(_textStandartDamage));
        }

        foreach(KeyValuePair<TypeAttribute, int> pair in damageData.damageAttribute)
        {
            if(pair.Key == TypeAttribute.Poison && pair.Value != 0)
            {
                _textPoisonDamage.text = pair.Value.ToString();
                _textPoisonDamage.transform.position = pos - _offVectorPoisonDamage;
                _textPoisonDamage.gameObject.SetActive(true);
                StartCoroutine(TextAnim(_textPoisonDamage));
            }
        }
    }

    private IEnumerator TextAnim(TMP_Text text)
    {
        Color startColor = text.color;
        Color color = startColor;

        while(true)
        {
            if (color.a <= 0)
                break;

            text.transform.position += Time.deltaTime * _velocityVector;
            color.a -= Time.deltaTime * _alphaVector.magnitude;
            text.color = color;
            yield return null;
        }

        text.gameObject.SetActive(false);
        text.color = startColor;
    }
}
