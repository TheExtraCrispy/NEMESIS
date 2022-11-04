using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    // Start is called before the first frame update
    public BehaviorTree tree;
    void Start()
    {
        tree = tree.Clone();
        //tree.Bind();
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
