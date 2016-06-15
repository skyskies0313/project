using UnityEngine;
using System.Collections;

public class TestgetReward2 : MonoBehaviour
{ 
    
    void Update()
    {
        if (Input.GetKeyDown("r")) Debug.Log(Calc.CalcReward());
    }

    void OnGUI()
    {
        string label = Calc.CalcReward().ToString();
        GUI.Label(new Rect(0, 100, 100, 30), label);
    }
}
