using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarDectLight : MonoBehaviour
{
    //用于单辆自动驾驶车辆的信息记录，核心在于更新MesColl字典
    public GameObject E35;
    private List<GameObject> checkedObjects = new List<GameObject>();
    public List<string> ordinaryCarList = new List<string>();
    public Dictionary<string, int> MesColl = new Dictionary<string, int>();
    public Dictionary<string, int> MesCollleft = new Dictionary<string, int>();
    public Dictionary<string, int> MesCollright = new Dictionary<string, int>();
    private Vector3 CarCenter;
    private bool IsInRoad = false;
    public int OrdNumleft;
    public int OrdNumright;
    private string tmpautocarleft;
    private string tmpautocarright;

    private void Start()
    {
        MesColl.Add($"{gameObject.name}",0);
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(CarCenter, new Vector3(18, 0.5f, 0.5f));
    }
 
    
    void Update()
    {
        CarCenter = gameObject.transform.position;
        Collider[] colliders = Physics.OverlapBox(CarCenter, new Vector3(18, 0.5f, 0.5f)/2);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == E35)
            {
                IsInRoad = true;
            }
        }
        //*********逻辑***********
        foreach (Collider collider in colliders)
        {
            if (IsInRoad)
            {
                if (!checkedObjects.Contains(collider.gameObject))
                {
                    Vector3 colliderpos = transform.InverseTransformPoint(collider.transform.position);
                    if (collider.CompareTag("autocar"))
                    {
                        if (colliderpos.x < 0)//自动驾驶车辆在左边
                        {
                            OrdNumleft = 0;
                            if (MesCollleft.ContainsKey(collider.name) == false)
                            {
                                MesCollleft.Add(collider.name,OrdNumleft);
                            }
                            tmpautocarleft = collider.name;
                        }
                        else//自动驾驶车辆在右边
                        {
                            OrdNumright = 0;
                            if (MesCollright.ContainsKey(collider.name) == false)
                            {
                                MesCollright.Add(collider.name,OrdNumright);
                            }
                            tmpautocarright = collider.name;
                        }
                    }
                    else if (collider.CompareTag("ordinarycar"))
                    {
                        /*
                        if (colliderpos.x < 0)
                        {
                            OrdNumleft += 1;
                            if (tmpautocarleft != null)
                            {
                                MesCollleft[tmpautocarleft] = OrdNumleft;
                            }
                        }
                        else
                        {
                            OrdNumright += 1;
                            if (tmpautocarright != null)
                            {
                                MesCollright[tmpautocarright] = OrdNumright;
                            }
                        }
                        */
                        ordinaryCarList.Add(collider.name);
                        
                    }

                    MesColl = new Dictionary<string, int>(MesCollleft);
                    MesColl.AddRange(MesCollright);
                    checkedObjects.Add(collider.gameObject);
                }
            }
            
        }
    }
}
