#pragma checksum "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "656d8bd6136c19d825306c97e4703ab7a7e049d9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Client_View), @"mvc.1.0.view", @"/Views/Client/View.cshtml")]
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
#line 1 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\_ViewImports.cshtml"
using BonusSystem;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\_ViewImports.cshtml"
using BonusSystem.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"656d8bd6136c19d825306c97e4703ab7a7e049d9", @"/Views/Client/View.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"334b854437598feab12d8d0e3d2578c5fbe29dc0", @"/Views/_ViewImports.cshtml")]
    public class Views_Client_View : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Client>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<p>ClientId: ");
#nullable restore
#line 3 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml"
        Write(Model.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<p>");
#nullable restore
#line 4 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml"
Write(Model.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 4 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml"
               Write(Model.MiddleName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 4 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml"
                                 Write(Model.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<p>Phone number: ");
#nullable restore
#line 5 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml"
            Write(Model.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n\r\n<hr />\r\n\r\n<p>CardId: ");
#nullable restore
#line 9 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml"
      Write(Model.BonusCard.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<p>Number card: ");
#nullable restore
#line 10 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml"
           Write(Model.BonusCard.Number);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<p>Balance: ");
#nullable restore
#line 11 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml"
       Write(Model.BonusCard.Balance);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n<p>\r\n    <div>Create data: ");
#nullable restore
#line 13 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml"
                 Write(Model.BonusCard.CreateDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n    <div>Expiration data: ");
#nullable restore
#line 14 "D:\Программирование\Приложения\BonusSystem\BonusSystem\Views\Client\View.cshtml"
                     Write(Model.BonusCard.ExpirationDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n</p>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Client> Html { get; private set; }
    }
}
#pragma warning restore 1591
