using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contensive.BaseClasses;

namespace Contensive.addons.multiFormAjaxSampleV2
{
    public class form3Class : formBaseClass
    {

        public override int processForm(CPBaseClass cp, int srcFormId, string rqs, DateTime rightnow, applicationClass application)
        {
            int nextFormId = srcFormId;

            try
            {
                string button;
                CPCSBaseClass cs = cp.CSNew();

                // ajax routines return a different name for button

                button = cp.Doc.GetText("ajaxButton");

                if (button == "")
                    {
                        button = cp.Doc.GetText(commonModule.rnButton);
                    }

                if (button == commonModule.buttonFinish)
                    {
                        // process the form
                        application.completed = true;
                        application.changed = true;
                    }

                // determine the next form  

                switch (button)
                {
                    case commonModule.buttonPrevious:
                        //
                        nextFormId = commonModule.formIdTwo;
                        break;
                    case commonModule.buttonFinish:
                        //
                        nextFormId = commonModule.formIdFour;
                        break;
                }

            }
            catch (Exception ex)
            {
                cp.Site.ErrorReport(ex, "processForm");
            }

            return nextFormId;
        }

        public override string getForm(CPBaseClass cp, int dstFormId, string rqs, DateTime rightNow, applicationClass application)
        {
            string returnHtml = "";
            try
            {
                CPBlockBaseClass layout = cp.BlockNew();
                CPCSBaseClass cs = cp.CSNew();
                string body;

                layout.OpenLayout("MultiFormAjaxSample - Form 3");

                // manuiplate the html, pre-populating fields, hiding parts not needed, etc.
                // get the resulting form from the layout object
                // add the srcFormId as a hidden
                // wrap it in a form for the javascript to use during submit

                body = layout.GetHtml();
                body += cp.Html.Hidden(commonModule.rnSrcFormId, dstFormId.ToString());
                returnHtml = cp.Html.Form(body, "", "", "mfaForm3", rqs);

            }
            catch (Exception ex)
            {
                cp.Site.ErrorReport(ex, "getForm");
            }
            return returnHtml;
        }
        //
        //
        //
        private void errorReport(Exception ex, CPBaseClass cp, string method)
        {
            cp.Site.ErrorReport(ex, "error in aoManagerTemplate.multiFormAjaxSample." + method);
        }
    
    
    }
}
