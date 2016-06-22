using UnityEngine;
using System.Collections;

public class trackGoal : MonoBehaviour {
    public Rigidbody Car;
    public static int count;
    public static bool unko;
    public static int courseCount;


    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>();
        unko = false;
        count = 1;
        courseCount = 1;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car" && CheckPoint.getCheckPoint() && CheckPoint2.getCheckPoint2() && CheckPoint3.getCheckPoint3())
        {
            setCount(getCount() + 1);
            CheckPoint.setCheckPoint(false);
            CheckPoint2.setCheckPoint2(false);
            CheckPoint3.setCheckPoint3(false);
            // Debug.Log(getCount().ToString());
        }
        /*else if(!(other.gameObject.tag == "Untagged"))
        {
            Calc.setBack(true);
        }*/
        else if (!(CheckPoint.getCheckPoint() && CheckPoint2.getCheckPoint2() && CheckPoint3.getCheckPoint3())){
            Calc.setBack(false);
        }
    }

    public static void setCount(int countset) {
        courseCount = countset;
    }

    public static int getCount()
    {
        return courseCount;
    }

    void OnTriggerExit(Collider other)
    {
       
    }

    
    void Update()
    {


        if (Calc.carZ > 0)
        {
            GameObject.Find("Goal").GetComponent<Collider>().enabled = false;
        }
        else if (Calc.carZ <= 0)
        {

            GameObject.Find("Goal").GetComponent<Collider>().enabled = true;
        }
     
    }

}
