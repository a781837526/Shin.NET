// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Shin.Core.Service.Swagger;

[ApiDescriptionSettings(Order = 100, Description = "Swagger")]
public class SwaggerService : IDynamicApiController, ITransient
{
    private readonly SwaggerGenOptions _genOptions;

    public SwaggerService(IOptions<SwaggerGenOptions> genOptions)
    {
        _genOptions = genOptions.Value;
    }

    [DisplayName("获取Swagger接口分组列表"), HttpGet]
    public async Task<IActionResult> GetSwaggerGroups()
    {
        var docs = _genOptions.SwaggerGeneratorOptions.SwaggerDocs;
        if (docs == null || !docs.Any()) Oops.Oh("No Swagger documents configured.");

        return new JsonResult(docs.Select(d => new
        {
            name = d.Key,
            url = $"/swagger/{d.Key}/swagger.json"
        }));
    }
}