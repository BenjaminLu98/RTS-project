using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject healthBarPrefab;
    public bool alwaysShow = true;
    public float showTime = 1.0f;
    public Transform anchor;

    GameObject healthBar;

    
    Image GreenSlider;

    Canvas targetCanvas;

    float timeLeft;

    private void Awake()
    {
        Unit unit = GetComponent<Unit>();
        unit.onDamaged += Unit_onDamaged;
        timeLeft = showTime;
    }

    
    // Update the health bar when the unit is damaged.
    private void Unit_onDamaged(float defaultHP, float currentHP)
    {
        float percentage = Mathf.Clamp(currentHP/defaultHP, 0, 1);
        healthBar.SetActive(true);
        timeLeft = showTime;
        GreenSlider.fillAmount = percentage;
    }

    private void OnEnable()
    {
        if (!healthBarPrefab)
        {
            Debug.LogWarning("Healthbar prefab not set!");
        }
        else
        {
            foreach(Canvas canvas in FindObjectsOfType<Canvas>())
            {
                if(canvas.renderMode == RenderMode.WorldSpace)
                {
                    targetCanvas = canvas;
                }
            }

            healthBar = Instantiate(healthBarPrefab,targetCanvas.transform);
            GreenSlider = healthBar.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            healthBar.SetActive(alwaysShow);
        }
    }

    // health bar follow the anchor.
    // Not in update() because the unit should move first, then the health bar follow. Otherwise the bar may blink.
    private void LateUpdate()
    {
        if (healthBar)
        {
            healthBar.transform.position = anchor.position;

            if (!alwaysShow) {
                if (timeLeft < 0)
                {
                    healthBar.SetActive(false);
                }
                else
                {
                    timeLeft -= Time.deltaTime;
                }
            }
        }
    }

}
