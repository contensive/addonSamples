using System;
using System.Collections.Generic;
using System.Text;
using Contensive.BaseClasses;

namespace addonTemplateCs
{
    //
    // 1) Change the namespace to the collection name
    // 2) Change this class name to the addon name
    // 3) Create a Contensive Addon record with the namespace apCollectionName.ad
    // 3) add reference to CPBase.DLL, typically installed in c:\program files\kma\contensive\
    //
    public class addonClass : Contensive.BaseClasses.AddonBaseClass
    {
        //
        // execute method is the only public
        //
        public override object Execute(Contensive.BaseClasses.CPBaseClass cp)
        {
            string returnHtml = "";
            try
            {
                returnHtml = "Visual Studio add-on assembly - OK response";
            }
            catch (Exception ex)
            {
                cp.Site.ErrorReport( "There was a unexpected exception, " + ex.ToString() );
                returnHtml = "Visual Studio add-on assembly - error response";
            }
            return returnHtml;
        }
    }
}
