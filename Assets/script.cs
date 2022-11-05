using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : ScriptableObject
{
    [SerializeField]
    private int testInt;

    [SerializeField]
    private string testString;

    [CreateAssetMenu(fileName = "New TestData", menuName = "Test Data", order = 51)]
    public class TestData : ScriptableObject {}

    void Start()
    {
        
    }

    

}
