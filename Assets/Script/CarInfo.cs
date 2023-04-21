using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CarInfo
{
    public string vehid;
    public float posx;
    public float posy;
    public float speed;
    public float heading;
    public int VehicleType;

 
    public string Type;
 
    public CarInfo(string txt)
    {
        if (txt.Contains(";"))
        {
            string[] a = txt.Split(';'); //split the data of a vehicle, the data order: vehid, posx, posy, speed, heading, brakelight state, sizeclass
            if (a.Length >= 5)
            {
                vehid = a[0];
                posx = (float)Convert.ToDouble(a[1], new CultureInfo("en-US"));
                posy = (float)Convert.ToDouble(a[2], new CultureInfo("en-US"));
                speed = (float)Convert.ToDouble(a[3], new CultureInfo("en-US"));
                heading = (float)Convert.ToDouble(a[4], new CultureInfo("en-US"));
                VehicleType = (int)Convert.ToDouble(a[5], new CultureInfo("en-US"));


                if (VehicleType == 1)
                    Type = "autovehicle";
                else
                    Type = "ordinaryvehicle";

            }
            else
            {
                Debug.Log("incorrect messeage length");
            }
        }
    }
 
 
 
}
