#pragma checksum "C:\Users\Andrej\RiderProjects\TicketApp\Web\Views\Order\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "59801851462e403015ce77f3dd2a48b41570d933"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Web.Views.Order.Views_Order_Index), @"mvc.1.0.view", @"/Views/Order/Index.cshtml")]
namespace Web.Views.Order
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59801851462e403015ce77f3dd2a48b41570d933", @"/Views/Order/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"42de83961b09108a3decbb32b879cf1ab633c81e", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Order_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Domain.Order>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Andrej\RiderProjects\TicketApp\Web\Views\Order\Index.cshtml"
  
    ViewBag.Title = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <h1 class=\"h1\">All orders</h1>\r\n");
#nullable restore
#line 9 "C:\Users\Andrej\RiderProjects\TicketApp\Web\Views\Order\Index.cshtml"
     foreach (var order in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"card\">\r\n            <div class=\"card-body\">\r\n                <h5 class=\"card-title\">Order number: ");
#nullable restore
#line 13 "C:\Users\Andrej\RiderProjects\TicketApp\Web\Views\Order\Index.cshtml"
                                                Write(order.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                <h6 class=\"card-subtitle mb-2 text-muted\">Customer: ");
#nullable restore
#line 14 "C:\Users\Andrej\RiderProjects\TicketApp\Web\Views\Order\Index.cshtml"
                                                               Write(order.User.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                <p class=\"card-text\">Total price: ");
#nullable restore
#line 15 "C:\Users\Andrej\RiderProjects\TicketApp\Web\Views\Order\Index.cshtml"
                                              Write(order.OrderItems.Sum(i => i.MovieTicket.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n            </div>\r\n            <div class=\"card-footer\">\r\n                \r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 21 "C:\Users\Andrej\RiderProjects\TicketApp\Web\Views\Order\Index.cshtml"
        
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Domain.Order>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591