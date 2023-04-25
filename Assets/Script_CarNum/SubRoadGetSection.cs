using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//我们规定，在所有的子道路中，位于最后一位的子道路应该是该条总道路中最左边的那一条道路
//车辆的射线检测
public class SubRoadGetSection : MonoBehaviour
{
    public int SubRoadCarNum;
    public Dictionary<string, int> SubRoadCarDict = new Dictionary<string, int>();

    public GameObject Destination;
    public List<GameObject> AutoCarList = new List<GameObject>();
    private Vector3 center;
    private string tagToFind;
    private MeshRenderer meshRenderer;
    private Vector3 rotation;
    private  Quaternion Rotation;
    private Vector3 objectSize;

    void Start()
    {
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        objectSize = meshRenderer.bounds.size;
        rotation = transform.rotation.eulerAngles;
        center = gameObject.transform.position;
        tagToFind = "autocar";
        Rotation = Quaternion.Euler(0, rotation.y, rotation.z);
    }

    void Update()
    {
        AutoCarList.Clear();
        SubRoadCarDict.Clear();
        Collider[] colliders = Physics.OverlapBox(center, new Vector3(objectSize.x / 10f, objectSize.y, objectSize.z)/2, Rotation);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(tagToFind))
            {
                GameObject gameObject = collider.gameObject;
                if (!AutoCarList.Contains(gameObject))
                {
                    AutoCarList.Add(gameObject);
                }
            }
        }
        
        //对autolist进行排序
        if (AutoCarList != null)
        {
            AutoCarList.Sort((a, b) =>
            {
                float distanceToA = Vector3.Distance(a.transform.position, Destination.transform.position);
                float distanceToB = Vector3.Distance(b.transform.position, Destination.transform.position);
                return distanceToA.CompareTo(distanceToB);
            });
        }
        
        //输出车辆之间的间隔字典
        for (int i = 0; i < AutoCarList.Count; i++)
        {
            if (i != AutoCarList.Count - 1 && AutoCarList.Count != 0)
            {
                SubRoadCarDict.Add($"{AutoCarList[i].name}_{AutoCarList[i + 1].name}",0);
            }
        }
    }

}
