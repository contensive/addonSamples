using System;
using System.Collections.Generic;
using System.Text;
using Contensive.BaseClasses;

namespace managerSample
{
    //
    // 1) Change the namespace to the collection name
    // 2) Change this class name to the addon name
    // 3) Create a Contensive Addon record with the namespace aoCollectionName.addonClass
    //
    public class addonClass : Contensive.BaseClasses.AddonBaseClass
    {
        //
        // execute method is the only public
        //
        public override object Execute(Contensive.BaseClasses.CPBaseClass cp)
        {
            string returnHtml = "Hello World";
            try
            {
                // rqs defined once and passed so when we do ajax integration, the rqs will be passed from
                //      from the ajax call so it can be the rqs of the original page, not the /remoteMethod rqs
                //      you get from cp.doc.RefreshQueryString.
                // srcFormId - passed from a submitting form. Otherwise 0.
                // dstFormId - the next for to display. used for links to new pages. Will be over-ridden
                //      by the formProcessing of srcFormId if it is present.
                // appId - Forms typically save data back to the Db. The 'application' is the table
                //      when the data is saved.
                // rightNow - the date and time when the page is hit. Set once and passed as argument to
                //      enable a test-mode where the time can be hard-coded.
                //
                int srcFormId = cp.Utils.EncodeInteger(cp.Doc.GetProperty(statics.rnSrcFormId, ""));
                int dstFormId = cp.Utils.EncodeInteger(cp.Doc.GetProperty(statics.rnDstFormId, ""));
                int appId = cp.Utils.EncodeInteger(cp.Doc.GetProperty(statics.rnAppId, ""));
                string rqs = cp.Doc.RefreshQueryString;
                DateTime rightNow = DateTime.Now;
                CPCSBaseClass cs = cp.CSNew();
                adminFramework.pageWithNavClass page = new adminFramework.pageWithNavClass();
                blankClass blank = new blankClass();
                //
                //------------------------------------------------------------------------
                // add common page elements
                //------------------------------------------------------------------------
                //
                page.title = "Manager Sample Addon";
                page.description = "This is the visual studio c# addon template called Manager Sample. A manager addon is a set of forms that together manage a feature.";
                //
                //------------------------------------------------------------------------
                // process submitted form
                //------------------------------------------------------------------------
                //
                if (srcFormId != 0)
                {
                    switch (srcFormId)
                    {
                        // add a case for each form process needed
                        case statics.formIdBlank:
                            dstFormId = blank.processForm(cp, srcFormId, rqs, rightNow, ref appId);
                            break;
                    }
                }
                //
                //------------------------------------------------------------------------
                // add page navigation
                //------------------------------------------------------------------------
                //
                page.addNav();
                page.navCaption = "Home";
                page.navLink = "?" + cp.Utils.ModifyQueryString(rqs, statics.rnDstFormId, "0", true);
                //
                page.addNav();
                page.navCaption = "Blank Form";
                page.navLink = "?" + cp.Utils.ModifyQueryString(rqs, statics.rnDstFormId, statics.formIdBlank.ToString(), true);
                //
                switch (dstFormId)
                {
                    case (statics.formIdBlank):
                        page.body = blank.getForm(cp, dstFormId, rqs, rightNow, ref appId);
                        break;
                    default:
                        page.body = "<p>Welcome to the default form.</p>";
                        break;
                }
                //
                //------------------------------------------------------------------------
                // output the page
                //------------------------------------------------------------------------
                //
                returnHtml = page.getHtml(cp);
                cp.Doc.AddHeadStyle(page.styleSheet);
            }
            catch (Exception ex)
            {
                errorReport(cp, ex, "execute");
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
            cp.Site.ErrorReport(ex, "error in addonSampleCs2005r4.addonClass.getForm");
        }
    }
}
