﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;
using UnityEngine.Networking;
using Unity.Jobs.LowLevel.Unsafe;

public class SerialHandler : MonoBehaviour
{

    SerialPort comPort;
    [SerializeField]
    string[] portsNames;
    Thread serialThread;
    // Start is called before the first frame update
    List<byte> incoming;
    [SerializeField]
    string command;

    private void Awake()
    {
        portsNames = SerialPort.GetPortNames();
        comPort = new SerialPort("COM24", 115200);
        comPort.Open();
        serialThread = new Thread(ProcessSerial);
        serialThread.Start();
        incoming = new List<byte>();
    }


    void Start()
    {
        if(!comPort.IsOpen)
        {
            portsNames = SerialPort.GetPortNames();
            comPort = new SerialPort("COM24", 115200);
            comPort.Open();
            serialThread = new Thread(ProcessSerial);
            serialThread.Start();
        }

        
    }

    private void OnDestroy()
    {
        if(comPort.IsOpen)
        {
            comPort.Close();
        }
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Tring to open serial port");
            if (!comPort.IsOpen || comPort == null)
            {
                portsNames = SerialPort.GetPortNames();
                comPort = new SerialPort("COM24", 115200);
                comPort.Open();
                serialThread = new Thread(ProcessSerial);
                serialThread.Start();
            }
        }

    }

    private void ProcessSerial(object obj)
    {
        while(true)
        {
            if (comPort.BytesToRead > 0)
            {
                byte data = (byte)comPort.ReadByte();

                if(data == 13)
                {
                    //ProcessCommand(incoming.ToArray());
                    Dispatcher.Invoke(() =>
                    {
                        ProcessCommand(incoming.ToArray());
                    });

                    incoming.Clear();
                }
                else
                {
                    incoming.Add(data);
                }
            }
        }
    }


    void ProcessCommand(byte[] array)
    {
        command = System.Text.Encoding.UTF8.GetString(array, 0, array.Length);
        //print(command);
        //EventManager.Instance.InvokeNative("onBoardInteraction", gameObject, new CustomEventArgs(command));
        EventManager.Instance.onBoardInteraction.Invoke(gameObject, new CustomEventArgs(command));
    }

}
