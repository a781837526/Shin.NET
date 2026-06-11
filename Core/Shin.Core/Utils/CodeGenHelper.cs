// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using DbType = SqlSugar.DbType;

namespace Shin.Core;

/// <summary>
/// 代码生成帮助类
/// </summary>
public static class CodeGenHelper
{
    /// <summary>
    /// .NET与前端类型映射表
    /// </summary>
    private static readonly Dictionary<string, string> NetToTypeScriptMap = new() {
        { "bool", "boolean" },
        { "int", "number" },
        { "long", "number" },
        { "double", "number" },
        { "float", "number" },
        { "decimal", "number" },
        { "byte", "number" },
        { "datetime", "string" },
        { "guid", "string" },
        { "string", "string" },
    };

    /// <summary>
    /// 转换大驼峰法命名
    /// </summary>
    /// <param name="columnName">字段名</param>
    /// <param name="dbColumnNames">EntityBase 实体属性名称</param>
    /// <returns></returns>
    public static string CamelColumnName(string columnName, string[] dbColumnNames)
    {
        if (columnName.Contains('_'))
        {
            var arrColName = columnName.Split('_');
            var sb = new StringBuilder();
            foreach (var col in arrColName)
            {
                if (col.Length > 0)
                    sb.Append(col[..1].ToUpper() + col[1..].ToLower());
            }
            columnName = sb.ToString();
        }
        else
        {
            var propertyName = dbColumnNames.FirstOrDefault(u => u.ToLower() == columnName.ToLower());
            if (!string.IsNullOrEmpty(propertyName))
            {
                columnName = propertyName;
            }
            else
            {
                columnName = columnName[..1].ToUpper() + columnName[1..].ToLower();
            }
        }
        return columnName;
    }

    // 根据数据库类型来处理对应的数据字段类型
    public static string ConvertDataType(DbColumnInfo dbColumnInfo, DbType dbType = DbType.Custom)
    {
        if (dbType == DbType.Custom)
            dbType = App.GetOptions<DbConnectionOptions>().ConnectionConfigs[0].DbType;

        var dataType = dbType switch
        {
            DbType.Oracle => ConvertDataType_OracleSQL(string.IsNullOrEmpty(dbColumnInfo.OracleDataType) ? dbColumnInfo.DataType : dbColumnInfo.OracleDataType, dbColumnInfo.Length, dbColumnInfo.Scale),
            DbType.Dm => ConvertDataType_Dm(string.IsNullOrEmpty(dbColumnInfo.OracleDataType) ? dbColumnInfo.DataType : dbColumnInfo.OracleDataType, dbColumnInfo.Length, dbColumnInfo.Scale),
            DbType.PostgreSQL => ConvertDataType_PostgreSQL(dbColumnInfo.DataType),
            _ => ConvertDataType_Default(dbColumnInfo.DataType),
        };
        return dataType + (dbColumnInfo.IsNullable ? "?" : "");
    }

    // 达梦(DM)数据类型对应的字段类型
    public static string ConvertDataType_Dm(string dataType, int? length, int? scale)
    {
        return ConvertDataType_OracleSQL(dataType, length, scale); //达梦兼容Oracle，目前先这样实现
    }

    // OracleSQL数据类型对应的字段类型
    public static string ConvertDataType_OracleSQL(string dataType, int? length, int? scale)
    {
        switch (dataType.ToLower())
        {
            case "interval year to month": return "int";

            case "interval day to second": return "TimeSpan";

            case "smallint": return "Int16";

            case "int":
            case "integer": return "int";

            case "long": return "long";

            case "float": return "float";

            case "decimal": return "decimal";

            case "number":
                if (length == null) return "decimal";
                return scale switch
                {
                    > 0 => "decimal",
                    0 or null when length is > 1 and < 12 => "int",
                    0 or null when length > 11 => "long",
                    _ => length == 1 ? "bool" : "decimal"
                };

            case "char":
            case "clob":
            case "nclob":
            case "nchar":
            case "nvarchar":
            case "varchar":
            case "nvarchar2":
            case "varchar2":
            case "rowid":
                return "string";

            case "timestamp":
            case "timestamp with time zone":
            case "timestamptz":
            case "timestamp without time zone":
            case "date":
            case "time":
            case "time with time zone":
            case "timetz":
            case "time without time zone":
                return "DateTime";

            case "bfile":
            case "blob":
            case "raw":
                return "byte[]";

            default:
                return "object";
        }
    }

