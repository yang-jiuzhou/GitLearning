using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBBio.Evaluation
{
    public class PeakMath
    {
        /// <summary>
        /// 计算0.607处标准偏差，半峰宽为2.35倍，峰宽为4倍
        /// </summary>
        /// <param name="_X"></param>
        /// <param name="_Y"></param>
        /// <param name="_Peek"></param>
        /// <returns></returns>
        public static double CalcSd(double[] _X, double[] _Y, PeakIntegration _Peek)
        {
            //半峰宽0.607峰高处
            double k = (_Peek.EndBaseY - _Peek.StartBaseY) / (_X[_Peek.EndPoint] - _X[_Peek.StartPoint]);
            double b = _Peek.StartBaseY - k * _X[_Peek.StartPoint];
            double hh = ((_Y[_Peek.PeekPoint] - (k * _X[_Peek.PeekPoint] + b)) * 0.607) + (k * _X[_Peek.PeekPoint] + b);

            List<double> ALisDistance = new List<double>();
            List<double> BLisDistance = new List<double>();

            for (int i = _Peek.StartPoint; i < _Peek.PeekPoint; i++)
                ALisDistance.Add(Math.Abs(_Y[i] - hh));
            for (int j = _Peek.PeekPoint; j < _Peek.EndPoint; j++)
                BLisDistance.Add(Math.Abs(_Y[j] - hh));
            if (ALisDistance.Count == 0 || BLisDistance.Count == 0)
                return -1;
            int AIndex = ALisDistance.IndexOf(ALisDistance.Min()) + _Peek.StartPoint;
            int BIndex = BLisDistance.IndexOf(BLisDistance.Min()) + _Peek.PeekPoint;

            return Math.Round((_X[BIndex] - _X[AIndex]) / 2, 3);
        }

        /// <summary>
        /// 计算半峰宽
        /// </summary>
        /// <param name="_X"></param>
        /// <param name="_Y"></param>
        /// <param name="_Peek"></param>
        /// <returns></returns>
        public static double CalHalfWidth(double[] _X, double[] _Y, PeakIntegration _Peek)
        {
            return Math.Round(CalcSd(_X, _Y, _Peek) * 2.355, 3);
        }


        /// <summary>
        /// 计算分离度
        /// </summary>
        /// <param name="_X"></param>
        /// <param name="_Y"></param>
        /// <param name="_Peek"></param>
        /// <returns></returns>
        public static double CalcResolution(double[] _X, double[] _Y, PeakIntegration _Peek1, PeakIntegration _Peek2)
        {
            double W1 = CalcSd(_X, _Y, _Peek1) * 2;
            double W2 = CalcSd(_X, _Y, _Peek2) * 2;

            double tr1 = _X[_Peek1.PeekPoint];
            double tr2 = _X[_Peek2.PeekPoint];
            return Math.Round((tr2 - tr1) / (W1 + W2), 3); //为了结果体现Clarity一致性，没有乘以2
        }

        /// <summary>
        /// 计算不对称因子AS
        /// </summary>
        /// <param name="_X"></param>
        /// <param name="_Y"></param>
        /// <param name="_Peek"></param>
        /// <returns></returns>
        public static double CalcAS(double[] _X, double[] _Y, PeakIntegration _Peek)
        {
            //半峰宽5%峰高处
            double k = (_Peek.EndBaseY - _Peek.StartBaseY) / (_X[_Peek.EndPoint] - _X[_Peek.StartPoint]);
            double b = _Peek.StartBaseY - k * _X[_Peek.StartPoint];
            double hh = (_Y[_Peek.PeekPoint] - (k * _X[_Peek.PeekPoint] + b)) * 0.1;

            List<double> ALisDistance = new List<double>();
            List<double> BLisDistance = new List<double>();

            for (int i = _Peek.StartPoint; i < _Peek.PeekPoint; i++)
                ALisDistance.Add(Math.Abs(_Y[i] - hh));
            for (int j = _Peek.PeekPoint; j < _Peek.EndPoint; j++)
                BLisDistance.Add(Math.Abs(_Y[j] - hh));

            if (ALisDistance.Count == 0 || BLisDistance.Count == 0)
                return -1;

            int AIndex = ALisDistance.IndexOf(ALisDistance.Min()) + _Peek.StartPoint;
            int BIndex = BLisDistance.IndexOf(BLisDistance.Min()) + _Peek.PeekPoint;

            double A = _X[_Peek.PeekPoint] - _X[AIndex];
            double B = _X[BIndex] - _X[_Peek.PeekPoint];

            return Math.Round((A + B) / (2 * A), 3);
        }

        /// <summary>
        /// 计算拖尾因子tf
        /// </summary>
        /// <param name="_X"></param>
        /// <param name="_Y"></param>
        /// <param nam e="_Peek"></param>
        /// <returns></returns>
        public static double CalcTF(double[] _X, double[] _Y, PeakIntegration _Peek)
        {
            //半峰宽5%峰高处
            double k = (_Peek.EndBaseY - _Peek.StartBaseY) / (_X[_Peek.EndPoint] - _X[_Peek.StartPoint]);
            double b = _Peek.StartBaseY - k * _X[_Peek.StartPoint];
            double hh = (_Y[_Peek.PeekPoint] - (k * _X[_Peek.PeekPoint] + b)) * 0.05;

            List<double> ALisDistance = new List<double>();
            List<double> BLisDistance = new List<double>();

            for (int i = _Peek.StartPoint; i < _Peek.PeekPoint; i++)
                ALisDistance.Add(Math.Abs(_Y[i] - hh));
            for (int j = _Peek.PeekPoint; j < _Peek.EndPoint; j++)
                BLisDistance.Add(Math.Abs(_Y[j] - hh));

            if (ALisDistance.Count == 0 || BLisDistance.Count == 0)
                return -1;
            int AIndex = ALisDistance.IndexOf(ALisDistance.Min()) + _Peek.StartPoint;
            int BIndex = BLisDistance.IndexOf(BLisDistance.Min()) + _Peek.PeekPoint;
            double A = _X[_Peek.PeekPoint] - _X[AIndex];
            double B = _X[BIndex] - _X[_Peek.PeekPoint];

            return Math.Round((A + B) / (2 * A), 3);
        }

        /// <summary>
        /// 理论塔板数计算
        /// </summary>
        /// <param name="_X">X轴总数组</param>
        /// <param name="_ Y">Y轴总数组</param>
        /// <param name="_Peek">峰实体</param>
        /// <param name="_CH">柱长厘米</param>
        /// <returns></returns>
        public static double CalcTPN(double[] _X, double[] _Y, PeakIntegration _Peek, double _CH)
        {

            //半峰宽可视化
            //Share.MessageBoxWin.Show(_X[_Peek.StartPoint].ToString() + "  " + _X[AIndex].ToString() + "  " + _X[BIndex].ToString() + "  " + _X[_Peek.EndPoint].ToString());

            //峰保留时间
            double tr = _X[_Peek.PeekPoint];
            //半峰宽
            double S = CalcIntegration(_X, _Y, _Peek);
            //double H = _Y[_Peek.PeekPoint] - (k * _X[_Peek.PeekPoint] + b);
            //double hw = S / (H * Math.Sqrt(2 * Math.PI)) / 60; //积分求半峰宽
            double hw = CalcSd(_X, _Y, _Peek) * 2.355; //0.607处峰宽除以2倍得到标准偏差，乘以2.355得到半峰宽
            //double w = (_X[BIndex] - _X[AIndex]) * 2; //0.607处峰宽除以2倍得到标准偏差，乘以4得到峰宽

            double result = 5.54 * Math.Pow((tr / hw), 2); //半峰宽计算柱效
            //double result = 16 * Math.Pow((tr / w), 2); //峰宽计算柱效
            return Math.Round(result * 100 / _CH, 3);
        }

        /// <summary>
        /// 积分运算
        /// </summary>
        /// <param name="_X">X轴总数组</param>
        /// <param name="_Y">Y轴总数组</param>
        /// <param name="_Peek">峰实体</param>
        /// <returns>积分结果</returns>
        public static double CalcIntegration(double[] _X, double[] _Y, PeakIntegration _Peek /*IntegrationType _type = IntegrationType.Other*/)
        {
            double K = (_Peek.EndBaseY - _Peek.StartBaseY) / (_X[_Peek.EndPoint] - _X[_Peek.StartPoint]);
            double B = _Peek.StartBaseY - K * _X[_Peek.StartPoint];
            int count = _Peek.EndPoint - _Peek.StartPoint + 1;
            if (count == 1)
                return 0;
            double h = (_X[_Peek.EndPoint] - _X[_Peek.StartPoint]) / count;
            double result = 0;
            for (int i = _Peek.StartPoint; i < _Peek.EndPoint - 1; i++)
            {
                //下底
                double down = Math.Abs(_Y[i] - (K * _X[i] + B));
                //上底
                double up = Math.Abs(_Y[i + 1] - (K * _X[i + 1] + B));
                //积分累加
                result += (up + down) * h / 2;
            }
            return Math.Round(result, 3);//积分面积：S*mV
        }

        /// <summary>
        /// 峰高运算
        /// </summary>
        /// <param name="_X">X轴总数组</param>
        /// <param name="_Y">Y轴总数组</param>
        /// <param name="_Peek">峰实体</param>
        /// <returns>积分结果</returns>
        public static double CalcPeekHigh(double[] _X, double[] _Y, PeakIntegration _Peek)
        {
            double K = (_Peek.EndBaseY - _Peek.StartBaseY) / (_X[_Peek.EndPoint] - _X[_Peek.StartPoint]);
            double B = _Peek.StartBaseY - K * _X[_Peek.StartPoint];
            double high = Math.Round(_Y[_Peek.PeekPoint] - (K * _X[_Peek.PeekPoint] + B), 3);
            return high;
        }

        /// <summary>
        /// 噪音计算(至少30秒)
        /// </summary>
        /// <param name="_X"></param>
        /// <param name="_Y"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static double CalcAstmNoise(double[] _X, double[] _Y, double startTime, double endTime)
        {
            if (null == _X || null == _Y)
            {
                return 0;
            }

            if (endTime - startTime < 0.5)
            {
                return 0;
            }

            double[] x0 = _X, y0 = _Y;
            List<double> listX = new List<double>();
            List<double> listY = new List<double>();
            for (int i = 0; i < x0.Length; i++)
            {
                listX.Add(x0[i]);
                listY.Add(y0[i]);
            }
            //数据清洗，去除毛刺
            double avg = listY.Average();
            for (int i = 0; i < listX.Count; i++)
            {
                if (System.Math.Abs(listY[i]) > 3 * avg)
                {
                    listX.RemoveAt(i);
                    listY.RemoveAt(i);
                }
            }
            x0 = listX.ToArray();
            y0 = listY.ToArray();

            if (x0.Length != y0.Length)
                return 0;

            //按时间分割成新数组
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < x0.Length; i++)
            {
                if (x0[i] <= startTime)
                    startIndex = i;
                if (x0[i] <= endTime)
                    endIndex = i;
            }
            double[] x = new double[endIndex - startIndex + 1];
            double[] y = new double[endIndex - startIndex + 1];
            Array.Copy(x0, startIndex, x, 0, endIndex - startIndex + 1);
            Array.Copy(y0, startIndex, y, 0, endIndex - startIndex + 1);

            //以30秒为分割，记录X轴坐标。
            List<int> listIndex = new List<int>();

            //记录一共有多少间隔和间隔下标
            int count = 1;
            List<int> intervalIndex = new List<int>();
            intervalIndex.Add(0);
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] > 0.5 * count + x[0])
                {
                    count++;
                    intervalIndex.Add(i);
                }
            }

            List<double> Ns = new List<double>();
            //计算每个周期回归函数和峰噪声（N=Imax-Imin),Imax是最大漂移量，Imin是最小漂移量
            for (int i = 1; i < intervalIndex.Count; i++)
            {
                //周期内数量
                int n = intervalIndex[i] - intervalIndex[i - 1] + 1;
                double[] xxx = new double[n];
                double[] yyy = new double[n];
                Array.Copy(x, intervalIndex[i - 1], xxx, 0, n);
                Array.Copy(y, intervalIndex[i - 1], yyy, 0, n);
                Linear linear = new Linear();
                var result = linear.LinearResult(xxx, yyy);
                double A = result[0];
                double B = result[1];

                List<double> I = new List<double>();
                for (int j = 0; j < xxx.Length; j++)
                {
                    double r = A * xxx[j] + B;
                    I.Add(yyy[j] - r);
                }

                double Imax = I.Max();
                double Imin = I.Min();
                double N = System.Math.Abs(Imax) + System.Math.Abs(Imin);
                //double N = Imax - Imin;
                Ns.Add(N);
            }
            //计算ASTM
            return Math.Round(Ns.Average(), 3);
        }

        /// <summary>
        /// 噪音计算
        /// </summary>
        /// <param name="_X"></param>
        /// <param name="_Y"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static double CalcSixSigmaNoise(double[] _X, double[] _Y, double startTime, double endTime)
        {
            if (null == _X || null == _Y)
            {
                return 0;
            }

            double[] x0 = _X, y0 = _Y;
            List<double> listX = new List<double>();
            List<double> listY = new List<double>();
            for (int i = 0; i < x0.Length; i++)
            {
                listX.Add(x0[i]);
                listY.Add(y0[i]);
            }
            //数据清洗，去除毛刺
            double avg = listY.Average();
            for (int i = 0; i < listX.Count; i++)
            {
                if (System.Math.Abs(listY[i]) > 3 * avg)
                {
                    listX.RemoveAt(i);
                    listY.RemoveAt(i);
                }
            }
            x0 = listX.ToArray();
            y0 = listY.ToArray();

            if (x0.Length != y0.Length)
                return 0;

            //按时间分割成新数组
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < x0.Length; i++)
            {
                if (x0[i] <= startTime)
                    startIndex = i;
                if (x0[i] <= endTime)
                    endIndex = i;
            }
            double[] x = new double[endIndex - startIndex + 1];
            double[] y = new double[endIndex - startIndex + 1];
            Array.Copy(x0, startIndex, x, 0, endIndex - startIndex + 1);
            Array.Copy(y0, startIndex, y, 0, endIndex - startIndex + 1);

            //线性回归
            Linear linear = new Linear();
            var result = linear.LinearResult(x, y);
            double A = result[0];
            double B = result[1];
            int n = x.Length;
            //计算离散的平方和
            List<double> lisIntervals = new List<double>();
            for (int i = 0; i < n; i++)
            {
                double r = Math.Pow((y[i] - (A * x[i] + B)), 2);
                lisIntervals.Add(r);
            }
            //计算方差
            double DX = lisIntervals.Sum() / n;
            //计算标准差
            double res = Math.Sqrt(DX);
            return Math.Round(res * 6, 3);
        }

        /// <summary>
        /// 漂移计算
        /// </summary>
        /// <param name="_X"></param>
        /// <param name="_Y"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static double CalcDrift(double[] _X, double[] _Y, double startTime, double endTime)
        {
            if (null == _X || null == _Y)
            {
                return 0;
            }

            double[] x0 = _X, y0 = _Y;
            List<double> listX = new List<double>();
            List<double> listY = new List<double>();
            for (int i = 0; i < x0.Length; i++)
            {
                listX.Add(x0[i]);
                listY.Add(y0[i]);
            }
            //数据清洗，去除毛刺
            double avg = listY.Average();
            for (int i = 0; i < listX.Count; i++)
            {
                if (System.Math.Abs(listY[i]) > 3 * avg)
                {
                    listX.RemoveAt(i);
                    listY.RemoveAt(i);
                }
            }
            x0 = listX.ToArray();
            y0 = listY.ToArray();

            if (x0.Length != y0.Length)
                return 0;

            //按时间分割成新数组
            int startIndex = 0;
            int endIndex = 0;
            for (int i = 0; i < x0.Length; i++)
            {
                if (x0[i] <= startTime)
                    startIndex = i;
                if (x0[i] <= endTime)
                    endIndex = i;
            }
            double[] x = new double[endIndex - startIndex + 1];
            double[] y = new double[endIndex - startIndex + 1];
            Array.Copy(x0, startIndex, x, 0, endIndex - startIndex + 1);
            Array.Copy(y0, startIndex, y, 0, endIndex - startIndex + 1);

            //线性回归
            Linear linear = new Linear();
            var result = linear.LinearResult(x, y);
            double A = result[0];
            double B = result[1];

            return Math.Round(A, 3);
        }
    }

    // 线性回归
    public class Linear
    {
        public double[] LinearResult(double[] arrayX, double[] arrayY)
        {
            double[] result = { 0, 0 };

            if (arrayX.Length == arrayY.Length)
            {
                double averX = arrayX.Average();
                double averY = arrayY.Average();
                result[0] = Scale(averX, averY, arrayX, arrayY);
                result[1] = Offset(result[0], averX, averY);
            }

            return result;
        }

        private double Scale(double averX, double averY, double[] arrayX, double[] arrayY)
        {
            double scale = 0;
            if (arrayX.Length == arrayY.Length)
            {
                double Molecular = 0;
                double Denominator = 0;
                for (int i = 0; i < arrayX.Length; i++)
                {
                    Molecular += (arrayX[i] - averX) * (arrayY[i] - averY);
                    Denominator += Math.Pow((arrayX[i] - averX), 2);
                }
                scale = Molecular / Denominator;
            }

            return scale;
        }

        private double Offset(double scale, double averX, double averY)
        {
            double offset = 0;
            offset = averY - scale * averX;
            return offset;
        }
    }
}