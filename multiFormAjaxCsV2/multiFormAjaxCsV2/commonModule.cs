using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contensive.BaseClasses;

namespace Contensive.addons.multiFormAjaxSampleV2

{
    public class applicationClass
    {
        public int id;
        public string firstName;
        public string lastName;
        public string email;
        public Boolean completed;
        public Boolean changed;

    }

    public abstract class formBaseClass
    {
        abstract public int processForm(CPBaseClass cp, int srcFormId, string rqs, DateTime rightNow, applicationClass application);

        abstract public string getForm(CPBaseClass cp, int dstFormId, string rqs, DateTime rightNow, applicationClass application);
    }


    public static class commonModule
    {
        
        public const string cnMultiFormAjaxApplications = "MultiFormAjax Application";

        public const string buttonOK = "OK";
        public const string buttonSave = "Save";
        public const string buttonCancel = "Cancel";
        public const string buttonNext = "Next";
        public const string buttonPrevious = "Previous";
        public const string buttonContinue = "Continue";
        public const string buttonRestart = "Restart";
        public const string buttonFinish = "Finish";


        // request names 
        //
        public const string rnUserId  = "userId";
        public const string rnSrcFormId  = "srcFormId";
        public const string rnDstFormId  = "dstFormId";
        public const string rnButton  = "button";
                
        // Forms
        //
        public const int formIdOne = 1;
        public const int formIdTwo = 2;
        public const int formIdThree = 3;
        public const int formIdFour = 4;      
        


        //
        //
        //
        public static DateTime getRightNow(CPBaseClass cp)
        {
            DateTime returnValue = DateTime.Now;
            try
            {
                string testString = cp.Site.GetProperty("Sample Manager Test Mode Date", "");

                if (testString != "")
                    {
                        returnValue = encodeMinDate(cp.Utils.EncodeDate(testString));

                        if (returnValue == DateTime.MinValue)
                        {
                            returnValue = DateTime.Now;
                        }
                    }

            }
            catch (Exception ex)
            {
                cp.Site.ErrorReport(ex, "Error in getRightNow");
            }

            return returnValue;
        }

        //
        //
        //
        static DateTime encodeMinDate(DateTime sourceDate)
        {
            DateTime returnValue = sourceDate;
            if (returnValue < DateTime.Parse("01/01/1900"))
                { returnValue = DateTime.MinValue;
                }
            return returnValue;
        }


        //
        //
        //
        public static applicationClass getApplication(CPBaseClass cp, Boolean createRecordIfMissing)
        {   
            applicationClass application = new applicationClass();

            try {
                CPCSBaseClass cs = cp.CSNew();
                CPCSBaseClass csSrc = cp.CSNew();

                // get id of this user's application
                // use visit property if they keep the same application for the visit
                // use visitor property if each time they open thier browser, they get the previous application
                // use user property if they only get to the application when they are associated to the current user (they should be authenticated first)

                application.completed = false;
                application.changed = false;
                application.id = cp.Visit.GetInteger("multiformAjaxSample ApplicationId");

                if (application.id != 0)
                {
                    if (!cs.Open("MultiFormAjax Application", "(dateCompleted is null)"))
                    {
                        application.id = 0;
                    }
                }

                if (cs.OK())
                {
                    application.firstName = cs.GetText("firstName");
                    application.lastName = cs.GetText("lastName");
                    application.email = cs.GetText("email");
                }
                else {
                    if (csSrc.Open("people", "id=" + cp.User.Id))
                    {
                        application.firstName = csSrc.GetText("firstName");
                        application.lastName = csSrc.GetText("lastName");
                        application.email = csSrc.GetText("email");
                    }
                    csSrc.Close();
                }
                cs.Close();

            }
            catch (Exception ex)
            {
                cp.Site.ErrorReport(ex, "Error in getApplication");
            }

            return application;
        }


        //
        //
        //
        public static void saveApplication(CPBaseClass cp, applicationClass application, DateTime rightNow) {
            try {
                CPCSBaseClass cs = cp.CSNew();
            
                if (application.changed ){
                    if (application.id > 0) {
                        cs.Open(cnMultiFormAjaxApplications, "(id=" + application.id + ")"); 
                    }
                    if (!cs.OK()) {
                        if (cs.Insert("MultiFormAjax Application")) {
                            //
                            // create a new record. 
                            // Set application Id incase needed later
                            // Set visit property to save the application Id
                            //
                            application.id = cs.GetInteger("id");
                            cp.Visit.SetProperty("multiformAjaxSample ApplicationId", application.id.ToString());
                            cs.SetField("visitId", cp.Visit.Id.ToString());
                        }
                    }

                    if (cs.OK()) { 
                        cs.SetField("firstName", application.firstName);
                        cs.SetField("lastName", application.lastName);
                        cs.SetField("email", application.email);
                        if (application.completed) {
                            cs.SetField("datecompleted", rightNow.ToString());
                        }
                    }

                    cs.Close();
                }
            }
            catch (Exception ex)
            {
                cp.Site.ErrorReport(ex, "Error in getApplication");
            }
        } // End saveApplication

    } // End Module

}
