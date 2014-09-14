
Option Strict On

Imports Contensive.BaseClasses

Namespace Contensive.addons.multiFormAjaxSample
    '
    Public Class form2Class
        Inherits formBaseClass
        '
        '
        '
        Friend Overrides Function processForm(ByVal cp As CPBaseClass, ByVal srcFormId As Integer, ByVal rqs As String, ByVal rightNow As Date, ByRef application As applicationClass) As Integer
            Dim nextFormId As Integer = srcFormId
            Try
                Dim button As String = cp.Doc.GetProperty(rnButton)
                Dim cs As CPCSBaseClass = cp.CSNew
                Dim lastName As String
                Dim isInputOK As Boolean = True
                '
                ' if the application record has not been created yet, create  it now
                '
                If application.id = 0 Then
                    application = getApplication(cp, True)
                End If
                '
                ' check the input requirements
                ' if user errors are handled with javascript, no need to display a message, just prevent save
                '
                lastName = cp.Doc.GetText("lastName")
                If lastName = "" Then
                    isInputOK = False
                End If
                '
                ' if no user errors, process input
                ' if errors, just return default nextFormId which will redisplay this form
                '
                If isInputOK Then
                    application.lastName = lastName
                    '
                    ' determine the next form
                    '
                    Select Case button
                        Case buttonNext
                            nextFormId = formIdThree
                        Case buttonPrevious
                            nextFormId = formIdOne
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
                Dim lastName As String
                '
                Call layout.OpenLayout("MultiFormAjaxSample - Form 2")
                '
                ' manuiplate the html, pre-populating fields, hiding parts not needed, etc.
                '
                ' get the resulting form from the layout object
                ' add the srcFormId as a hidden
                ' wrap it in a form for the javascript to use during submit
                '
                body = layout.GetHtml()
                body &= cp.Html.Hidden(rnSrcFormId, dstFormId.ToString)
                returnHtml = cp.Html.Form(body, , , "mfaForm2")
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
