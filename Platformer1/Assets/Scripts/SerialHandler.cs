using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;

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

    void Start()
    {
        portsNames = SerialPort.GetPortNames();
        comPort = new SerialPort("COM24", 115200);
        comPort.Open();
        serialThread = new Thread(ProcessSerial);
        serialThread.Start();
        incoming = new List<byte>();
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
                    ProcessCommand(incoming.ToArray());
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
        print(command);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
