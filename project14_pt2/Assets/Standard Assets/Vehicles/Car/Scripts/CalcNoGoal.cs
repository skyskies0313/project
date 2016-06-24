using UnityEngine;
using System.Collections;

public class CalcNoGoal : MonoBehaviour
{
    public static double carZ; //車のz座標
    public static double carX; //車のx座標
    public static double preCarZ = 0;
    public static double preCarX = 0;
    public static double preReward = 0;
    public static double trackSize = 1200.00;   //トラックの距離
    static int[] count = new int[] {0,0,0,0};
   

    public static double getCurrentReward()

    {
       
        carZ = GameObject.Find("Car").GetComponent<Collider>().transform.position.z;
        carX = GameObject.Find("Car").GetComponent<Collider>().transform.position.x;
     
        double[] basePointZ = new double[] { 120.00, 480.00, 720.00, 1080.00 };
        //{最初のカーブに入る点, 最初のカーブを出る点, 2番目のカーブに入る点, 2番目のカーブを出る点}
        double[] centerZ = new double[] { 120.00, -120.00 };
        //{最初のカーブの中心点, 2番目のカーブの中心点}
        double curveSize = 360.00;    //カーブの距離

        double d;                     //カーブの中心と車の距離
        double rad;                   //車が進んだカーブの角度
        double reward = 0;        //報酬
      

        if (IsFirstCurve())
        {
            d = System.Math.Sqrt(carX * carX + (carZ - centerZ[0]) * (carZ - centerZ[0]));
            rad = System.Math.Acos(carX / d) * 180.0 / System.Math.PI;
            count[1] = 0;
            count[2] = 0;
            count[3] = 0;

            if (double.IsNaN(rad) && IsRight())
            {
               
                reward = 120 - preCarZ;
                preCarZ = 0;

            }
            else if (double.IsNaN(rad) && IsLeft())
            {
                
                reward = 360 - preCarZ;
                preCarZ = 120;
            }
            else
            {
                if (count[0] == 0)
                {
                    count[0]++;
                    reward = curveSize * rad / 180 + 120 - preCarZ;
                    preCarZ = curveSize * rad / 180;
                    if (-100 < reward && reward < 100)
                    {
                        return ToRoundDown((reward), 2);
                    }
                    else if (reward < -100 || 100 < reward) { return 0; }

                }
                
                reward = curveSize * rad / 180 - preCarZ;
                
                preCarZ = curveSize * rad / 180;
                
            }
        }
        else if (IsSecondCurve())
        {
            d = System.Math.Sqrt(carX * carX + (carZ - centerZ[1]) * (carZ - centerZ[1]));
            rad = System.Math.Acos(carX / d) * 180.0 / System.Math.PI;
            count[0] = 0;
            count[1] = 0;
            count[3] = 0;


            if (double.IsNaN(rad) && IsLeft())
            {
                reward = -120 + preCarZ;
                preCarZ = 0;
            }
            else if (double.IsNaN(rad) && IsRight())
            {
                reward = 120 - preCarZ;
                preCarZ = -carZ;
            }
            else
            {
                if(count[2] == 0)
                {
                    count[2]++;
                    reward = curveSize * (1 - rad / 180) + 120 + carZ;
                    preCarZ = curveSize * (1 - rad / 180);
                    if (-100 < reward && reward < 100)
                    {
                        return ToRoundDown((reward), 2);
                    }
                    else if (reward < -100 || 100 < reward) { return 0; }
                }
                
                count[1] = 0;
                reward = curveSize * (1 - rad / 180) - preCarZ;
                preCarZ = curveSize * (1 - rad / 180);
            }
        }
        else if (IsFirstStraight())
        {
            count[0] = 0;
            count[1] = 0;
            count[2] = 0;
            if (count[3] == 0)
            {
                count[3]++;
                reward = 120 + carZ + 360 - preCarZ;
                preCarZ = carZ;
                if (-100 < reward && reward < 100)
                {
                    return ToRoundDown((reward), 2);
                }
                else if(reward < -100 || 100 < reward) { return 0; }

            }
            reward = carZ - preCarZ;
            preCarZ = carZ;
        }
        else if (IsSecondStraight())
        {   
            count[0] = 0;
            count[2] = 0;
            count[3] = 0;

            if(count[1] == 0)
            {
              
                count[1]++;
                reward = 360 - preCarZ + 120 - carZ;
                preCarZ = carZ;
                if (-100 < reward && reward < 100)
                {
                    return ToRoundDown((reward), 2);
                }
                else if (reward < -100 || 100 < reward) { return 0; }

            }
            reward = -carZ + preCarZ;
            preCarZ = carZ;
        }

        //Debug.Log(ToRoundDown((trackSize - reward), 2));
        if (-100 < reward && reward < 100)
        {
            return ToRoundDown((reward), 2);
        }
        else if (reward < -100 || 100 < reward) { return 0; }
        return reward;

    }




    public static byte[] CalcReward()
    {
       double Reward  = getCurrentReward() -0.1;
        Debug.Log(Reward);
        return System.Text.Encoding.UTF8.GetBytes(Reward.ToString());

    }
    //小数第2位で切り捨て
    public static double ToRoundDown(double dValue, int iDigits)
    {
        double dCoef = System.Math.Pow(10, iDigits);

        return dValue > 0 ? System.Math.Floor(dValue * dCoef) / dCoef :
                            System.Math.Ceiling(dValue * dCoef) / dCoef;
    }


    //トラックの左側にいるかどうか
    public static bool IsLeft()
    {
        bool ifLeft;
        if (carX < 0)
        {
            ifLeft = true;
        }
        else
        {
            ifLeft = false;
        }
        return ifLeft;
    }


    //トラックの右側にいるかどうか
    public static bool IsRight()
    {
        bool isRigtht;
        if (carX > 0)
        {
            isRigtht = true;
        }
        else
        {
            isRigtht = false;
        }
        return isRigtht;
    }


    //最初のカーブにいるかどうか
    public static bool IsFirstCurve()
    {
        bool isFirstCurve;
        if (carZ > 120)
        {
            isFirstCurve = true;
        }
        else
        {
            isFirstCurve = false;
        }
        return isFirstCurve;
    }


    //2番目のカーブにいるかどうか
    public static bool IsSecondCurve()
    {
        bool isSecondCurve;
        if (carZ < -120)
        {
            isSecondCurve = true;
        }
        else
        {
            isSecondCurve = false;
        }
        return isSecondCurve;
    }


    //最初の直線にいるかどうか
    public static bool IsFirstStraight()
    {
        bool isFirstStraight;
        if (IsRight() && -120 <= carZ && carZ <= 120)
        {
            isFirstStraight = true;
        }
        else
        {
            isFirstStraight = false;
        }
        return isFirstStraight;
    }


    //2番目の直線にいるかどうか
    public static bool IsSecondStraight()
    {
        bool isSecondStraight;
        if (IsLeft() && -120 <= carZ && carZ <= 120)
        {
            isSecondStraight = true;
        }
        else
        {
            isSecondStraight = false;
        }
        return isSecondStraight;
    }
}
