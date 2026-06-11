// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Core.Interface;

/// <summary>
/// 系统文件服务接口
/// </summary>
public interface ISysFileService : IDynamicApiController, ITransient
{
    /// <summary>
    /// 获取文件分页列表 🔖
    /// </summary>
    Task<SqlSugarPagedList<SysFile>> Page(PageFileInput input);

    /// <summary>
    /// 上传文件Base64
    /// </summary>
    Task<SysFile> UploadFileFromBase64(UploadFileFromBase64Input input);

    /// <summary>
    /// 上传多文件
    /// </summary>
    Task<List<SysFile>> UploadFiles(List<IFormFile> files);

    /// <summary>
    /// 根据文件Id或Url下载
    /// </summary>
    Task<IActionResult> DownloadFile(SysFile input);

    /// <summary>
    /// 文件预览
    /// </summary>
    Task<IActionResult> GetPreview(long id);

    /// <summary>
    /// 获取文件流
    /// </summary>
    Task<Stream> GetFileStream(SysFile file);

    /// <summary>
    /// 下载指定文件Base64格式
    /// </summary>
    Task<string> DownloadFileBase64(string url);

    /// <summary>
    /// 删除文件
    /// </summary>
    Task DeleteFile(BaseIdInput input);

    /// <summary>
    /// 更新文件
    /// </summary>
    Task UpdateFile(SysFile input);

    /// <summary>
    /// 获取文件
    /// </summary>
    Task<SysFile> GetFile(long id);

    /// <summary>
    /// 根据文件Id集合获取文件
    /// </summary>
    Task<List<SysFile>> GetFileByIds([FromQuery][FlexibleArray<long>] List<long> ids);

    /// <summary>
    /// 获取文件路径
    /// </summary>
    Task<List<TreeNode>> GetFolder();

    /// <summary>
    /// 上传文件
    /// </summary>
    Task<SysFile> UploadFile(UploadFileInput input, string targetPath = "");

    /// <summary>
    /// 上传头像 🔖
    /// </summary>
    Task<SysFile> UploadAvatar(IFormFile file);

    /// <summary>
    /// 上传电子签名
    /// </summary>
    Task<SysFile> UploadSignature(IFormFile file);

    /// <summary>
    /// 更新文件的业务数据Id
    /// </summary>
    Task UpdateFileByDataId(long dataId, List<SysFile> sysFiles);

    /// <summary>
    /// 删除业务数据对应的文件
    /// </summary>
    Task DeleteFileByDataId(long dataId);
}