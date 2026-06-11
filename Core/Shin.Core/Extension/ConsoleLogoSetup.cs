// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core;

/// <summary>
/// 控制台logo
/// </summary>
public static class ConsoleLogoSetup
{
    public static void AddConsoleLogo(this IServiceCollection services)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(@"
     ___  _               _   _ ______ _______
    / __\| |   (_)       | \ | |  ____|__   __|
     (__ | |__  _ _ _    |  \| | |__     | |
    \__ \| |_ `| | '_ \  | . ` |  __|    | |
    ___) | | | | | | | |_| |\  | |____   | |
   /____/|_| |_|_|_| |_(_)_| \_|______|  |_| ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"让.NET更简单、更通用、更流行！");
    }
}