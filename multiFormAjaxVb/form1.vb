
Option Strict On

Imports Contensive.BaseClasses

Namespace Contensive.addons.multiFormAjaxSample
    '
    Public Class form1Class
        Inherits formBaseClass
        '
        '
        '
        Friend Overrides Function processForm(ByVal cp As CPBaseClass, ByVal srcFormId As Integer, ByVal rqs As String, ByVal rightnow As Date) As Integer
            Dim nextFormId As Integer = srcFormId
            Try
                Dim button As String = cp.Doc.GetProperty(rnButton)
                '
                If button = buttonSave Or button = buttonOK Then
                    '
                    ' process the form
                    '
                End If
                '
                ' determine the next form
                '
                Select Case button
                    Case buttonCancel, buttonOK, buttonSave
                        nextFormId = formIdOne
                    Case buttonNext
                        nextFormId = formIdTwo
                    Case buttonPrevious
                        nextFormId = formIdOne
                End Select
            Catch ex As Exception
                errorReport(ex, cp, "processForm")
            End Try
            Return nextFormId
        End Function
        '
        '
        '
        Friend Overrides Function getForm(ByVal cp As CPBaseClass, ByVal dstFormId As Integer, ByVal rqs As String, ByVal rightNow As Date) As String
            Dim returnHtml As String = ""
            Try
                Dim layout As CPBlockBaseClass = cp.BlockNew
                Dim cs As CPCSBaseClass = cp.CSNew
                Dim body As String
                '
                Call layout.OpenLayout("MultiFormAjaxSample - Form 1")
                '
                ' manuiplate the html, pre-populating fields, hiding parts not needed, etc.
                ' get the resulting form from the layout object
                ' add the srcFormId as a hidden
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