using UnityEngine;
using System.Collections;

public class trackGoal : MonoBehaviour {
    public Rigidbody Car;
    public static bool goal;
    public static int count = 1;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>();
    }

    public static bool Isgoal()
    {
        return goal;
    }


    void OnTriggerEnter(Collider other)
    {
        
            goal = true;
        

    }

   
    void OnTriggerExit(Collider other)
    {
        
            goal = false;

        count ++;
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    

}
