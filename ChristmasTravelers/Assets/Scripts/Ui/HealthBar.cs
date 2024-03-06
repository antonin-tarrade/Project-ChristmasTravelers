using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxValue;
    public float value;

    [SerializeField] private RectTransform topBar;
    [SerializeField] private RectTransform bottomBar;
    [SerializeField] private float animationSpeed;


    private float fullWidth;
    private bool firstInit = true;
    private float TargetWidth => value * fullWidth / maxValue;

    private Coroutine adjustBarWidth;

    private void InitWidth()
    {
        if (firstInit) fullWidth = topBar.rect.width;
        firstInit = false;
    }

    public void InitBar(float maxHealth)
    {
        InitWidth();
        bottomBar.SetWidth(fullWidth);
        topBar.SetWidth(fullWidth);
        maxValue = maxHealth;
        value = maxValue;
    }

    public void Change(float amount)
    {
        value = Mathf.Clamp(value + amount, 0, maxValue);

        if (adjustBarWidth != null)
        {
            StopCoroutine(adjustBarWidth);
        }
        adjustBarWidth = StartCoroutine(AdjustBarWidth(amount));
    }



    private IEnumerator AdjustBarWidth(float amount)
    {
        RectTransform suddenChangeBar = amount >= 0 ? bottomBar : topBar;
        RectTransform slowChangeBar = amount >= 0 ? topBar : bottomBar;
        suddenChangeBar.SetWidth(TargetWidth);
        while (Mathf.Abs(suddenChangeBar.rect.width - slowChangeBar.rect.width) > 0.01f)
        {
            slowChangeBar.SetWidth(Mathf.Lerp(slowChangeBar.rect.width, TargetWidth, Time.deltaTime * animationSpeed));
            yield return null;
        }
        slowChangeBar.SetWidth(TargetWidth);
    }

}