using System;
using System.Collections.Generic;
using System.Text;
using Contensive.BaseClasses;

namespace Contensive.addons.multiFormAjaxSampleV2
{

    public class frameClass : Contensive.BaseClasses.AddonBaseClass
    {
        //
        // execute method is the only public
        //
        public override object Execute(CPBaseClass cp)
        {
            string returnHtml = "";
            try
            {
                string body = "";
                string rqs = cp.Doc.RefreshQueryString;
                formHandlerClass formHandler = new formHandlerClass();

                cp.Doc.SetProperty("multiformAjaxCsFrameRqs", rqs);
                body = formHandler.Execute(cp).ToString();

                cp.Doc.AddHeadJavascript("var multiformAjaxCsFrameRqs='" + rqs + "';");

                returnHtml = cp.Html.div(body,"","", "multiFormAjaxFrame");
            }
            catch (Exception ex)
            {
                cp.Site.ErrorReport("There was a unexpected exception, " + ex.ToString());
                returnHtml = "Visual Studio add-on assembly - error response";
            }
            return returnHtml;
        }
    }
}
