using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
    public Rigidbody Car;
    bool goal;
    int count;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>();
	}

    public bool Isgoal()
    {
        return goal;
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Car")
        {
            goal = true;
            other.gameObject.transform.position = new Vector3(other.gameObject.transform.position.x, 1, -200);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Car")
        {
            goal = false;
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
}
