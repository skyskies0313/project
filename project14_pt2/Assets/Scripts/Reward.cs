using UnityEngine;
using System.Collections;

public class Reward : MonoBehaviour {
    private Rigidbody unko;
    public float reward;
    void Start () {
        unko = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        reward = GameObject.Find("Goal").GetComponent<Collider>().bounds.center.z - unko.transform.position.z;
    }
    void OnGUI()
    {
        string label = reward.ToString();
        GUI.Label(new Rect(0, 0, 100, 30), label);
    }
}
