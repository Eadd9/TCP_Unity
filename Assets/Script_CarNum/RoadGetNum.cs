using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGetNum : MonoBehaviour
{
    public string RoadID;
    public int CarNumonroad;
    public SubRoadGetNum SubRoadGetNum1;
    public SubRoadGetNum SubRoadGetNum2;
    public SubRoadGetNum SubRoadGetNum3;
    // Start is called before the first frame update
    void Start()
    {
        //可以使用GetComponentsInChildren()方法来获取它们的数组
        RoadID = gameObject.name;
        SubRoadGetNum1 = GameObject.Find(transform.GetChild(0).name).GetComponent<SubRoadGetNum>();
        SubRoadGetNum2 = GameObject.Find(transform.GetChild(1).name).GetComponent<SubRoadGetNum>();
        SubRoadGetNum3 = GameObject.Find(transform.GetChild(2).name).GetComponent<SubRoadGetNum>();
    }

    // Update is called once per frame
    void Update()
    {
        CarNumonroad = SubRoadGetNum1.SubRoadCarNum + SubRoadGetNum2.SubRoadCarNum + SubRoadGetNum3.SubRoadCarNum;
        Debug.Log(CarNumonroad);
    }
}
