// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 系统租户表种子数据
/// </summary>
public class SysTenantSeedData : ISqlSugarEntitySeedData<SysTenant>
{
    /// <summary>
    /// 种子数据
    /// </summary>
    /// <returns></returns>
    public IEnumerable<SysTenant> HasData()
    {
        var defaultDbConfig = App.GetOptions<DbConnectionOptions>().ConnectionConfigs[0];
        var userList = new SysUserSeedData().HasData().ToList();
        var admin = userList.First(u => u.Account == "Shin.NET");
        return new[]
        {
            new SysTenant{ Id=SqlSugarConst.DefaultTenantId, OrgId=SqlSugarConst.DefaultTenantId, UserId=admin.Id, Host="gitee.com", TenantType=TenantTypeEnum.Id, DbType=defaultDbConfig.DbType, Connection=defaultDbConfig.ConnectionString, ConfigId=SqlSugarConst.MainConfigId, Logo="/upload/logo.png", Title="Shin.NET", ViceTitle="Shin.NET", ViceDesc="", Watermark="Shin.NET", Copyright="Copyright \u00a9 2021-present Shin.NET All rights reserved.", Icp="省ICP备12345678号", IcpUrl="https://beian.miit.gov.cn", Remark="系统默认", CreateTime=DateTime.Parse("2022-02-10 00:00:00") },
        };
    }
}