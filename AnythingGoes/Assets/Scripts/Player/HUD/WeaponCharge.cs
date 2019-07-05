using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class WeaponCharge : MonoBehaviour
{
    [SerializeField]
    Image availableCharge;
    [SerializeField]
    Image previousCharge;
    // Use this for initialization
    void Start()
    {
        Mediator.RegisterHandler(GameUIEvents.UpdateWeaponCharge, this, UpdateWeaponUI);
    }

    private void UpdateWeaponUI(Message message)
    {
        availableCharge.fillAmount = (float)message.Properties[GameUIEventProperties.AvailableCharge];
        previousCharge.fillAmount = (float)message.Properties[GameUIEventProperties.PreviousCharge];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
