using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoadGetNum : MonoBehaviour
{
    private Dictionary<string, int> SubRoadCarDict = new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SubRoadCarDict = gameObject.GetComponent<SubRoadGetSection>().SubRoadCarDict;
    }
}
