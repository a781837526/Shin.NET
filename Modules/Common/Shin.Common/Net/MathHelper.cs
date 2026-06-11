// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Common;

/// <summary>
/// 数学帮助类
/// </summary>
public class MathHelper
{
    /// <summary>
    /// 判断两条一维线段之间的关系
    /// </summary>
    /// <param name="A1">第一条线段的起点</param>
    /// <param name="A2">第一条线段的终点</param>
    /// <param name="B1">第二条线段的起点</param>
    /// <param name="B2">第二条线段的终点</param>
    /// <returns>是否交叠</returns>
    public static bool IsCross(double A1, double A2, double B1, double B2)
    {
        //1.A中的最小值大于B中的最小值时
        double A小 = Math.Min(A1, A2);
        double A大 = Math.Max(A1, A2);
        double B小 = Math.Min(B1, B2);
        double B大 = Math.Max(B1, B2);

        if (A小 < B小 && A大 <= B小)
            return false;
        else if (B小 < A小 && B大 <= A小)
            return false;

        return true;
    }

    /// <summary>
    /// 得到start到end之间所有可以整除length的double值，并加入start和end
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static List<double> GetDivisibleNumbers(double start, double end, double length)
    {
        List<double> divisibleNumbers = new List<double>() { start, end };

        for (double i = Math.Ceiling(start / length) * length; i <= end; i += length)
        {
            double d = Math.Round(i / length) * length;
            if (!divisibleNumbers.Contains(d))
            {
                divisibleNumbers.Add(d);
            }
        }
        divisibleNumbers.Sort();
        return divisibleNumbers;
    }

    /// <summary>
    /// 得到小数点后面的小数
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static double GetDotValue(double value)
    {
        try
        {
            return double.Parse(string.Format("0.{0}", value.ToString().Split('.')[1]));
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 计算百分比.
    /// </summary>
    /// <param name="numA">分子.</param>
    /// <param name="numB">分母.</param>
    /// <param name="accuracy">小数点后精度(默认精度：1).</param>
    /// <returns>百分制小数.</returns>
    public static decimal GetPercent(int numA, int numB, int accuracy = 1)
        => (numB != 0 && accuracy >= 0) ? Math.Round(Convert.ToDecimal(numA) / Convert.ToDecimal(numB) * 100, accuracy) : 0;
}