// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 百度翻译
/// </summary>
public interface IBaiDuTranslationService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 百度在线翻译
    /// </summary>
    /// <param name="from">翻译源语种</param>
    /// <param name="to">翻译目标语种</param>
    /// <param name="content">文本内容</param>
    ///<remarks>
    ///源语种和目标语种支持：
    ///zh:简体中文
    ///cht:繁體中文(台灣)
    ///yue:繁體中文(香港)
    ///en:英语
    ///de:德语
    ///spa:西班牙语
    ///fin:芬兰语
    ///fra:法语
    ///it:意大利语
    ///jp:日语
    ///kor:韩语
    ///nor:挪威语
    ///pl:波兰语
    ///pt:葡萄牙语
    ///ru:俄语
    ///th:泰语
    ///id:印度尼西亚语
    ///may:马来西亚
    ///vie:越南语
    ///
    ///更多语种请查看：https://api.fanyi.baidu.com/doc/21
    /// </remarks>
    /// <returns>翻译后的文本内容</returns>
    Task<BaiDuTranslationResult> Translation(string from, string to, string content);

    /// <summary>
    /// 生成前端页面i18n文件
    /// </summary>
    Task GeneratePageI18nFile();

    /// <summary>
    /// 生成前端菜单i18n文件
    /// </summary>
    Task GenerateMenuI18nFile();
}