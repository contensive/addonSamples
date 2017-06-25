
using System;
using System.Collections.Generic;
using System.Text;
using Contensive.BaseClasses;

namespace Contensive.Addons.ManagerSampleCs
{
    class blankClass
    {
        const string sampleLayout = ""
            + "<form id=\"myForm\" action=\"\">"
            + "\n\t<h2 class=\"myHeader\">This is the sample form</h2>"
            + "\n\t<div class=\"myInputRow\"><label for=\"myFormName\" class=\"myLabel\">Name</label><input class=\"myFormName\" name=\"myFormName\" value=\"\"></div>"
            + "\n\t<div class=\"myButtonRow\"><input type=\"submit\" name=\"button\" value=\"Submit Me\"></div>"
            + "\n</form>";
        //
        // ===============================================================================
        // process Form
        // ===============================================================================
        //
        public int processForm(CPBaseClass cp, int srcFormId, string rqs, DateTime rightNow, ref int appId)
        {
            int nextFormId = srcFormId;
            try
            {
                string button = cp.Doc.GetProperty(constants.rnButton, "");
                CPCSBaseClass cs = cp.CSNew();
                //
                if (button != "")
                {
                    //
                    // server-side validation
                    // add client-side validation in the javascript tab of the addon. (but still always include server-side)
                    //
                    constants.checkRequiredFieldText( cp, "name", "Name");
                    //
                    if (cp.UserError.OK())
                    {
                        cs.Open("people", "id=" + cp.User.Id.ToString(), "", true, "", 1, 1);
                        cs.SetField("name", cp.Doc.GetText("name", ""));
                        cs.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorReport(cp, ex, "processForm");
            }
            return nextFormId;
        }
        //
        // ===============================================================================
        // get Form
        // ===============================================================================
        //
        public string getForm(CPBaseClass cp, int dstFormId, string rqs, DateTime rightNow, ref int appId)
        {
            string returnHtml = "";
            try
            {
                string form = "";
                CPBlockBaseClass layout = cp.BlockNew();
                string returnJs = "";
                CPCSBaseClass cs = cp.CSNew();
                //
                // open layout, grab form, add hiddens, replace back into layout
                //
                layout.OpenLayout("sample layout");
                if (layout.GetHtml() == "")
                {
                    layout.Load(sampleLayout);
                    cs.Insert("Layouts");
                    cs.SetField("name", "sample layout");
                    cs.SetField("layout", layout.GetHtml());
                    cs.Close();
                }
                //
                form = layout.GetInner("#myForm");
                form += cp.Html.Hidden(constants.rnSrcFormId, dstFormId.ToString(), "", "");
                form += cp.Html.Hidden(constants.rnAppId, appId.ToString(), "", "");
                if (!cp.UserError.OK())
                {
                    form = cp.Html.div(cp.UserError.GetList(), "", "", "") + form;
                }
                form = cp.Html.Form(form, "", "", "", "", "");
                layout.SetOuter("#myForm", form);
                //
                // Populate the layout
                // attempt to open the application record. It is created in the process so this may fail.
                //      if not cs.OK(), the getFormField will return blank.
                //
                cs.Open("people", "id=" + cp.User.Id.ToString(), "", true, "", 1, 1);
                if (true)
                {
                    //
                    // either server-side
                    //
                    layout.SetOuter(".myInputRow input", cp.Html.InputText("name", constants.getFormField(cp, cs, "name"), "", "", false, "", ""));
                }
                else
                {
                    //
                    // or client-side
                    //
                    returnJs += constants.cr + "jQuery('.myInputRow myLabel').html('" + constants.getFormField(cp, cs, "name") + "')";
                }
                cs.Close();
                //
                // apply any javascript to doc
                //
                if (returnJs != "")
                {
                    cp.Doc.AddHeadJavascript("jQuery(document).ready(function(){" + returnJs + constants.cr + "});");
                }
                //
                // return converted layout
                //
                returnHtml = layout.GetHtml();
            }
            catch (Exception ex)
            {
                errorReport(cp, ex, "getForm");
            }
            return returnHtml;
        }
        //
        // ===============================================================================
        // handle errors for this class
        // ===============================================================================
        //
        private void errorReport(CPBaseClass cp, Exception ex, string method)
        {
            cp.Site.ErrorReport(ex, "error in addonTemplateCs2005.blankClass.getForm");
        }
    }
}
