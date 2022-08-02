using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OBUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text OBText;
     
    private void OnEnable()
    {
        PlayerObserverManager.OnPlayerColetarChanged += UpdateOBText;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnPlayerColetarChanged -= UpdateOBText;
    }

    private void UpdateOBText(int ColetarNvObj)
    {
        OBText.text = ColetarNvObj.ToString();
    }
    
    
}


