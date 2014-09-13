
Option Strict On

Imports Contensive.BaseClasses

Namespace Contensive.addons.multiFormAjaxSample
    '
    Public Class form1Class
        Inherits formBaseClass
        '
        '
        '
        Friend Overrides Function processForm(ByVal cp As CPBaseClass, ByVal srcFormId As Integer, ByVal rqs As String, ByVal rightnow As Date, ByRef application As applicationClass) As Integer
            Dim nextFormId As Integer = srcFormId
            Try
                Dim button As String = cp.Doc.GetProperty(rnButton)
                Dim cs As CPCSBaseClass = cp.CSNew
                Dim firstName As String
                Dim isInputOK As Boolean = True
                '
                ' if the application record has not been created yet, create  it now
                '
                If applicationId = 0 Then
                    applicationId = getApplicationId(cp, True)
                End If
                '
                ' check the input requirements
                ' if user errors are handled with javascript, no need to display a message, just prevent save
                '
                firstName = cp.Doc.GetText("firstName")
                If firstName = "" Then
                    isInputOK = False
                End If
                '
                ' if no user errors, process input
                ' if errors, just return default nextFormId which will redisplay this form
                '
                If isInputOK Then
                    If cs.Open(cnMultiFormAjaxApplications, "id=" & applicationId) Then
                        Call cs.SetField("firstName", firstName)
                    End If
                    Call cs.Close()
                    '
                    ' determine the next form
                    '
                    Select Case button
                        Case buttonNext
                            nextFormId = formIdTwo
                    End Select
                End If
            Catch ex As Exception
                errorReport(ex, cp, "processForm")
            End Try
            Return nextFormId
        End Function
        '
        '
        '
        Friend Overrides Function getForm(ByVal cp As CPBaseClass, ByVal dstFormId As Integer, ByVal rqs As String, ByVal rightNow As Date, ByRef application As applicationClass) As String
            Dim returnHtml As String = ""
            Try
                Dim layout As CPBlockBaseClass = cp.BlockNew
                Dim cs As CPCSBaseClass = cp.CSNew
                Dim body As String
                Dim firstName As String = ""
                '
                Call layout.OpenLayout("MultiFormAjaxSample - Form 1")
                '
                ' manuiplate the html, pre-populating fields, hiding parts not needed, etc.
                ' get the resulting form from the layout object
                ' add the srcFormId as a hidden
                '
                If application.id = 0 Then
                    '
                    ' the first form must be handled a little differently because it will be displayed before anyone submits a form. 
                    ' So the user may not continue and should not get an application record.
                    ' To display the form, check if the application has been created. If yes, prepopulate the form from the application. 
                    ' If not, populate the form from the same source used to populate the application.
                    '
                    If cs.Open("people", "id=" & cp.User.Id) Then
                        firstName = cs.GetText("firstName")
                    End If
                    Call cs.Close()
                Else
                    '
                    ' populate the form from the application
                    '
                    If cs.Open("MultiFormAjax Application", "(id=" & ApplicationId & ")") Then
                        firstName = cs.GetText("firstName")
                    End If
                    Call cs.Close()
                End If
                Call layout.SetInner("#mfaFirstNameWrapper", cp.Html.InputText("firstName", firstName))
                '
                ' wrap it in a form for the javascript to use during submit
                '
                body = layout.GetHtml()
                body &= cp.Html.Hidden(rnSrcFormId, dstFormId.ToString)
                returnHtml = cp.Html.Form(body, , , "mfaForm1")
            Catch ex As Exception
                errorReport(ex, cp, "getForm")
            End Try
            Return returnHtml
        End Function
        '
        '
        '
        Private Sub errorReport(ByVal ex As Exception, ByVal cp As CPBaseClass, ByVal method As String)
            cp.Site.ErrorReport(ex, "error in aoManagerTemplate.multiFormAjaxSample." & method)
        End Sub
    End Class
End Namespace
