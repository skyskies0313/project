using UnityEngine;
using System.Collections;

public class Calc : MonoBehaviour
{
    public static double carZ; //車のz座標
    public static double carX; //車のx座標
    public static double preReward = 0;
    public static double trackSize = 1200.00;   //トラックの距離
    public static int calcCount = trackGoal.getCount();
    

    public static void setcarXZ(double carx, double carz)
    {
        carX = carx;
        carZ = carz;
    }

    public static double getCarX()
    {
        return carX;
    }

    public static double getCarZ()
    {
        return carZ;
    }

    public static double getCurrentReward()

    {
        calcCount = trackGoal.getCount();
        
        carZ = GameObject.Find("Car").GetComponent<Collider>().transform.position.z;
        carX = GameObject.Find("Car").GetComponent<Collider>().transform.position.x;
        setcarXZ(carX, carZ);
        double[] basePointZ = new double[] { 120.00, 480.00, 720.00, 1080.00};
        //{最初のカーブに入る点, 最初のカーブを出る点, 2番目のカーブに入る点, 2番目のカーブを出る点}
        double[] centerZ = new double[] { 120.00, -120.00 };
        //{最初のカーブの中心点, 2番目のカーブの中心点}
        double curveSize = 360.00;    //カーブの距離

        double d;                     //カーブの中心と車の距離
        double rad;                   //車が進んだカーブの角度
        double reward = 1200.00*calcCount;        //報酬


        if (IsFirstCurve())
        {
            d = System.Math.Sqrt(carX * carX + (carZ - centerZ[0]) * (carZ - centerZ[0]));
            rad = System.Math.Acos(carX / d) * 180.0 / System.Math.PI;
            if (double.IsNaN(rad) && IsRight())
            {
                reward = basePointZ[0] + trackSize * (calcCount-1);
            }
            else if (double.IsNaN(rad) && IsLeft())
            {
                reward = basePointZ[1] + trackSize * (calcCount - 1);
            }
            else
            {
                reward = basePointZ[0] + curveSize * rad / 180 + trackSize * (calcCount - 1);
            }
        }
        else if (IsSecondCurve())
        {
            d = System.Math.Sqrt(carX * carX + (carZ - centerZ[1]) * (carZ - centerZ[1]));
            rad = System.Math.Acos(carX / d) * 180.0 / System.Math.PI;

            if (double.IsNaN(rad) && IsLeft())
            {
                reward = basePointZ[2] + trackSize * (calcCount - 1);
            }
            else if (double.IsNaN(rad) && IsRight())
            {
                reward = basePointZ[3] + trackSize * (calcCount - 1);
            }
            else
            {
                reward = basePointZ[2] + curveSize * (1 - rad / 180) + trackSize * (calcCount - 1);
            }
        }
        else if (IsFirstStraight())
        {
            reward = carZ + trackSize * (calcCount - 1);
        }
        else if (IsSecondStraight())
        {
            reward = basePointZ[0] - carZ + basePointZ[1] + trackSize * (calcCount - 1);
        }
        else if (IsThirdStraight())
        {
            reward = basePointZ[3] + basePointZ[0] + carZ + trackSize * (calcCount - 1);
        }

        //Debug.Log(ToRoundDown((trackSize - reward), 2));
        return ToRoundDown((reward), 2);

    }

  


    public static byte[] CalcReward()
    {
        getCurrentReward();
        double Reward;
        if ((!CheckPoint.getCheckPoint() || !CheckPoint2.getCheckPoint2() || !CheckPoint3.getCheckPoint3())&&!IsFirstStraight())
        {
            Reward = getCurrentReward() - trackSize * calcCount - preReward;
            preReward = getCurrentReward() - trackSize * calcCount;
        }
        else
        {
            Reward = getCurrentReward() - preReward;
            preReward = getCurrentReward();
        }
        
        Debug.Log(Reward);
        //return System.BitConverter.GetBytes(Reward);
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
        if (carX < 0) { ifLeft = true;
        }else
        {
            ifLeft = false;
        }
        return ifLeft;
    }


    //トラックの右側にいるかどうか
    public static bool IsRight()
    {
        bool isRigtht;
        if(carX > 0)
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
        if(carZ > 120)
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
        if(carZ < -120)
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
        if(IsRight()&& 0 <= carZ && carZ <= 120)
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
        if(IsLeft() && -120 <= carZ && carZ <= 120)
        {
            isSecondStraight = true;
        }
        else
        {
            isSecondStraight = false;
        }
        return isSecondStraight;
    }


    //最後の直線にいるかどうか
    public static bool IsThirdStraight()
    {
        bool isThirdStraight;
        if(IsRight() && -120 <= carZ && carZ < 0)
        {
            isThirdStraight = true;
        }
        else
        {
            isThirdStraight = false;
        }
        return isThirdStraight;
    }
}
