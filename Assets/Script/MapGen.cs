using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("The number")]
    public int lanenum = 2000;

    [Header("Car Object")]
    public GameObject Road;
    public GameObject Jun;
    

    //4152.68,5247.53
    [Header("Map offset")]
    public float posoffset_x = 1651.51f;
    public float posoffset_y = 1653.26f;

    [Header("Vehicle ID Lists")]
    GameObject[] road = new GameObject[2000];
    GameObject[] Juns = new GameObject[500];
    

    private string[][] Array1;
    private string[][] Array2;
    private int Lanenum;
    private int Junnum;
    private float RoadLength;
    private float Angle;
    private float CenterPosX;
    private float CenterPosZ;
    private float JunPosX;
    private float JunPosZ;
    // Start is called before the first frame update
    void Start()
    {
        //读取txt二进制文件  
        TextAsset binAsset = Resources.Load("test", typeof(TextAsset)) as TextAsset;
        TextAsset binAssetJun = Resources.Load("testJun", typeof(TextAsset)) as TextAsset;

        //读取每一行的内容  
        string[] lineArray = binAsset.text.Split("\r"[0]);
        string[] lineArrayJun = binAssetJun.text.Split("\r"[0]);

        //创建二维数组  
        Array1 = new string[lineArray.Length][];
        Array2 = new string[lineArrayJun.Length][];

        //把test中的数据储存在二维数组中  
        for (int i = 0; i < lineArray.Length; i++)
        {
            Array1[i] = lineArray[i].Split(',');
        }

        //把testJun中的数据储存在二维数组中
        for (int i = 0; i < lineArrayJun.Length; i++)
        {
            Array2[i] = lineArrayJun[i].Split(',');
        }

        Lanenum = Array1.Length - 1;
        Junnum = Array2.Length - 1;

        //克隆所需要的路口数量
        for (var i = 1; i < Junnum + 1; i++)
        {
            GameObject J;
            J = Instantiate(Jun);
            J.name = $"Jun ({i})";
        }

        //克隆所需要的道路数量
        for (var i = 1; i < Lanenum + 1; i++)
        {
            GameObject r;
            r = Instantiate(Road);
            r.name = $"Road ({i})";
        }

        //为各路口分别分配位置
        for (var i = 0; i < Junnum; i++)
        {
            JunPosX = 0;
            JunPosZ = 0;
            Juns[i] = GameObject.Find($"Jun ({i + 1})"); //指定路口
            JunPosX = float.Parse(Array2[i][0]) - posoffset_x;
            JunPosZ = float.Parse(Array2[i][1]) - posoffset_y;
            Juns[i].transform.localPosition = new Vector3(JunPosX, 0.1f, JunPosZ); //指定其坐标

        }

        //为各条道路分别分配设定好的元素
        for (var i = 1; i < Lanenum; i++)
        {
            RoadLength = 0;
            CenterPosX = 0;
            CenterPosZ = 0;
            Angle = 0;
            road[i] = GameObject.Find($"Road ({i})"); //指定道路
            RoadLength = float.Parse(Array1[i][0]);
            road[i].transform.localScale = new Vector3(0.5f, RoadLength, 1f); //指定道路长度
            Angle = float.Parse(Array1[i][1]);
            road[i].transform.localEulerAngles = new Vector3(-90f, 0.0f, Angle); //指定道路的角度
            CenterPosX = float.Parse(Array1[i][2]);
            CenterPosZ = float.Parse(Array1[i][3]);
            road[i].transform.localPosition = new Vector3(CenterPosX, 0.01f, CenterPosZ); //指定其坐标
        }
    }

}
