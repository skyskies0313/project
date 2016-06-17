using UnityEngine;
using System.Collections;

public class CheckPoint3 : MonoBehaviour {

    public Rigidbody Car;
    public static bool throughCheckPoint3;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car" && CheckPoint2.getCheckPoint2() && CheckPoint.getCheckPoint())
            setCheckPoint3(true);
        Debug.Log(getCheckPoint3().ToString());


    }
    void OnTriggerExit(Collider other)
    {
    }

    public static bool getCheckPoint3()
    {
        return throughCheckPoint3;
    }
    public static void setCheckPoint3(bool tcp)
    {
        throughCheckPoint3 = tcp;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
