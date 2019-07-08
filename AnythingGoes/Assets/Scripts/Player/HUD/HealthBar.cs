using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HealthBar : BaseUI
{
    [SerializeField]
    Image currentHealth;
    [SerializeField]
    Image previuosHealth;

    float pauseTime;
    // Use this for initialization
    void Start()
    {
        Mediator.RegisterHandler(GameUIEvents.HealthUpdate, this, updateHealth);
    }

    private void updateHealth(Message message)
    {
        currentHealth.fillAmount = (float)message.Properties[GameUIEventProperties.Health];
        
        if (previuosHealth.fillAmount < currentHealth.fillAmount)
        {
           
            previuosHealth.fillAmount = currentHealth.fillAmount;
        }
        else
        {
            pauseTime = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseTime > 0)
        {
            pauseTime -= Time.deltaTime;
        }
        else if (currentHealth.fillAmount < previuosHealth.fillAmount)
        {
            previuosHealth.fillAmount -= Time.deltaTime * 0.5f;
        }
    }
}
