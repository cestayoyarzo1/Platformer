using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System;

public class CanvasScript : MonoBehaviour
{
    [SerializeField]
    TMP_Dropdown comportDropdown, baudRateDropdown;

    [SerializeField]
    string[] comportNames;

    [SerializeField]
    Button connect, startGame;

    [SerializeField]
    GameObject portOpen;

    void Start()
    {
        comportNames = SerialPort.GetPortNames();
        comportDropdown = GetComponentInChildren<TMP_Dropdown>();
        foreach (string comportName in comportNames)
        {
            comportDropdown.options.Add(new TMP_Dropdown.OptionData(comportName));
        }
        comportDropdown.onValueChanged.AddListener(PortSelected);
        baudRateDropdown.onValueChanged.AddListener(BrSelected);
        connect.gameObject.SetActive(false);
        startGame.gameObject.SetActive(false);
        portOpen.SetActive(false);
        connect.onClick.AddListener(ConnectPort);
        baudRateDropdown.gameObject.SetActive(false);
    }

    private void BrSelected(int arg)
    {
        if (arg != 0)
        {
            connect.gameObject.SetActive(true);
        }
        else
        {
            connect.gameObject.SetActive(false);
        }
    }

    private void ConnectPort()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PortSelected(int arg)
    {
        if(arg != 0)
        {
            baudRateDropdown.gameObject.SetActive(true);
        }
        else
        {
            baudRateDropdown.gameObject.SetActive(false);
            connect.gameObject.SetActive(false);
        }
    }
}
