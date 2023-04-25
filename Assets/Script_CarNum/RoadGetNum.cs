using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGetNum : MonoBehaviour
{
    public string RoadID;
    public int CarNumonroad;
    public SubRoadGetSection SubRoadGetNum1;
    public SubRoadGetSection SubRoadGetNum2;
    public SubRoadGetSection SubRoadGetNum3;
    // Start is called before the first frame update
    void Start()
    {
        //可以使用GetComponentsInChildren()方法来获取它们的数组
        RoadID = gameObject.name;
        SubRoadGetNum1 = GameObject.Find(transform.GetChild(0).name).GetComponent<SubRoadGetSection>();
        SubRoadGetNum2 = GameObject.Find(transform.GetChild(1).name).GetComponent<SubRoadGetSection>();
        SubRoadGetNum3 = GameObject.Find(transform.GetChild(2).name).GetComponent<SubRoadGetSection>();
    }

    // Update is called once per frame
    void Update()
    {
        CarNumonroad = SubRoadGetNum1.SubRoadCarNum + SubRoadGetNum2.SubRoadCarNum + SubRoadGetNum3.SubRoadCarNum;

    }
}
