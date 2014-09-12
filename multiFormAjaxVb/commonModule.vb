
Option Strict On

Imports Contensive.BaseClasses

Namespace Contensive.addons.multiFormAjaxSample

    Public Module commonModule
        '
        Public Const cr As String = vbCrLf & vbTab
        Public Const cr2 As String = cr & vbTab
        Public Const cr3 As String = cr2 & vbTab
        '
        Public Const buttonOK As String = " OK "
        Public Const buttonSave As String = " Save "
        Public Const buttonCancel As String = " Cancel "
        Public Const buttonNext As String = " Next "
        Public Const buttonPrevious As String = " Previous "
        Public Const buttonContinue As String = " Continue "
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
    End Module
End Namespace
