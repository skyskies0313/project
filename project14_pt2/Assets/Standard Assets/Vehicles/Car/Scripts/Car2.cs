using UnityEngine;
using System.Collections;

public class Car2 : MonoBehaviour
{
    private Rigidbody unko;
    public float posz;
    public float posx;

    void Start()
    {
        unko = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        posz = unko.transform.position.z;
        posx = unko.transform.position.x;

    }
    void OnGUI()
    {
        string labelX = "x=" + posx.ToString();
        string labelZ = "z=" + posz.ToString();
        GUI.Label(new Rect(0, 0, 100, 30), labelX);
        GUI.Label(new Rect(0, 50, 100, 30), labelZ);
        GUI.Label(new Rect(0, 90, 100, 30), trackGoal.getCount().ToString());
       

    }
}
