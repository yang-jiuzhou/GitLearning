using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Share
{
    /**
     * ClassName: Incremental
     * Description: PID算法（增量式）
     * Version: 1.0
     * Create:  2019/09/16
     * Author:  yangjiuzhou
     * Company: jshanbon
     **/
    [Serializable]
    public class Incremental
    {
        private double kp;      //比列系数
        private double ki;      //积分系数
        private double kd;      //微分系数
        private double target;  //目标值
        private double actual;  //实际值
        private double error;   //误差
        private double errorPre1;//上一次误差
        private double errorPre2;//上一次误差
        private double A;
        private double B;
        private double C;


        public Incremental()
        {

        }

        public Incremental(double p, double i, double d)
        {
            kp = p;
            ki = i;
            kd = d;
            target = 0;
            actual = 0;
            error = target - actual;
            errorPre1 = 0;
            errorPre2 = 0;
            A = kp + ki + kd;
            B = -2 * kd - kp;
            C = kd;
        }

        public double Control(double tar, double act)
        {
            double result = 0;
            target = tar;
            actual = act;
            error = target - actual;
            result = A * error + B * errorPre1 + C * errorPre2;
            errorPre2 = errorPre1;
            errorPre1 = error;
            
            return result;
        }
    }
}
