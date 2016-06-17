using UnityEngine;
using System.Collections;

public class CheckPoint2 : MonoBehaviour {


    public Rigidbody Car;
    public static bool throughCheckPoint2;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car" && !CheckPoint.getCheckPoint() && !CheckPoint3.getCheckPoint3())
            setCheckPoint2(true);
        Debug.Log(getCheckPoint2().ToString());


    }
    void OnTriggerExit(Collider other)
    {
    }

    public static bool getCheckPoint2()
    {
        return throughCheckPoint2;
    }
    public static void setCheckPoint2(bool tcp)
    {
        throughCheckPoint2 = tcp;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
