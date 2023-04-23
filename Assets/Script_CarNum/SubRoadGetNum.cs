using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubRoadGetNum : MonoBehaviour
{
    public int SubRoadCarNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int[] intArray = { 1,2,3,4,5 };
        System.Random random = new System.Random();
        // 随机选择字符数组中的一个元素
        int index = random.Next(0, intArray.Length); // 随机生成一个在 [0, charArray.Length) 区间内的整数
        SubRoadCarNum = intArray[index];
    }
}
