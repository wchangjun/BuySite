#pragma checksum "C:\Users\User\Documents\BuySite\BuySite\Views\Product\Upload.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5003c19dfcfb69dd4fee0f9b6ae1cec070e5b2a7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Product_Upload), @"mvc.1.0.view", @"/Views/Product/Upload.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\User\Documents\BuySite\BuySite\Views\_ViewImports.cshtml"
using BuySite;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\User\Documents\BuySite\BuySite\Views\_ViewImports.cshtml"
using BuySite.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5003c19dfcfb69dd4fee0f9b6ae1cec070e5b2a7", @"/Views/Product/Upload.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0c3cc870beedda15a488086e55c45e5c8ac77d54", @"/Views/_ViewImports.cshtml")]
    public class Views_Product_Upload : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("Upload"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"container\">\r\n    <div>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5003c19dfcfb69dd4fee0f9b6ae1cec070e5b2a73572", async() => {
                WriteLiteral(@"
            <div class=""form-group row"">
                <label for=""title"" class=""col-sm-4 col-form-label"">商品名稱</label>
                <div class=""col-sm-8"">
                    <input type=""text"" class=""form-control"" id=""title"" v-model=""hello.title"">
                </div>
            </div>
            <div class=""form-group row"">
                <label for=""pric"" class=""col-sm-4 col-form-label"">價錢</label>
                <div class=""col-sm-8"">
                    <input type=""number"" class=""form-control"" id=""pric"" v-model=""hello.price"">
                </div>
            </div>
            <div class=""form-group row"">
                <label for=""description"" class=""col-sm-4 col-form-label"">敘述</label>
                <div class=""col-sm-8"">
                    <input type=""text"" class=""form-control"" id=""description"" v-model=""hello.description"">
                </div>
            </div>
            <div class=""form-group row"">
                <label for=""pic"" class=""col-sm-4 col-form-la");
                WriteLiteral("bel\">上傳圖片</label>\r\n                <div class=\"col-sm-8\">\r\n");
                WriteLiteral(@"                    <input type=""file"" class=""form-control"" id=""pic"" v-on:change=""up"">
                </div>
            </div>
            <div class=""form-group row"">
                <label class=""col-sm-4 col-form-label"">操作</label>
                <div class=""col-sm-8"">
                    <button type=""button"" class=""btn btn-primary"" v-on:click=""s"">新增</button>
");
                WriteLiteral("                </div>\r\n            </div>\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    </div>
</div>

<script>
    var app = new Vue({
        el: '#Upload',
        data: {
            hello: {
                title: """",
                price: """",
                description: """",
                pic: null
            }
        },
        methods: {
            up: function (event) {
                alert(""1"");
                console.log(event)
                this.hello.pic = event.target.files[0] //限制這個list陣列只能傳一張

            },
            s: function () {
                alert(""1"");
                var bodyFormData = new FormData();
                bodyFormData.append(""Title"", this.hello.title);
                bodyFormData.append(""Pric"", this.hello.price);
                bodyFormData.append(""description"", this.hello.description);
                bodyFormData.append(""pic"", this.hello.pic);

                axios.post(""/Product/Upload"" + FormData)
                    .then((resp) => {
                        console.log(resp);
                    })
");
            WriteLiteral(@"                    .catch((res) => {
                        console.log(res);
                    });
            },
            //send: function () {
            //    alert(""1"");
            //    var bodyFormData = new FormData();
            //    bodyFormData.append(""Title"", this.hello.title);
            //    bodyFormData.append(""Pric"", this.hello.price);
            //    bodyFormData.append(""description"", this.hello.description);
            //    bodyFormData.append(""pic"", this.hello.pic);

            //    axios({
            //        method: ""post"",
            //        url: ""/Product/UploadFile"",
            //        data: bodyFormData,
            //        headers: { ""Content-Type"": ""multipart/form-data"" },
            //    })
            //        .then(function (response) {
            //            //handle success
            //            console.log(response);
            //        })
            //        .catch(function (response) {
            //          ");
            WriteLiteral(@"  //handle error
            //            console.log(response);
            //        });
            //}
        },
    });
</script>
<script>
    $('div').on('submit', function () {
        $('input[type=""submit""]').val('傳送.....');
        /*$(""input[type='submit']"").prop('disabled', true);*/
    });

    //$('div').on('submit', function (event) {
    //    console.log(event)
    //    $
    //});
</script>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
