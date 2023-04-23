using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToPython : MonoBehaviour
{
    public string MesToPython;
    public RoadGetNum RoadGetNum1;
    public RoadGetNum RoadGetNum2;
    //public string Mes1;
    void Start()
    {
        RoadGetNum1 = GameObject.Find("E35").GetComponent<RoadGetNum>();
        RoadGetNum2 = GameObject.Find("-E35").GetComponent<RoadGetNum>();
    }

    void Update()
    {
        MesToPython = "01G";
        MesToPython += RoadGetNum1.RoadID + ";" + RoadGetNum1.CarNumonroad + "@" + RoadGetNum2.RoadID + ";" + RoadGetNum2.CarNumonroad + "&";
        
    }

    public string MES_Python()
    {
        return MesToPython;
    }
}
