using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarDect : MonoBehaviour
{
    //用于单辆自动驾驶车辆的信息记录，核心在于更新MesColl字典
    public GameObject E35;
    private List<GameObject> checkedObjects = new List<GameObject>();
    public Dictionary<string, int> MesColl = new Dictionary<string, int>();
    private Vector3 CarCenter;
    public int tmpnumleft = 0;
    public int tmpnumright = 0;
    private bool IsInRoad = false;
    void Update()
    {
        CarCenter = gameObject.transform.position;
        Queue<string> queleft = new Queue<string>(2);
        Queue<string> queright = new Queue<string>(2);
        Collider[] colliders = Physics.OverlapBox(CarCenter, new Vector3(8, 0.5f, 0.5f)/2);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == E35)
            {
                IsInRoad = true;
            }
        }
        foreach (Collider collider in colliders)
        {

            if (IsInRoad)
            {
                if (!checkedObjects.Contains(collider.gameObject))
                {
                    Vector3 colliderpos = transform.InverseTransformPoint(collider.transform.position);
                    if (collider.CompareTag("autocar"))
                    {
                        Debug.Log($"{gameObject.name}:1");
                        if (colliderpos.x < 0)//自动驾驶车辆在左边
                        {
                            queleft.Enqueue(collider.name); 
                            if (queleft.Count == 2)
                            {
                                string namefront = queleft.Dequeue();
                                string nameback = queleft.Dequeue();
                                MesColl.Add($"{namefront}_{nameback}",tmpnumleft);
                                tmpnumleft = 0;
                            }
                        }
                        else//自动驾驶车辆在右边
                        {
                            queright.Enqueue(collider.name);
                            if (queright.Count == 2)
                            {
                                string namefront = queright.Dequeue();
                                string nameback = queright.Dequeue();
                                MesColl.Add($"{namefront}_{nameback}",tmpnumright);
                                tmpnumright = 0;
                            }
                        }
                
                    }
                    else if (collider.CompareTag("ordinarycar"))
                    {
                        Debug.Log("2");
                        if (colliderpos.x < 0)
                        {
                            tmpnumleft += 1;
                        }
                        else
                        {
                            tmpnumright += 1;
                        }
                    }
                    checkedObjects.Add(collider.gameObject);
                }
            }
            
        }
    }
}
/*用于确定检测范围
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(CarCenter, new Vector3(8, 0.5f, 0.5f));
    }
 */
