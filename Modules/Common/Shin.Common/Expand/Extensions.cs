// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Common;

/// <summary>
/// 转换扩展类
/// </summary>
[SuppressSniffer]
public static partial class Extensions
{
    #region 转换为long

    /// <summary>
    /// 将object转换为long,若转换失败,则返回0.不抛出异常.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static long ParseToLong(this object obj)
    {
        try
        {
            return long.Parse(obj.ToString() ?? string.Empty);
        }
        catch
        {
            return 0L;
        }
    }

    /// <summary>
    /// 将object转换为long,若转换失败,则返回指定值.不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static long ParseToLong(this string str, long defaultValue)
    {
        try
        {
            return long.Parse(str);
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion 转换为long

    #region 转换为int

    /// <summary>
    /// 将object转换为int，若转换失败，则返回0。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int ParseToInt(this object str)
    {
        try
        {
            return Convert.ToInt32(str);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 将object转换为int，若转换失败，则返回指定值。不抛出异常
    /// null返回默认值.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int ParseToInt(this object str, int defaultValue)
    {
        if (str == null)
        {
            return defaultValue;
        }

        try
        {
            return Convert.ToInt32(str);
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion 转换为int

    #region 转换为short

    /// <summary>
    /// 将object转换为short，若转换失败，则返回0。不抛出异常.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static short ParseToShort(this object obj)
    {
        try
        {
            return short.Parse(obj.ToString() ?? string.Empty);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 将object转换为short，若转换失败，则返回指定值。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static short ParseToShort(this object str, short defaultValue)
    {
        try
        {
            return short.Parse(str.ToString() ?? string.Empty);
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion 转换为short

    #region 转换为demical

    /// <summary>
    /// 将object转换为demical，若转换失败，则返回指定值。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static decimal ParseToDecimal(this object str, decimal defaultValue)
    {
        try
        {
            return decimal.Parse(str.ToString() ?? string.Empty);
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// 将object转换为demical，若转换失败，则返回0。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static decimal ParseToDecimal(this object str)
    {
        try
        {
            return decimal.Parse(str.ToString() ?? string.Empty);
        }
        catch
        {
            return 0;
        }
    }

    #endregion 转换为demical

    #region 转化为bool

    /// <summary>
    /// 将object转换为bool，若转换失败，则返回false。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool ParseToBool(this object str)
    {
        try
        {
            if (str == null)
                return false;
            bool? value = GetBool(str);
            if (value != null)
                return value.Value;
            bool result;
            return bool.TryParse(str.ToString(), out result) && result;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 将object转换为bool，若转换失败，则返回指定值。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static bool ParseToBool(this object str, bool result)
    {
        try
        {
            return bool.Parse(str.ToString() ?? string.Empty);
        }
        catch
        {
            return result;
        }
    }

    /// <summary>
    /// 获取布尔值.
    /// </summary>
    private static bool? GetBool(this object data)
    {
        switch (data.ToString().Trim().ToLower())
        {
            case "0":
                return false;

            case "1":
                return true;

            case "是":
                return true;

            case "否":
                return false;

            case "yes":
                return true;

            case "no":
                return false;

            default:
                return null;
        }
    }

    #endregion 转化为bool

    #region 转换为float

    /// <summary>
    /// 将object转换为float，若转换失败，则返回0。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static float ParseToFloat(this object str)
    {
        try
        {
            return float.Parse(str.ToString() ?? string.Empty);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 将object转换为float，若转换失败，则返回指定值。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    public static float ParseToFloat(this object str, float result)
    {
        try
        {
            return float.Parse(str.ToString() ?? string.Empty);
        }
        catch
        {
            return result;
        }
    }

    #endregion 转换为float

    #region 转换为Guid

    /// <summary>
    /// 将string转换为Guid，若转换失败，则返回Guid.Empty。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Guid ParseToGuid(this string str)
    {
        try
        {
            return new Guid(str);
        }
        catch
        {
            return Guid.Empty;
        }
    }

    #endregion 转换为Guid

    #region 转换为DateTime

    /// <summary>
    /// 将string转换为DateTime，若转换失败，则返回日期最小值。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static DateTime ParseToDateTime(this string str)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return DateTime.MinValue;
            }

            if (str.Contains("-") || str.Contains("/"))
            {
                return DateTime.Parse(str);
            }

            int length = str.Length;
            return length switch
            {
                4 => DateTime.ParseExact(str, "yyyy", CultureInfo.CurrentCulture),
                6 => DateTime.ParseExact(str, "yyyyMM", CultureInfo.CurrentCulture),
                8 => DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.CurrentCulture),
                10 => DateTime.ParseExact(str, "yyyyMMddHH", CultureInfo.CurrentCulture),
                12 => DateTime.ParseExact(str, "yyyyMMddHHmm", CultureInfo.CurrentCulture),

                // ReSharper disable once StringLiteralTypo
                14 => DateTime.ParseExact(str, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),

                // ReSharper disable once StringLiteralTypo
                _ => DateTime.ParseExact(str, "yyyyMMddHHmmss", CultureInfo.CurrentCulture)
            };
        }
        catch
        {
            return DateTime.MinValue;
        }
    }

    /// <summary>
    /// 将时间戳转为DateTime.
    /// </summary>
    /// <param name="timeStamp">时间戳.</param>
    /// <returns></returns>
    public static DateTime TimeStampToDateTime(this long timeStamp)
    {
        try
        {
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeMilliseconds(timeStamp);
            return dto.ToLocalTime().DateTime;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 将时间戳转为<see cref="DateTime"/>
    /// </summary>
    /// <param name="timeStamp">时间戳</param>
    /// <param name="format">格式字符串</param>
    public static DateTime TimeStampToDateTime(this long timeStamp, string format)
    {
        try
        {
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeMilliseconds(timeStamp);
            return dto.ToLocalTime().DateTime.ParseToDateTime(format);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 将时间戳转为DateTime.
    /// </summary>
    /// <param name="timeStamp">时间戳.</param>
    /// <returns></returns>
    public static DateTime TimeStampToDateTime(this string timeStamp)
    {
        try
        {
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(timeStamp));
            return dto.ToLocalTime().DateTime;
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 将时间戳转为DateTime
    /// </summary>
    /// <param name="timeStamp">时间戳</param>
    /// <param name="format">格式字符串</param>
    public static DateTime TimeStampToDateTime(this string timeStamp, string format)
    {
        try
        {
            DateTimeOffset dto = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(timeStamp));
            return dto.ToLocalTime().DateTime.ParseToDateTime(format);
        }
        catch (Exception)
        {
            throw;
        }
    }

    /// <summary>
    /// 将 DateTime? 转换为 DateTime.
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static DateTime ParseToDateTime(this DateTime? date)
    {
        return Convert.ToDateTime(date);
    }

    /// <summary>
    /// 将 DateTime 根据指定格式转换.
    /// </summary>
    /// <param name="date">时间.</param>
    /// <param name="format">格式字符串.</param>
    /// <returns></returns>
    public static DateTime ParseToDateTime(this DateTime? date, string format)
    {
        return Convert.ToDateTime(string.Format("{0:" + format + "}", date));
    }

    /// <summary>
    /// 将 DateTime 根据指定格式转换
    /// </summary>
    /// <param name="date">时间</param>
    /// <param name="format">格式字符串</param>
    /// <returns></returns>
    public static DateTime ParseToDateTime(this DateTime date, string format)
    {
        return Convert.ToDateTime(string.Format("{0:" + format + "}", date));
    }

    /// <summary>
    /// 将string转换为DateTime，若转换失败，则返回默认值.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static DateTime ParseToDateTime(this string str, DateTime? defaultValue)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return defaultValue.GetValueOrDefault();
            }

            if (str.Contains("-") || str.Contains("/"))
            {
                return DateTime.Parse(str);
            }

            int length = str.Length;
            return length switch
            {
                4 => DateTime.ParseExact(str, "yyyy", CultureInfo.CurrentCulture),
                6 => DateTime.ParseExact(str, "yyyyMM", CultureInfo.CurrentCulture),
                8 => DateTime.ParseExact(str, "yyyyMMdd", CultureInfo.CurrentCulture),
                10 => DateTime.ParseExact(str, "yyyyMMddHH", CultureInfo.CurrentCulture),
                12 => DateTime.ParseExact(str, "yyyyMMddHHmm", CultureInfo.CurrentCulture),

                // ReSharper disable once StringLiteralTypo
                14 => DateTime.ParseExact(str, "yyyyMMddHHmmss", CultureInfo.CurrentCulture),

                // ReSharper disable once StringLiteralTypo
                _ => DateTime.ParseExact(str, "yyyyMMddHHmmss", CultureInfo.CurrentCulture)
            };
        }
        catch
        {
            return defaultValue.GetValueOrDefault();
        }
    }

    /// <summary>
    /// <see cref="DateTime"/>转换成JS时间戳
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ParseToJsTick(this DateTime? dateTime)
    {
        if (dateTime.HasValue)
            return ParseToJsTick(dateTime.Value);
        else
            return long.MinValue;
    }

    /// <summary>
    /// <see cref="DateTime"/>转换成JS时间戳
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ParseToJsTick(this DateTime dateTime)
    {
        DateTime startTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1));
        long timeStamp = (long)(dateTime - startTime).TotalMilliseconds; // 相差毫秒数
        return timeStamp;
    }

    #endregion 转换为DateTime

    #region 转换为string

    /// <summary>
    /// 将object转换为string，若转换失败，则返回""。不抛出异常.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ParseToString(this object obj)
    {
        try
        {
            return obj == null ? string.Empty : obj.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }

    /// <summary>
    /// 将object转换为string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ParseToStrings<T>(this object obj)
    {
        try
        {
            if (obj is IEnumerable<T> list)
            {
                return string.Join(",", list);
            }

            return obj.ToString();
        }
        catch
        {
            return string.Empty;
        }
    }

    #endregion 转换为string

    #region 转换为double

    /// <summary>
    /// 将object转换为double，若转换失败，则返回0。不抛出异常.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static double ParseToDouble(this object obj)
    {
        try
        {
            return double.Parse(obj.ToString() ?? string.Empty);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// 将object转换为double，若转换失败，则返回指定值。不抛出异常.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static double ParseToDouble(this object str, double defaultValue)
    {
        try
        {
            return double.Parse(str.ToString() ?? string.Empty);
        }
        catch
        {
            return defaultValue;
        }
    }

    #endregion 转换为double

    #region 强制转换类型

    /// <summary>
    /// 强制转换类型.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<TResult> CastSuper<TResult>(this IEnumerable source)
    {
        return from object item in source select (TResult)Convert.ChangeType(item, typeof(TResult));
    }

    #endregion 强制转换类型

    #region 转换为ToUnixTime

    public static long ParseToUnixTime(this DateTime nowTime)
    {
        DateTimeOffset dto = new DateTimeOffset(nowTime);
        return dto.ToUnixTimeMilliseconds();
    }

    #endregion 转换为ToUnixTime

    #region 转换为帕斯卡命名法

    /// <summary>
    /// 将字符串转为帕斯卡命名法.
    /// </summary>
    /// <param name="original">源字符串.</param>
    /// <returns></returns>
    public static string ParseToPascalCase(this string original)
    {
        Regex invalidCharsRgx = new Regex("[^_a-zA-Z0-9]");
        Regex whiteSpace = new Regex(@"(?<=\s)");
        Regex startsWithLowerCaseChar = new Regex("^[a-z]");
        Regex firstCharFollowedByUpperCasesOnly = new Regex("(?<=[A-Z])[A-Z0-9]+$");
        Regex lowerCaseNextToNumber = new Regex("(?<=[0-9])[a-z]");
        Regex upperCaseInside = new Regex("(?<=[A-Z])[A-Z]+?((?=[A-Z][a-z])|(?=[0-9]))");

        // 用undescore替换空白，然后用空字符串替换所有无效字符
        var pascalCase = invalidCharsRgx.Replace(whiteSpace.Replace(original, "_"), string.Empty)

            // 用下划线分割
            .Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries)

            // 首字母设置为大写
            .Select(w => startsWithLowerCaseChar.Replace(w, m => m.Value.ToUpper()))

            // 如果没有下一个小写字母(ABC -> Abc)，则将第二个及所有后面的大写字母替换为小写字母
            .Select(w => firstCharFollowedByUpperCasesOnly.Replace(w, m => m.Value.ToLower()))

            // 数字后面的第一个小写字母 设置大写(Ab9cd -> Ab9Cd)
            .Select(w => lowerCaseNextToNumber.Replace(w, m => m.Value.ToUpper()))

            // 第二个小写字母和下一个大写字母，除非最后一个字母后跟任何小写字母 (ABcDEf -> AbcDef)
            .Select(w => upperCaseInside.Replace(w, m => m.Value.ToLower()));

        return string.Concat(pascalCase);
    }

    #endregion 转换为帕斯卡命名法

    #region IsEmpty

    /// <summary>
    /// 是否为空.
    /// </summary>
    /// <param name="value">值.</param>
    public static bool IsEmpty(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }

    /// <summary>
    /// 是否为空.
    /// </summary>
    /// <param name="value">值.</param>
    public static bool IsEmpty(this Guid? value)
    {
        if (value == null)
            return true;
        return IsEmpty(value.Value);
    }

    /// <summary>
    /// 是否为空.
    /// </summary>
    /// <param name="value">值.</param>
    public static bool IsEmpty(this Guid value)
    {
        if (value == Guid.Empty)
            return true;
        return false;
    }

    /// <summary>
    /// 是否为空.
    /// </summary>
    /// <param name="value">值.</param>
    public static bool IsEmpty(this object value)
    {
        if (value != null && !string.IsNullOrEmpty(value.ToString()))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    #endregion IsEmpty

    #region IsNotEmptyOrNull

    /// <summary>
    /// 不为空.
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static string ObjToString(this object thisValue)
    {
        if (thisValue != null) return thisValue.ToString().Trim();
        return string.Empty;
    }

    /// <summary>
    /// 不为空.
    /// </summary>
    /// <param name="thisValue"></param>
    /// <returns></returns>
    public static bool IsNotEmptyOrNull(this object thisValue)
    {
        return ObjToString(thisValue) != string.Empty && ObjToString(thisValue) != "undefined" && ObjToString(thisValue) != "null";
    }

    #endregion IsNotEmptyOrNull

    #region 年龄计算

    /// <summary>
    /// 生成年龄
    /// </summary>
    /// <param name="nowTime">截止日期</param>
    /// <param name="birthday">出生日期</param>
    /// <returns>年龄</returns>
    public static int gotAge(this string nowTime, string birthday)
    {
        return gotAge(nowTime.ParseToDateTime(), birthday.ParseToDateTime());
    }

    /// <summary>
    /// 生成年龄
    /// </summary>
    /// <param name="nowTime">截止日期</param>
    /// <param name="birthday">出生日期</param>
    /// <returns>年龄</returns>
    public static int gotAge(this DateTime nowTime, DateTime birthday)
    {
        int age = nowTime.Year - birthday.Year;
        if (nowTime < birthday || (nowTime.Month == birthday.Month && nowTime < birthday))
            age--;
        return age;
    }

    #endregion 年龄计算

    #region 集合分页

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="source">数据源</param>
    /// <param name="page">页码</param>
    /// <param name="pageSize">每页数量</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<T> Pagination<T>(this IEnumerable<T> source, int page, int pageSize, out int total)
    {
        if (source == null || page < 1)
        {
            total = 0;
            return source;
        }

        total = source.Count();
        return source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }

    #endregion 集合分页

    /// <summary>
    /// 添加合计行
    /// </summary>
    /// <param name="dataSource"></param>
    public static void AddSumRowToDataSource<T>(this List<T> dataSource) where T : new()
    {
        if (dataSource == null || dataSource.Count() == 0)
        {
            return;
        }
        T sumItem = new T();//创建一个对象，用来存合计的值
        Type type = sumItem.GetType(); //对象类型
        IEnumerable<System.Reflection.PropertyInfo> properties = from pi in type.GetProperties() select pi;  //获取此对象所有的属性
        foreach (System.Reflection.PropertyInfo propertyInfo in properties)
        {//对每个属性尝试合计，如果能合计就跳过
            try
            {
                decimal sumValue = dataSource.Sum(e =>
                {
                    Type etype = e.GetType();
                    IEnumerable<System.Reflection.PropertyInfo> eproperty = from pi in etype.GetProperties() where pi.Name == propertyInfo.Name select pi;
                    return Convert.ToDecimal(eproperty.First().GetValue(e, null));
                });

                if (propertyInfo.PropertyType == typeof(float))
                    propertyInfo.SetValue(sumItem, Convert.ToInt16(sumValue));
                else if (propertyInfo.PropertyType == typeof(int))
                    propertyInfo.SetValue(sumItem, Convert.ToInt32(sumValue));
                else if (propertyInfo.PropertyType == typeof(long))
                    propertyInfo.SetValue(sumItem, Convert.ToInt64(sumValue));
                else if (propertyInfo.PropertyType == typeof(double))
                    propertyInfo.SetValue(sumItem, Convert.ToDouble(sumValue));
                else
                    propertyInfo.SetValue(sumItem, sumValue);
            }
            catch (Exception)
            {
                if (propertyInfo.PropertyType == typeof(string))
                    propertyInfo.SetValue(sumItem, "合计");
            }
        }
        dataSource.Add(sumItem);
    }

    #region 数字逻辑

    /// <summary>
    /// 是否是奇数
    /// </summary>
    /// <param name="n">需要判断的数字</param>
    /// <returns></returns>
    public static bool IsOdd(this int n)
    {
        return Convert.ToBoolean(n & 1);
    }

    #endregion 数字逻辑

    #region 自定义方法

    /// <summary>
    /// 将数据表转换为泛型集合
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    /// <param name="table">数据表</param>
    /// <returns>指定类型的泛型集合</returns>
    public static IEnumerable<T> ToEnumerable<T>(this DataTable table) where T : class
    {
        foreach (DataRow row in table.Rows)
        {
            yield return ToEntity<T>(row);
        }
    }

    /// <summary>
    /// 将一行数据转换成实体类
    /// </summary>
    /// <typeparam name="T">实体类</typeparam>
    /// <param name="dRow">数据表某一行</param>
    /// <returns></returns>
    public static T ToEntity<T>(this DataRow dRow) where T : class
    {
        List<string> drItems = new List<string>(dRow.ItemArray.Length);
        for (int i = 0; i < dRow.ItemArray.Length; i++)
        {
            drItems.Add(dRow.Table.Columns[i].ColumnName.ToLower());
        }
        T model = Activator.CreateInstance<T>();
        foreach (PropertyInfo pi in model.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance))
        {
            if (drItems.Contains(pi.Name.ToLower()))
            {
                if (pi.PropertyType.IsEnum) //属性类型是否表示枚举
                {
                    object enumName = System.Enum.ToObject(pi.PropertyType, pi.GetValue(model, null));
                    pi.SetValue(model, enumName, null); //获取枚举值，设置属性值
                }
                else
                {
                    if (!dRow[pi.Name].IsNullOrEmptyOrDBNull())
                    {
                        pi.SetValue(model, MapNullableType(dRow[pi.Name], pi.PropertyType), null);
                    }
                }
            }
        }
        return model;
    }

    /// <summary>
    /// 将<see cref="IEnumerable{T}"/>转换成<see cref="DataTable"/>
    /// </summary>
    /// <param name="entitys">泛类型集合</param>
    /// <returns></returns>
    public static DataTable ToTable<T>(this IEnumerable<T> entitys) => entitys.ToList().ToTable();

    /// <summary>
    /// 将<see cref="List{T}"/>转换成<see cref="DataTable"/>
    /// </summary>
    /// <param name="entitys">泛类型集合</param>
    /// <returns></returns>
    public static DataTable ToTable<T>(this List<T> entitys)
    {
        //检查实体集合不能为空
        if (entitys == null)
        {
            throw new Exception("需转换的集合为空");
        }
        Type entityType = null;
        if (entitys.Count == 0)
        {
            T _t = Activator.CreateInstance<T>();
            entityType = _t.GetType();
        }
        else
        {
            //取出第一个实体的所有Propertie
            entityType = entitys[0].GetType();
        }
        PropertyInfo[] entityProperties = entityType.GetProperties();

        //生成DataTable的structure
        //生产代码中，应将生成的DataTable结构Cache起来，此处略
        DataTable dt = new DataTable();
        for (int i = 0; i < entityProperties.Length; i++)
        {
            //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
            dt.Columns.Add(entityProperties[i].Name);
        }

        //将所有entity添加到DataTable中
        foreach (object entity in entitys)
        {
            //检查所有的的实体都为同一类型
            //if (entity.GetType() != entityType)
            //{
            //    throw new Exception("要转换的集合元素类型不一致");
            //}
            object[] entityValues = new object[entityProperties.Length];
            for (int i = 0; i < entityProperties.Length; i++)
            {
                entityValues[i] = entityProperties[i].GetValue(entity, null);
            }
            dt.Rows.Add(entityValues);
        }
        return dt;
    }

    /// <summary>
    /// 对象克隆
    /// </summary>
    /// <typeparam name="T">克隆实体的类型</typeparam>
    /// <param name="Entity">需要克隆的实体</param>
    /// <returns>相同类型的新实体</returns>
    public static T Clone<T>(this T Entity)
    {
        if (Entity == null)
            return default(T);

        Type type = Entity.GetType();
        object obj = Activator.CreateInstance(type);
        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo propertyInfo in properties)
        {
            if (propertyInfo.CanWrite)
            {
                object value = propertyInfo.GetValue(obj, null);
                if (value != null)
                {
                    propertyInfo.SetValue(obj, value, null);
                }
            }
        }

        return (T)obj;
    }

    /// <summary>
    /// 泛型集合去重
    /// </summary>
    /// <typeparam name="T">去重实体的类型</typeparam>
    /// <param name="entitys">需要去重的实体</param>
    /// <param name="propertyName">去重标识(属性名)</param>
    /// <returns>去重后的内容</returns>
    public static string[] Distinct<T>(this IEnumerable<T> entitys, string propertyName)
    {
        string lastValue = string.Empty;
        List<string> distinctList = new List<string>();

        //检查实体集合不能为空
        if (entitys == null)
        {
            return new string[] { };
        }
        Type entityType = null;
        if (entitys.Count() == 0)
        {
            T _t = Activator.CreateInstance<T>();
            entityType = _t.GetType();
        }
        else
        {
            //取出第一个实体的所有Propertie
            entityType = entitys.First().GetType();
        }
        PropertyInfo[] entityProperties = entityType.GetProperties();

        //检索所有属性的名称是否与指定的名称相同，然后和上一次循环的值对比，不相等，则取出它的值
        foreach (object entity in entitys)
        {
            //检查所有的的实体都为同一类型
            if (entity.GetType() != entityType)
                throw new Exception("集合元素类型不一致");

            for (int i = 0; i < entityProperties.Length; i++)
            {
                if (entityProperties[i].Name == propertyName)
                {
                    string thisValue = entityProperties[i].GetValue(entity, null).ToString();

                    if (string.IsNullOrEmpty(lastValue) || lastValue != thisValue)
                        distinctList.Add(thisValue);

                    lastValue = entityProperties[i].GetValue(entity, null).ToString();
                }
            }
        }

        return distinctList.Distinct().ToArray();
    }

    #region 辅助方法

    /// <summary>
    ///
    /// </summary>
    /// <param name="value"></param>
    /// <param name="mType"></param>
    /// <returns></returns>
    public static object MapNullableType(object value, Type mType)
    {
        if (mType.IsGenericType && mType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
        {
            if (IsNullOrEmptyOrDBNull(value))
                return null;
            System.ComponentModel.NullableConverter nullableConverter = new System.ComponentModel.NullableConverter(mType);
            mType = nullableConverter.UnderlyingType;
        }
        if (mType == typeof(bool) || mType == typeof(Boolean))
        {
            if (value is string)
            {
                if (value.ToString() == "1" || value.ToString() == "true")
                    return true;
                else
                    return false;
            }
        }
        if (mType.IsEnum) //属性类型是否表示枚举
        {
            int intvalue;
            if (int.TryParse(value.ToString(), out intvalue))
                return System.Enum.ToObject(mType, Convert.ToInt32(value));
            else
                return System.Enum.Parse(mType, value.ToString(), false);
        }
        return Convert.ChangeType(value, mType);
    }

    /// <summary>
    /// 判断null或DBNull或空字符串
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNullOrEmptyOrDBNull(this object obj)
    {
        return ((obj is DBNull) || obj == null || string.IsNullOrEmpty(obj.ToString())) ? true : false;
    }

    #endregion 辅助方法

    /// <summary>
    /// 获取指定枚举值中的自定义描述内容
    /// </summary>
    /// <param name="value">枚举值</param>
    /// <returns>返回<see cref="DescriptionAttribute"/>中的描述，若不存在则返回 <c>Empty</c></returns>
    public static string GetDescription(this System.Enum value)
    {
        FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
                                                     typeof(DescriptionAttribute),
                                                     false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }

    /// <summary>
    /// 获取指定描述内容的枚举值
    /// </summary>
    /// <typeparam name="EnumType">枚举类型</typeparam>
    /// <param name="description">自定义描述</param>
    /// <returns>枚举值</returns>
    public static EnumType GetValueByDescription<EnumType>(this string description)
    {
        Type type = typeof(EnumType);
        if (!type.IsEnum)
            throw new ArgumentException("指定的EnumType不是一个枚举类型");
        foreach (string enumName in System.Enum.GetNames(type))
        {
            object enumValue = System.Enum.Parse(type, enumName);
            if (description == ((System.Enum)enumValue).GetDescription())
                return (EnumType)enumValue;
        }
        throw new ArgumentException("没有在指定的EnumType中找到与自定义描述对应的枚举项，请检查参数");
    }

    /// <summary>
    /// <see cref="object"/>转指定对象
    /// </summary>
    public static T ToObject<T>(this object json)
    {
        return _ = json.ToJson().ToObject<T>() ?? default;
    }

    /// <summary>
    /// 自定义控制台输出
    /// </summary>
    /// <param name="consoleStr">输出文本</param>
    /// <param name="color">文本颜色</param>
    public static void ConsoleWrite(this string consoleStr, ConsoleColor color = ConsoleColor.Yellow)
    {
        try
        {
            var originColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(consoleStr);
            Console.ForegroundColor = originColor;
        }
        catch (Exception ex)
        {
            Log.Error("自定义控制台输出过程中出现错误：" + ex.Message);
        }
    }

    /// <summary>
    /// 构建完整的GET请求Url
    /// </summary>
    /// <param name="url">请求Url</param>
    /// <param name="queryParams">GET请求参数</param>
    public static string GetUrlBuilder(this string url, IDictionary<string, object> queryParams = null)
    {
        if (url.IsNullOrEmpty())
            return string.Empty;

        UriBuilder uriBuilder = new UriBuilder(url);
        var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        if (!queryParams.IsNullOrEmpty())
            foreach (var param in queryParams)
            {
                if (!param.Value.IsNullOrEmpty())
                    query[param.Key] = param.Value?.ToString();
            }

        uriBuilder.Query = query.ToString();
        return uriBuilder.ToString();
    }

    #endregion 自定义方法
}