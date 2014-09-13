
Option Strict On

Imports Contensive.BaseClasses

Namespace Contensive.addons.multiFormAjaxSample

    Public Module commonModule
        '
        Public Const cr As String = vbCrLf & vbTab
        Public Const cr2 As String = cr & vbTab
        Public Const cr3 As String = cr2 & vbTab
        '
        Friend Const cnMultiFormAjaxApplications As String = "MultiFormAjax Application"
        '
        Public Const buttonOK As String = "OK"
        Public Const buttonSave As String = "Save"
        Public Const buttonCancel As String = "Cancel"
        Public Const buttonNext As String = "Next"
        Public Const buttonPrevious As String = "Previous"
        Public Const buttonContinue As String = "Continue"
        Public Const buttonRestart As String = "Restart"
        Public Const buttonFinish As String = "Finish"
        '
        ' request names 
        '
        Public Const rnUserId As String = "userId"
        Public Const rnSrcFormId As String = "srcFormId"
        Public Const rnDstFormId As String = "dstFormId"
        Public Const rnButton As String = "button"
        '
        ' Forms
        '
        Public Const formIdOne As Integer = 1
        Public Const formIdTwo As Integer = 2
        Public Const formIdThree As Integer = 3
        Public Const formIdFour As Integer = 4
        '
        ' A few common usefull routines 
        '
        Friend Function toJSON(ByVal value As String) As String
            Dim s As String = value
            Try
                '
                s = s.Replace("""", "\""")
                s = s.Replace(vbCrLf, "\n")
                s = s.Replace(vbCr, "\n")
                s = s.Replace(vbLf, "\n")
                '
            Catch ex As Exception
                s = value
            End Try
            Return s
        End Function
        '
        Friend Function buffDate(ByVal sourceDate As Date) As String
            Dim returnValue As String
            '
            If sourceDate < #1/1/1900# Then
                returnValue = ""
            Else
                returnValue = sourceDate.ToShortDateString
            End If
            Return returnValue

        End Function
        '
        '
        '
        Friend Function getRightNow(ByVal cp As Contensive.BaseClasses.CPBaseClass) As Date
            Dim returnValue As Date = Date.Now()
            Try
                Dim testString As String = cp.Site.GetProperty("Sample Manager Test Mode Date", "")
                '
                ' change 'sample' to the name of this collection
                '
                If testString <> "" Then
                    returnValue = encodeMinDate(cp.Utils.EncodeDate(testString))
                    If returnValue = Date.MinValue Then
                        returnValue = Date.Now()
                    End If
                End If
            Catch ex As Exception
                Call cp.Site.ErrorReport(ex, "Error in getRightNow")
            End Try
            Return returnValue
        End Function
        '
        '
        '
        Friend Function encodeMinDate(ByVal sourceDate As Date) As Date
            Dim returnValue As Date = sourceDate
            If returnValue < #1/1/1900# Then
                returnValue = Date.MinValue
            End If
            Return returnValue
        End Function
        '
        '
        '
        '
        Friend Sub appendLog(ByVal cp As CPBaseClass, ByVal logMessage As String)
            Dim nowDate As Date = Date.Now.Date()
            Dim logFilename As String = nowDate.Year & nowDate.Month.ToString("D2") & nowDate.Day.ToString("D2") & ".log"
            Call cp.File.CreateFolder(cp.Site.PhysicalInstallPath & "\logs\multiformAjaxSample")
            Call cp.Utils.AppendLog("multiformAjaxSample\" & logFilename, logMessage)
        End Sub
        '
        '
        '
        Friend Function getApplicationId(ByVal cp As CPBaseClass, ByVal addIfMissing As Boolean) As Integer
            Dim applicationId As Integer = 0
            Try
                Dim cs As CPCSBaseClass = cp.CSNew()
                Dim csSrc As CPCSBaseClass = cp.CSNew()
                If cs.Open("MultiFormAjax Application", "(visitid=" & cp.Visit.Id & ")and(dateCompleted is null)") Then
                    applicationId = cs.GetInteger("id")
                End If
                Call cs.Close()
                If (applicationId = 0) And addIfMissing Then
                    If cs.Insert("MultiFormAjax Application") Then
                        applicationId = cs.GetInteger("id")
                        Call cs.SetField("visitId", cp.Visit.Id.ToString)
                        If csSrc.Open("people", "id=" & cp.User.Id) Then
                            Call cs.SetField("firstName", csSrc.GetText("firstName"))
                            Call cs.SetField("lastName", csSrc.GetText("lastName"))
                            Call cs.SetField("email", csSrc.GetText("email"))
                        End If
                        Call csSrc.Close()
                    End If
                    Call cs.Close()
                End If

            Catch ex As Exception
                Call cp.Site.ErrorReport(ex, "Error in getApplicationId")
            End Try
            Return applicationId
        End Function
    End Module
End Namespace
