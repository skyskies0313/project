using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

    public Rigidbody Car;
    public static bool throughCheckPoint;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Car" || other.gameObject.tag == "Untagged") && CheckPoint2.getCheckPoint2() && !CheckPoint3.getCheckPoint3())
        {
            setCheckPoint(true);
            Calc.setBack(false);
            Debug.Log(getCheckPoint().ToString());
        }
        else
        {
            Calc.setBack(true);
        }

    }
    void OnTriggerExit(Collider other)
    {
    }

    public static bool getCheckPoint()
    {
        return throughCheckPoint;
    }
    public static void setCheckPoint(bool tcp)
    {
        throughCheckPoint = tcp;
    }


    // Update is called once per frame
    void Update()
    {
       
    }
}
