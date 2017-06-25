

using System;
using System.Collections.Generic;
using System.Text;
using Contensive.BaseClasses;

namespace Contensive.Addon.AddonCollectionCs.Views
{
    //
    // this sample creates an addon collection (a group off addons that install together)
    // -- Change the namespace to the collection name
    // 2) Change this class name to the addon name
    // 3) Create a Contensive Addon record with the namespace apCollectionName.ad
    // 3) add reference to CPBase.DLL, typically installed in c:\program files\kma\contensive\
    //
    public class addonClass : Contensive.BaseClasses.AddonBaseClass
    {
        //
        // -- Contensive calls the execute method of your addon class
        public override object Execute(Contensive.BaseClasses.CPBaseClass cp)
        {
            string result = "";
            try
            {
                //
                // your code here
                //
                result = "success response";
            }
            catch (Exception ex)
            {
                cp.Site.ErrorReport(ex);
                result = "error response";
            }
            return result;
        }
    }
}
