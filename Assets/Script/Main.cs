using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour
{
    public GameObject TCP;
    private string Message;
    [Header("The number")]
    public int autocarnum = 80;
    public int ordinarycarnum = 40;
    
    [Header("Car Object")]
    public GameObject Tocusauto;
    public GameObject Tocusordinary;
    
    [Header("Map offset")]
    public float posoffset_x = 1651.51f;
    public float posoffset_y = 1653.26f;
    
    [Header("Vehicle ID Lists")]
    private string[] IDlistauto = new string[100];
    private string[] IDlistordinary = new string[100];
    private string[] oldIDlistauto = new string[100];
    private string[] oldIDlistordinary = new string[100];
    GameObject[] autocar = new GameObject[100];
    GameObject[] ordinarycar = new GameObject[100];
    
    private Dictionary<string, CarInfo> CarDictauto = new Dictionary<string, CarInfo>();
    private Dictionary<string, CarInfo> CarDictordinary = new Dictionary<string, CarInfo>();
    
    private float timer = 0.0f;
    

    
    // Start is called before the first frame update
    void Start()
    {
        //克隆所需要的自动驾驶车辆数量
        for (var i = 0; i < autocarnum; i++)
        {
            GameObject a;
            a = Instantiate(Tocusauto);
            a.name = $"autocar{i}";
        }
        
        //克隆所需要的自动驾驶车辆数量
        for (var i = 0; i < ordinarycarnum; i++)
        {
            GameObject b;
            b = Instantiate(Tocusordinary);
            b.name = $"ordinarycar{i}";
        }
        //用于弹性增加自动驾驶车辆
        string[] MoveAbleVehList01 = new string[] { };
        List<string> Arrayauto = new List<string>(MoveAbleVehList01);
        for(int i = 0; i < autocarnum; i++)
        {
            Arrayauto.Add($"autocar{i}");
        }
        string[] MoveAbleVehListauto = Arrayauto.ToArray();

        for (int i = 0; i < MoveAbleVehListauto.Length; i++)
        {
            autocar[i] = GameObject.Find(MoveAbleVehListauto[i]);
        }
        //用于弹性增加普通车辆
        string[] MoveAbleVehList02 = new string[] { };
        List<string> Arrayordinary = new List<string>(MoveAbleVehList02);
        for(int i = 0; i < ordinarycarnum; i++)
        {
            Arrayordinary.Add($"ordinarycar{i}");
        }
        string[] MoveAbleVehListordinary = Arrayordinary.ToArray();

        for (int i = 0; i < MoveAbleVehListordinary.Length; i++)
        {
            ordinarycar[i] = GameObject.Find(MoveAbleVehListordinary[i]);
        }
    }
    
    void Update()
    {
        Message = TCP.GetComponent<TCPtest>().RxMsg();
        //Debug.Log(Message);
        SplitData(Message,timer);
    }

    public void SplitData(string message, float timer)           //split incoming string per vehicle
    {
        if (message != null)      // @ is the separator between vehicles
        {
            int autoi = 0;
            int ordinaryi = 0;
            Array.Clear(IDlistauto, 0, IDlistauto.Length);
            Array.Clear(IDlistordinary, 0, IDlistordinary.Length);
            string[] DataPerVehicle = message.Split('@');       
            for (int i = 0; i < DataPerVehicle.Length; i++)
            {
                CarInfo car = new CarInfo(DataPerVehicle[i]);       //creating a CarInfo class with name car
                if (car.VehicleType == 1)
                {
                    IDlistauto[autoi] = car.vehid;  //adding the id to the ID list to check
                    autoi++;
                    if (CarDictauto.ContainsKey(car.vehid) == false) //check the ID, if the new ID is element of the old list, it returns with the index, if not element, it returns with -1
                    {
                        CarDictauto.Add(car.vehid, car);      //fill up dictionary
                    }
                    else
                    {
                        CarDictauto[car.vehid] = car;       //update dictionary
                    }
                }
                else
                {
                    IDlistordinary[ordinaryi] = car.vehid;  //adding the id to the ID list to check
                    ordinaryi++;
                    if (Array.IndexOf(oldIDlistordinary, car.vehid) == -1 && car.vehid != null) //check the ID, if the new ID is element of the old list, it returns with the index, if not element, it returns with -1
                    {
                        CarDictordinary.Add(car.vehid, car);      //fill up dictionary
                    }
                    else if (car.vehid != null)
                    {
                        CarDictordinary[car.vehid] = car;       //update dictionary
                    }
                }
            }
            Transformauto(CarDictauto, IDlistauto);    //call the transfrom function
            oldIDlistauto = IDlistauto; //update the list
            Transformordinary(CarDictordinary, IDlistordinary);    //call the transfrom function
            oldIDlistordinary = IDlistordinary; //update the list
        }
    }

    public void Transformauto(Dictionary<string, CarInfo> CarDict,  string[] IDs)
    {
        int num = CarDict.Count; 
        int j = 0;  //default
        int k = autocarnum;
        
        
        //string carfront = "car";
        for (int i = 0; i < num; i++)  //running through all vehicle
        {
            CarInfo tmp_CarInfo = CarDict[IDs[i]];  //creating tmp CarInfo to handle the current object


            for(int vehnum = 0; vehnum < k; vehnum++)
            {
                
                if (tmp_CarInfo.vehid == "car" + Convert.ToString(vehnum))
                {
                    j = vehnum;
                    break;
                }
             
            }
            Vector3 tempPos = autocar[j].transform.position;               // get the current position
            tempPos.x = (float)(tmp_CarInfo.posx - posoffset_x);       //adding the offset
            tempPos.y = 0.1f;
            tempPos.z = (float)(tmp_CarInfo.posy - posoffset_y);
            Quaternion tempRot = autocar[j].transform.rotation;            // get the current position
            Quaternion rot;
            Vector3 ydir = new Vector3(0, 1, 0);    //y direction to rotation
            rot = Quaternion.AngleAxis((tmp_CarInfo.heading), ydir);
            autocar[j].transform.SetPositionAndRotation(tempPos, rot);  //set the position and the rotation
            autocar[j].GetComponent<scr_VehicleHandler>().CalculateSteering(tmp_CarInfo.heading, tmp_CarInfo.speed, timer);
            //car[j].GetComponent<scr_VehicleHandler>().BrakeLightSwitch(tmp_CarInfo.brakestate);
        }
    }
    
    public void Transformordinary(Dictionary<string, CarInfo> CarDict,  string[] IDs)
    {
        int num = CarDict.Count; 
        int j = 0;  //default
        int k = ordinarycarnum;
        
        
        //string carfront = "car";
        for (int i = 0; i < num; i++)  //running through all vehicle
        {
            CarInfo tmp_CarInfo = CarDict[IDs[i]];  //creating tmp CarInfo to handle the current object


            for(int vehnum = 0; vehnum < k; vehnum++)
            {
                
                if (tmp_CarInfo.vehid == "car" + Convert.ToString(vehnum))
                {
                    j = vehnum;
                    break;
                }
             
            }
            Vector3 tempPos = ordinarycar[j].transform.position;               // get the current position
            tempPos.x = (float)(tmp_CarInfo.posx - posoffset_x);       //adding the offset
            tempPos.y = 0.1f;
            tempPos.z = (float)(tmp_CarInfo.posy - posoffset_y);
            Quaternion tempRot = ordinarycar[j].transform.rotation;            // get the current position
            Quaternion rot;
            Vector3 ydir = new Vector3(0, 1, 0);    //y direction to rotation
            rot = Quaternion.AngleAxis((tmp_CarInfo.heading), ydir);
            ordinarycar[j].transform.SetPositionAndRotation(tempPos, rot);  //set the position and the rotation
            ordinarycar[j].GetComponent<scr_VehicleHandler>().CalculateSteering(tmp_CarInfo.heading, tmp_CarInfo.speed, timer);
            //car[j].GetComponent<scr_VehicleHandler>().BrakeLightSwitch(tmp_CarInfo.brakestate);
        }
    }

}
public class CarInfo
{
    public string vehid;
    public float posx;
    public float posy;
    public float speed;
    public float heading;
    public int VehicleType;
    //public int sizeclass;
 
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
