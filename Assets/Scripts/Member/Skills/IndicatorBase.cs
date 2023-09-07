using System;
using UnityEngine;
using UnityEngine.Serialization;

public class IndicatorBase : MonoBehaviour
{
    [SerializeField] protected InputVariables inputVariables;
    public SkillBase ability;
    [SerializeField] protected RectTransform noiseIndicator;

    protected virtual void Start()
    {
        noiseIndicator.localScale = new Vector3(ability.noiseDistance, ability.noiseDistance,1);
    }
}

/*
 * Birden fazla yetenek çeşidi bulunur.
 * Tüm yetenekler range Indicator kullanır çünkü her bir yetenek ses çıkartır belirli mesafelerde
 * 1. Lineer atış
 * 2. Parabolic atış
 * 3. Parabolic alan
*/