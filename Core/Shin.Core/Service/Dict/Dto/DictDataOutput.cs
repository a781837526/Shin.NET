// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

public class DictDataOutput
{
    public long DictDataId { get; set; }
    public string TypeCode { get; set; }
    public string Label { get; set; }
    public string Value { get; set; }
    public string Code { get; set; }
    public string TagType { get; set; }
    public string StyleSetting { get; set; }
    public string ClassSetting { get; set; }
    public string ExtData { get; set; }
    public string Remark { get; set; }
    public int OrderNo { get; set; }
    public StatusEnum Status { get; set; }
}