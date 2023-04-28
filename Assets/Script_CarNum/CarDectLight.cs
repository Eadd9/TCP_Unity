using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarDectLight : MonoBehaviour
{
    public GameObject E35;
    public List<string> ordinaryCarList = new List<string>();
    private Vector3 CarCenter;
    private bool IsInRoad = false;

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
                if (collider.CompareTag("ordinarycar"))
                {
                    if (!ordinaryCarList.Contains(collider.name))
                    {
                        ordinaryCarList.Add(collider.name);
                    }
                }
            }
        }
    }
}
