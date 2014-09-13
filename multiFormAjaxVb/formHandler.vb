
Imports Contensive.BaseClasses

Namespace Contensive.addons.multiFormAjaxSample
    '
    Public Class formHandlerClass
        Inherits AddonBaseClass
        '
        ' Ajax Handler - Remote Method
        '   returns content of inner classes to the contains originally created around them
        '
        Public Overrides Function Execute(ByVal CP As CPBaseClass) As Object
            Dim returnHtml As String = ""
            Try
                '
                ' all form classes inherit from formBase, so this one form object can be used for every form type required
                ' refresh query string is everying needed on the query string to refresh this page. Used for links, etc to always submit to the same page.
                ' rightNow is convenient when you need to test time dependant options, you can force the rightNow with a site property, etc
                ' srcFormId is the form number that submitted the request (if it was a form submission) and should be a hidden on all forms
                ' dstFormId is a way to force a form to display without a form submission
                '
                Dim body As String = ""
                Dim form As formBaseClass
                Dim rqs As String = CP.Doc.GetProperty("baseRqs")
                Dim rightNow As Date = getRightNow(CP)
                Dim srcFormId As Integer = CP.Utils.EncodeInteger(CP.Doc.GetProperty(rnSrcFormId))
                Dim dstFormId As Integer = CP.Utils.EncodeInteger(CP.Doc.GetProperty(rnDstFormId))
                Dim formHandler As formHandlerClass = New formHandlerClass
                Dim application As applicationClass
                '
                ' get previously started application
                '
                application = getApplication(CP, False)
                '
                ' if there is no application, only allow form one
                '
                If application.id = 0 Then
                    If srcFormId <> formIdOne Then
                        srcFormId = 0
                    End If
                    dstFormId = formIdOne
                End If
                '
                ' process forms
                '
                If (srcFormId <> 0) Then
                    Select Case srcFormId
                        Case formIdOne
                            '
                            form = New form1Class
                            dstFormId = form.processForm(CP, srcFormId, rqs, rightNow, applicationId)
                        Case formIdTwo
                            '
                            form = New form2Class
                            dstFormId = form.processForm(CP, srcFormId, rqs, rightNow, applicationId)
                        Case formIdThree
                            '
                            form = New form3Class
                            dstFormId = form.processForm(CP, srcFormId, rqs, rightNow, applicationId)
                        Case formIdFour
                            '
                            form = New form4Class
                            dstFormId = form.processForm(CP, srcFormId, rqs, rightNow, applicationId)
                    End Select
                End If
                '
                ' get the next form that should appear on the page. 
                ' put the default form as the else case - to display if nothing else is selected
                '
                Select Case dstFormId
                    Case formIdFour
                        '
                        '
                        '
                        form = New form4Class
                        body = form.getForm(CP, dstFormId, rqs, rightNow, applicationId)
                    Case formIdThree
                        '
                        '
                        '
                        form = New form3Class
                        body = form.getForm(CP, dstFormId, rqs, rightNow, applicationId)
                    Case formIdTwo
                        '
                        '
                        '
                        form = New form2Class
                        body = form.getForm(CP, dstFormId, rqs, rightNow, applicationId)
                    Case Else
                        '
                        ' default is account list
                        '
                        dstFormId = formIdOne
                        form = New form1Class
                        body = form.getForm(CP, dstFormId, rqs, rightNow, applicationId)
                End Select
                '
                ' assemble body
                '
                returnHtml = body
            Catch ex As Exception
                CP.Site.ErrorReport(ex, "error in formHandlerClass.execute")
            End Try
            Return returnHtml
        End Function
    End Class
End Namespace
