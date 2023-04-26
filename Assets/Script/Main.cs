using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour
{
    public GameObject TCP;
    private string Message;
    
    [Header("Map offset")]
    public float posoffset_x = 0f;
    public float posoffset_y = 0f;
    
    [Header("Vehicle ID Lists")]
    List<string> IDlist = new List<string>();
    List<string> oldIDlist = new List<string>();

    private Dictionary<string, CarInfo> CarDict = new Dictionary<string, CarInfo>();

    private float timer = 0.0f;
    private int carnum = 119;
    
    [Header("EachCar number")]
    public int autocarnum = 120;
    public int ordinarycarnum = 120;
    
    public GameObject[] autocar = new GameObject[120];
    public GameObject[] ordinarycar = new GameObject[120];
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < autocarnum; i++)
        {
            autocar[i] = GameObject.Find($"autocar{i}");
        }

        for (int i = 0; i < ordinarycarnum; i++)
        {
            ordinarycar[i] = GameObject.Find($"ordinarycar{i}");
        }

    }
    
    void Update()
    {
        Message = TCP.GetComponent<TCPtest>().RxMsg();
        SplitData(Message);
    }
    
    public void SplitData(string message)
    {
        if (message != null)
        {
            IDlist.Clear();
            string[] DataPerVehicle = message.Split('@');
            foreach (var vehicle in DataPerVehicle)
            {
                CarInfo car = new CarInfo(vehicle); 
                IDlist.Add(car.vehid);
                if (oldIDlist.Contains(car.vehid) == false) 
                {
                    CarDict.Add(car.vehid, car);  
                }
                else
                {
                    CarDict[car.vehid] = car;  
                }
            }
            Transform(CarDict, IDlist);    //call the transfrom function
            oldIDlist = IDlist; //update the list
        }
    }
    
    public void Transform(Dictionary<string, CarInfo> CarDict, List<string> IDs )
    {
        int j = 0;
        foreach (var eachVehicle in CarDict)
        {
            CarInfo tmp_CarInfo = eachVehicle.Value;
            for(int vehnum = 0; vehnum < carnum; vehnum++)
            {
                if (tmp_CarInfo.vehid == "car" + Convert.ToString(vehnum))
                {
                    j = vehnum;
                    break;
                }
            }
            if (tmp_CarInfo.Type == "autovehicle")
            {
                AutoMove(j,tmp_CarInfo);
            }
            else if(tmp_CarInfo.Type == "ordinaryvehicle")
            {
                OrdiMove(j,tmp_CarInfo);
            }
        }
        
        
    }

    public void AutoMove(int j,CarInfo tmpCarInfo)
    {
        Vector3 tempPos = autocar[j].transform.position; 
        tempPos.x = (tmpCarInfo.posx - posoffset_x);
        tempPos.y = 0.1f;
        tempPos.z = (tmpCarInfo.posy - posoffset_y);
        Quaternion rot;
        Vector3 ydir = new Vector3(0, 1, 0);
        rot = Quaternion.AngleAxis((tmpCarInfo.heading), ydir);
        autocar[j].transform.SetPositionAndRotation(tempPos, rot);
        //autocar[j].GetComponent<scr_VehicleHandler>().CalculateSteering(tmpCarInfo.heading, tmpCarInfo.speed, timer);
    }
    
    public void OrdiMove(int j,CarInfo tmpCarInfo)
    {
        Vector3 tempPos = ordinarycar[j].transform.position; 
        tempPos.x = (tmpCarInfo.posx - posoffset_x);
        tempPos.y = 0.1f;
        tempPos.z = (tmpCarInfo.posy - posoffset_y);
        Quaternion rot;
        Vector3 ydir = new Vector3(0, 1, 0);
        rot = Quaternion.AngleAxis((tmpCarInfo.heading), ydir);
        ordinarycar[j].transform.SetPositionAndRotation(tempPos, rot);
        //autocar[j].GetComponent<scr_VehicleHandler>().CalculateSteering(tmpCarInfo.heading, tmpCarInfo.speed, timer);
    }
}

