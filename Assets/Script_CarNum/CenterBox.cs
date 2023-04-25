using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterBox : MonoBehaviour
{
    public Vector3 center;
    public string tagToFind;
    public List<string> List = new List<string>();
    public MeshRenderer meshRenderer;
    public Vector3 rotation;
    public  Quaternion Rotation;
    public Vector3 objectSize;
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
        Collider[] colliders = Physics.OverlapBox(center, new Vector3(objectSize.x / 10f, objectSize.y, objectSize.z)/2, Rotation);
        
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(tagToFind))
            {
                GameObject gameObject = collider.gameObject;
                // Do something with the game object...
                List.Add(gameObject.name);
            }
        }

        foreach (var CAR in List)
        {
            Debug.Log(CAR);
        }
        List.Clear();
    }
}
    
/*
private void OnDrawGizmos()
{
    MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
    Vector3 objectSize = meshRenderer.bounds.size;
    //Gizmos.matrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(rotation), Vector3.one);
    Gizmos.color = Color.green;//改变线框的颜色
    Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(objectSize.x / 10f, objectSize.y, objectSize.z));//正方体虚线框
}
*/