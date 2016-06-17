using UnityEngine;
using System.Collections;

public class TestGetReward : MonoBehaviour {
    byte[] a;
    
	void Update () {

        //a = Reward.getReward();
        if (Input.GetKeyDown("r"))
            for (int i = 0; i < a.Length; i++)
            {
                Debug.Log(a[i]);
            }
        if (Input.GetKeyDown("b"))
            Debug.Log(System.BitConverter.ToDouble(a, 0));
    }

}
