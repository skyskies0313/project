using UnityEngine;
using System.Collections;

public class Reward : MonoBehaviour {
    public static Rigidbody unko;
    public static double reward;
    public static byte[] breward;

    public static double getReward () {
        
        reward = GameObject.Find("Goal").GetComponent<Collider>().bounds.center.z - GameObject.Find("Car").GetComponent<Collider>().transform.position.z;
        //breward = System.BitConverter.GetBytes(reward);

        return reward;
           
    }
}
