using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contensive.BaseClasses;

namespace Contensive.addons.multiFormAjaxSampleV2 {
    public class form1Class : formBaseClass {

        public override int processForm(CPBaseClass cp, int srcFormId, string rqs, DateTime rightnow, applicationClass application)
        {
            int nextFormId = srcFormId;

            try
            {
                string button;
                CPCSBaseClass cs = cp.CSNew();
                string firstName;
                Boolean isInputOK = true;

                // ajax routines return a different name for button

                button = cp.Doc.GetText("ajaxButton");

                if (button=="") {
                    button = cp.Doc.GetText(commonModule.rnButton);
                }


                // check the input requirements
                // if user errors are handled with javascript, no need to display a message, just prevent save

                firstName = cp.Doc.GetText("firstName");
                if (firstName=="") {
                    isInputOK = false;
                }

                // if no user errors, process input
                // if errors, just return default nextFormId which will redisplay this form

                if (isInputOK) { 
                    application.firstName = firstName;
                    application.changed = true;
                    
                    // determine the next form  


                    switch (button)
                    {
                        case commonModule.buttonNext:
                            //
                            nextFormId = commonModule.formIdTwo;
                            break;
                    } 
                }

            }
            catch (Exception ex)
            {
                cp.Site.ErrorReport(ex, "processForm");
            }

            return nextFormId;
        }

        public override string getForm(CPBaseClass cp, int dstFormId, string rqs, DateTime rightNow, applicationClass application) {
            string returnHtml = "";
            try {
                CPBlockBaseClass layout = cp.BlockNew();
                CPCSBaseClass cs = cp.CSNew();
                string body;

                layout.OpenLayout("MultiFormAjaxSample - Form 1");

                // manuiplate the html, pre-populating fields, hiding parts not needed, etc.
                // get the resulting form from the layout object
                // add the srcFormId as a hidden

                layout.SetInner("#mfaFirstNameWrapper", cp.Html.InputText("firstName", application.firstName));

                body = layout.GetHtml();
                body += cp.Html.Hidden(commonModule.rnSrcFormId, dstFormId.ToString());
                returnHtml = cp.Html.Form(body, "", "", "mfaForm1", rqs);

            }
            catch (Exception ex) {
                cp.Site.ErrorReport(ex, "getForm");
            }
            return returnHtml;
        }
        //
        //
        //
        private void errorReport(Exception ex,CPBaseClass cp, string method) {
            cp.Site.ErrorReport(ex, "error in aoManagerTemplate.multiFormAjaxSample." + method);
        }
    }
}
