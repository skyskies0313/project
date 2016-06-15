using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System.Collections.Generic;
using WS;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
                                     //▽▽バイト列をコマンド（ストリング）へ▽▽▽▽▽
        public void commander(string s)
        {
            m_Car.Move(0, 0, 0, 0f);
            switch (s)
            {
                case "0": //reft
                    m_Car.Move(-1,1, 1, 0f);
                    print("accel+left");
                    break;
                case "1":  //right
                    m_Car.Move(1, 1, 1, 0f);
                    print("accel+right");
                    break;
                case "2":  //accel
                    m_Car.Move(0, 1, 1, 0f);
                    print("accel");
                    break;
                case "3": //back
                    m_Car.Move(0, -1, -1, 0f);
                    print("back");
                    break;
                case "4":
                    print("brank");
                    break;
            }
        }
        //△△△△△△△△△△△△△△△△△△△△△△△△


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate(){
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            //commander(Net.get_command()); コメントアウト
             m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }
}
