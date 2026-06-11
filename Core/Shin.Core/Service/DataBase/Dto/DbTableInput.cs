// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class DbTableInput
{
    public string ConfigId { get; set; }

    public string TableName { get; set; }

    public string Description { get; set; }

    public List<DbColumnInput> DbColumnInfoList { get; set; }
}

public class UpdateDbTableInput
{
    public string ConfigId { get; set; }

    public string TableName { get; set; }

    public string OldTableName { get; set; }

    public string Description { get; set; }
}

public class DeleteDbTableInput
{
    public string ConfigId { get; set; }

    public string TableName { get; set; }
}