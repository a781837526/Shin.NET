// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Common;

/// <summary>
/// 定时任务日志分组名称
/// </summary>
[Const("定时任务日志分组")]
public class LoggingConst
{
    /// <summary>
    /// 大桥设备智慧管家日志分组名称
    /// </summary>
    public const string EquipmentLogging = "Mbec.Logging.Equipment";

    /// <summary>
    /// 大桥设备智慧管家考核日志分组名称
    /// </summary>
    public const string EquipmentAssessmentLogging = "Mbec.Logging.Equipment.Assessment";

    /// <summary>
    /// 机械指挥官设备日志分组名称
    /// </summary>
    public const string MachineComMachineLogging = "Mbec.Logging.MachineCom.Machine";

    /// <summary>
    /// 机械指挥官船舶日志分组名称
    /// </summary>
    public const string MachineComShippingLogging = "Mbec.Logging.MachineCom.Shipping";
}