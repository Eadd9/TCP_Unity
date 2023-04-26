using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadGetNum : MonoBehaviour
{
    public string RoadID;
    public int CarNumonroad;
    public Dictionary<string, int> AutoCarDict = new Dictionary<string, int>();
    public List<GameObject> AllAutoCarList = new List<GameObject>();
    private SubRoadGetSection SubRoadGetNum1;
    private SubRoadGetSection SubRoadGetNum2;
    private SubRoadGetSection SubRoadGetNum3;
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
        CarNumonroad = AllAutoCarList.Count;
        AllAutoCarList.Clear();
        AutoCarDict.Clear();
        AutoCarDict = SubRoadGetNum1.SubRoadCarDict.Concat(SubRoadGetNum2.SubRoadCarDict)
            .Concat(SubRoadGetNum3.SubRoadCarDict).ToDictionary(x => x.Key, x => x.Value);
        AllAutoCarList.AddRange(SubRoadGetNum1.AutoCarList);
        AllAutoCarList.AddRange(SubRoadGetNum2.AutoCarList);
        AllAutoCarList.AddRange(SubRoadGetNum3.AutoCarList);
        //更新间隔字典中的值
        foreach (var autocar in AllAutoCarList)
        {
            Dictionary<string, int> VehDect = autocar.GetComponent<CarDect>().MesColl;
            foreach (var key in AutoCarDict.Keys)
            {
                if (VehDect.ContainsKey(key))
                {
                    AutoCarDict[key] = VehDect[key];
                }
            }
        }
        //更新道路上的总车辆数
        foreach (var Num in AutoCarDict.Values)
        {
            CarNumonroad += Num;
        }
    }
}
