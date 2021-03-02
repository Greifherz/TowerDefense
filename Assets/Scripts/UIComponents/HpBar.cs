using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image Foreground;
    
    public int FullValue;
    public int CurrentValue;
    public bool Active;
    
    private float FillAmount => (float)CurrentValue / FullValue;

    public void UpdateValue(int newValue)
    {
        CurrentValue = newValue;
        UpdateFillAmount();
    }
    
    private void UpdateFillAmount()
    {
        Foreground.fillAmount = FillAmount;
    }
}
