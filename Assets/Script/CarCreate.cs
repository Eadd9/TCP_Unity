using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCreate : MonoBehaviour
{

    [Header("Car Object")]
    public GameObject Tocusauto;
    public GameObject Tocusordinary;

    [Header("EachCar number")]
    public int autocarnum = 120;
    public int ordinarycarnum = 120;
    
    void Start()
    {
        //克隆所需要的自动驾驶车辆数量
        for (var i = 0; i < autocarnum; i++)
        {
            GameObject a;
            a = Instantiate(Tocusauto);
            a.name = $"autocar{i}";
        }
        //克隆所需要的普通驾驶车辆数量
        for (var i = 0; i < ordinarycarnum; i++)
        {
            GameObject b;
            b = Instantiate(Tocusordinary);
            b.name = $"ordinarycar{i}";
        }
    }
}