    // PostgreSQL数据类型对应的字段类型
    public static string ConvertDataType_PostgreSQL(string dataType)
    {
        return dataType switch
        {
            "int2" or "smallint" => "Int16",
            "int4" or "integer" => "int",
            "int8" or "bigint" => "long",
            "float4" or "real" => "float",
            "float8" or "double precision" => "double",
            "numeric" or "decimal" or "path" or "point" or "polygon" or "interval" or "lseg" or "macaddr" or "money" => "decimal",
            "boolean" or "bool" or "box" or "bytea" => "bool",
            "varchar" or "character varying" or "geometry" or "name" or "text" or "char" or "character" or "cidr" or "circle" or "tsquery" or "tsvector" or "txid_snapshot" or "xml" or "json" => "string",
            "uuid" => "Guid",
            "timestamp" or "timestamp with time zone" or "timestamptz" or "timestamp without time zone" or "date" or "time" or "time with time zone" or "timetz" or "time without time zone" => "DateTime",
            "bit" or "bit varying" => "byte[]",
            "varbit" => "byte",
            _ => "object",
        };
    }

    // 默认数据类型
    public static string ConvertDataType_Default(string dataType)
    {
        return dataType.ToLower() switch
        {
            "tinytext" or "mediumtext" or "longtext" or "mid" or "text" or "varchar" or "char" or "nvarchar" or "nchar" or "string" or "timestamp" => "string",
            "int" or "integer" or "int32" => "int",
            "smallint" => "Int16",
            //"tinyint" => "byte",
            "tinyint" => "bool", // MYSQL
            "bigint" or "int64" => "long",
            "bit" or "boolean" => "bool",
            "money" or "smallmoney" or "numeric" or "decimal" => "decimal",
            "real" => "Single",
            "datetime" or "datetime2" or "smalldatetime" => "DateTime",
            "date" => "DateOnly", // MYSQL
            "time" => "TimeOnly", // MYSQL
            "float" or "double" => "double",
            "image" or "binary" or "varbinary" => "byte[]",
            "uniqueidentifier" => "Guid",
            _ => "object",
        };
    }

    ///// <summary>
    ///// 数据类型转显示类型
    ///// </summary>
    ///// <param name="dataType"></param>
    ///// <returns></returns>
    //public static CodeGenEffectTypeEnum DataTypeToEff(string dataType)
    //{
    //    return dataType?.TrimEnd('?') switch
    //    {
    //        "int" => CodeGenEffectTypeEnum.InputNumber,
    //        "long" => CodeGenEffectTypeEnum.InputNumber,
    //        "float" => CodeGenEffectTypeEnum.InputNumber,
    //        "double" => CodeGenEffectTypeEnum.InputNumber,
    //        "decimal" => CodeGenEffectTypeEnum.InputNumber,
    //        "DateTime" => CodeGenEffectTypeEnum.DatePicker,
    //        "bool" => CodeGenEffectTypeEnum.Switch,
    //        _ => CodeGenEffectTypeEnum.Input,
    //    };
    //}

    // 是否通用字段
    public static bool IsCommonColumn(string columnName)
    {
        var columnList = new List<string>()
        {
            nameof(EntityBaseOrg.OrgId),
            //nameof(EntityBaseOrg.CreateOrgName),
            nameof(EntityBaseTenant.TenantId),
            nameof(EntityBase.CreateTime),
            nameof(EntityBase.UpdateTime),
            nameof(EntityBase.CreateUserId),
            nameof(EntityBase.UpdateUserId),
            nameof(EntityBase.CreateUserName),
            nameof(EntityBase.UpdateUserName),
            nameof(EntityBaseDel.IsDelete)
        };
        return columnList.Contains(columnName);
    }

    /// <summary>
    /// 获取数据库中真实名称
    /// </summary>
    /// <param name="name"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static string GetRealName(string name, DbConnectionConfig config)
    {
        if (string.IsNullOrWhiteSpace(name)) return null;
        string realName = config!.DbSettings.EnableUnderLine ? UtilMethods.ToUnderLine(name) : name;
        if (config.DbType == DbType.PostgreSQL) realName = realName.ToLower();
        return realName;
    }

    /// <summary>
    /// 获取前端类型
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>脚本
    public static string GetTypeScriptType(string type)
    {
        type = type?.Trim().TrimEnd('?').ToLower();
        if (string.IsNullOrWhiteSpace(type)) return "";

        // 判断是否为枚举
        if (type.EndsWith(nameof(Enum))) return "number";

        // 其他类型从类型映射表中取
        return NetToTypeScriptMap.TryGetValue(type, out var result) ? result : "any";
    }
}
