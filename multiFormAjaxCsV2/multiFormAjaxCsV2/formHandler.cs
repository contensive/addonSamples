using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contensive.addons.multiFormAjaxSampleV2
{
    public class formHandlerClass : Contensive.BaseClasses.AddonBaseClass
    {
        // public override Object
        public override object Execute(Contensive.BaseClasses.CPBaseClass cp)
        {
            string returnHtml = "";
            try
            {

                // all form classes inherit from formBase, so this one form object can be used for every form type required
                // refresh query string is everying needed on the query string to refresh this page. Used for links, etc to always submit to the same page.
                // rightNow is convenient when you need to test time dependant options, you can force the rightNow with a site property, etc
                // srcFormId is the form number that submitted the request (if it was a form submission) and should be a hidden on all forms
                // dstFormId is a way to force a form to display without a form submission

                string body = "";
                formBaseClass form;
                string rqs = cp.Doc.GetProperty("multiformAjaxCsFrameRqs");
                DateTime rightNow = commonModule.getRightNow(cp);
                int srcFormId = cp.Utils.EncodeInteger(cp.Doc.GetProperty(commonModule.rnSrcFormId));
                int dstFormId = cp.Utils.EncodeInteger(cp.Doc.GetProperty(commonModule.rnDstFormId));
                formHandlerClass formHandler = new formHandlerClass();
                applicationClass application;

                //get previously started application

                application = commonModule.getApplication(cp, false);

                // if there is no application, only allow form one

                if (application.id == 0) {
                    if (srcFormId != commonModule.formIdOne) {
                        srcFormId = 0;
                    }
                    dstFormId = commonModule.formIdOne;
                }

                // process forms

                if (srcFormId != 0) {
                    switch (srcFormId) {
                        case commonModule.formIdOne:
                            //
                            form = new form1Class();
                            dstFormId = form.processForm(cp, srcFormId, rqs, rightNow, application);
                            break;
                        case commonModule.formIdTwo:
                            //
                            form = new form2Class();
                            dstFormId = form.processForm(cp, srcFormId, rqs, rightNow, application);
                            break;
                        case commonModule.formIdThree:
                            //
                            form = new form3Class();
                            dstFormId = form.processForm(cp, srcFormId, rqs, rightNow, application);
                            break;
                        case commonModule.formIdFour:
                            //
                            form = new form4Class();
                            dstFormId = form.processForm(cp, srcFormId, rqs, rightNow, application);
                            break;
                    } 
                }
                
                // get the next form that should appear on the page. 
                // put the default form as the else case - to display if nothing else is selected


                switch (dstFormId) {
                    case commonModule.formIdFour:
                        //
                        //
                        //
                        form = new form4Class();
                        body = form.getForm(cp, dstFormId, rqs, rightNow, application);
                        break;
                    case commonModule.formIdThree:
                        //
                        //
                        //
                        form = new form3Class();
                        body = form.getForm(cp, dstFormId, rqs, rightNow, application);
                        break;
                    case commonModule.formIdTwo:
                        //
                        //
                        //
                        form = new form2Class();
                        body = form.getForm(cp, dstFormId, rqs, rightNow, application);
                        break;
                    default:
                        //
                        // default form
                        //
                        dstFormId = commonModule.formIdOne;
                        form = new form1Class();
                        body = form.getForm(cp, dstFormId, rqs, rightNow, application);
                        break;
                }

                commonModule.saveApplication(cp, application, rightNow);

                // assemble body

                returnHtml = body;

            }
            catch (Exception ex)
            {
                cp.Site.ErrorReport(ex, "error in formHandlerClass.execute");
            }
            return returnHtml;
        }

    }
}
