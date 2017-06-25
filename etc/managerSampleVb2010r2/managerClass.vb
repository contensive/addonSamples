
Imports Contensive.BaseClasses

Namespace Contensive.addons.managerSample
    '
    Public Class managerClass
        Inherits AddonBaseClass
        '
        Public Overrides Function Execute(ByVal CP As CPBaseClass) As Object
            Dim returnHtml As String = ""
            '
            Try
                Dim body As String = ""
                Dim managerSampleA As New managerSampleAClass
                Dim managerSampleB As New managerSampleBClass
                Dim rqs As String = CP.Doc.RefreshQueryString
                Call CP.Doc.AddHeadJavascript("var msBaseRqs='" & rqs & "';")
                Dim adminUrl As String = CP.Site.GetProperty("adminUrl")
                Dim accountManagerUrl As String = adminUrl & "?addonGuid={B2290F18-3477-449C-BBCF-D0FD44E2B677}"
                '
                Dim manager As New adminFramework.pageWithNavClass
                Dim rightNow As Date = getRightNow(CP)
                Dim srcFormId As Integer = CP.Utils.EncodeInteger(CP.Doc.GetProperty(rnSrcFormId))
                Dim dstFormId As Integer = CP.Utils.EncodeInteger(CP.Doc.GetProperty(rnDstFormId))
                '
                ' process form
                '
                If (srcFormId <> 0) Then
                    Select Case srcFormId
                        Case formIdSampleAMin To formIdSampleAMax
                            '
                            '
                            '
                            dstFormId = managerSampleA.processForm(CP, srcFormId, rqs, rightNow)
                        Case formIdSampleBMin To formIdSampleBMax
                            '
                            '
                            '
                            dstFormId = managerSampleB.processForm(CP, srcFormId, rqs)
                    End Select
                End If
                '
                ' get form
                '
                manager.navCaption = "Users"
                manager.navLink = "?" & CP.Utils.ModifyQueryString(rqs, rnDstFormId, formIdSampleAList)
                '
                manager.addNav()
                manager.navCaption = "Organizations"
                manager.navLink = "?" & CP.Utils.ModifyQueryString(rqs, rnDstFormId, formIdSampleBList)
                '
                Select Case dstFormId
                    Case formIdSampleBMin To formIdSampleBMax
                        '
                        '
                        '
                        manager.setActiveNav("Organizations")
                        body = managerSampleB.getForm(CP, dstFormId, rqs, rightNow)
                        body = CP.Html.div(body, , , "managerSampleBFrame")
                        manager.body = body
                    Case Else
                        '
                        ' default is account list
                        '
                        manager.setActiveNav("people")
                        body = managerSampleA.getForm(CP, dstFormId, rqs, rightNow)
                        body = CP.Html.div(body, , , "managerSampleAFrame")
                        manager.body = body
                End Select
                '
                'Assemble
                '
                manager.title = "Manager Sample"
                CP.Doc.AddHeadStyle(manager.styleSheet)
                returnHtml = manager.getHtml(CP)
            Catch ex As Exception
                CP.Site.ErrorReport(ex, "error in aoManagerTemplate.adminClass.execute")
            End Try
            Return returnHtml
        End Function

    End Class
End Namespace
