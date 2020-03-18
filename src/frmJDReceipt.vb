Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Text


Public Class frmJDReceipt

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
        wb.AllowNavigation = True
        wb.AllowWebBrowserDrop = True
        AddHandler wb.Navigated, AddressOf wbNavigatedEventHandler

        Dim axWb As SHDocVw.WebBrowser = CType(wb.ActiveXInstance, SHDocVw.WebBrowser)
        AddHandler axWb.NewWindow2, AddressOf axWbNewWindow2EventHandler
        AddHandler wb2.DocumentCompleted, AddressOf wbDocumentCompletedEventHandler
    End Sub

    Private Sub axWbNewWindow2EventHandler(ByRef ppDisp As Object, ByRef Cancel As Boolean)
        '打开新窗口的操作定向到指定的 WebBrowser
        ppDisp = wb2.ActiveXInstance
    End Sub

    Private Sub wbNavigatedEventHandler(sender As Object, e As WebBrowserNavigatedEventArgs)
        txtUrl.Text = e.Url.ToString
    End Sub

    Private _currElemet As HtmlElement

    Private Sub wbDocumentCompletedEventHandler(sender As Object, e As WebBrowserDocumentCompletedEventArgs)
        Dim browser As WebBrowser = CType(sender, WebBrowser)

        'Const WDFP_URL As String = "https://myivc.jd.com/fpzz/index.action"
        Const FPXQ_URL As String = "https://myivc.jd.com/fpzz/ivcLand.action?orderId="
        'Const CKFP_URL As String = "https://storage.jd.com/eicore-fm.jd.com/"
        Const HKFP_URL As String = "https://myivc.jd.com/fpzz/hkfpReq.action"

        Dim url As String = e.Url.ToString
        Dim doc As HtmlDocument = browser.Document
        If url.StartsWith(FPXQ_URL) Then
            '下载发票
            _currElemet = getCKFPElement(doc)
            If _currElemet IsNot Nothing Then
                url = _currElemet.GetAttribute("href")
                Call saveFile(url, doc)
            Else
                Debug.Print("Skip for FPXQ url is empty")
            End If

            If _fpxq IsNot Nothing Then
                If _fpxq.Count > 0 Then
                    '继续
                    'If MessageBox.Show("continue to next?", Me.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                    url = _fpxq.Dequeue
                    _currElemet = findElement(wb.Document, url, "发票详情")
                    If _currElemet IsNot Nothing Then
                        _currElemet.InvokeMember("click")
                    Else
                        Debug.Print("findElement NOT FOUND " & url)
                    End If
                    'End If
                Else
                    wb.BringToFront()
                    MessageBox.Show("all done")
                    _fpxq = Nothing
                    'ToolStrip1.Enabled = True
                End If
            End If
        ElseIf url.StartsWith(HKFP_URL) Then
            '换开发票：填写表单并自动提交
            'ivcTitleType.value="5"
            browser.BringToFront()
            btnDetailPage.Checked = True
            btnMainPage.Checked = False

            _currElemet = findElement(doc, "ivcTitleType")
            _currElemet.SetAttribute("value", "5")
            'company.value=""
            _currElemet = findElement(doc, "company")
            _currElemet.SetAttribute("value", My.Settings.ReceiptTitle)
            'taxNo.value=""
            _currElemet = findElement(doc, "taxNo")
            _currElemet.SetAttribute("value", My.Settings.TaxCode)

            _currElemet = findElement(doc, "javascript:void(0)", "提交")
            If _currElemet IsNot Nothing Then
                If MessageBox.Show("Sure to submit the request of changing receipt?"，
                                    Me.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
                    _currElemet.InvokeMember("click")
                End If
            End If
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        wb.Navigate(txtUrl.Text)
    End Sub

    ''' <summary>
    ''' 发票详情 Url 队列
    ''' </summary>
    Private _fpxq As Queue(Of String)

    Private Sub btnParse_Click(sender As Object, e As EventArgs) Handles btnParse.Click
        _fpxq = getFPXQUrls(wb.Document)
        If _fpxq.Count > 0 Then
            'If MessageBox.Show("start?", Me.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) = DialogResult.OK Then
            'ToolStrip1.Enabled = False
            _currElemet = findElement(wb.Document, _fpxq.Dequeue, "发票详情")
            _currElemet.InvokeMember("click")

            wb2.BringToFront()
            'End If
        Else
            MessageBox.Show("FPXQ not found")
        End If
    End Sub

    Private Sub btnMainPage_Click(sender As Object, e As EventArgs) Handles btnMainPage.Click
        wb.BringToFront()
        btnDetailPage.Checked = False
    End Sub

    Private Sub btnDetailPage_Click(sender As Object, e As EventArgs) Handles btnDetailPage.Click
        wb2.BringToFront()
        btnMainPage.Checked = False
    End Sub

    ''' <summary>
    ''' 下载保存文件
    ''' </summary>
    ''' <param name="url"></param>
    Private Shared Sub saveFile(ByVal url As String, doc As HtmlDocument)
        Dim strCookies As String = doc.Cookie
        'If String.IsNullOrEmpty(strCookies) Then
        '    strCookies = CookieHelper.GetCookieString(url)
        'End If
        Try
            Using wc As New Net.WebClient
                wc.Headers.Add("Cookie", strCookies) '共享 WebBrowser.Cookie
                wc.DownloadFile(url, getFilename(url))
            End Using
        Catch ex As Exception
            Stop
        End Try
    End Sub

    ''' <summary>
    ''' 通过 Url 截取文件名
    ''' </summary>
    ''' <param name="url"></param>
    ''' <param name="ext">扩展名， 默认为 .pdf</param>
    ''' <returns></returns>
    ''' <example> 
    ''' url = https://storage.jd.com/eicore-fm.jd.com/012001900311-54851690.pdf?Expires=2529543048&AccessKey=bfac05320eaf11cc80cf1823e4fb87d98523fc94&Signature=ezQY7fu9DNVPArELUFpXbOqjOu8%3D
    ''' </example>
    Private Shared Function getFilename(ByVal url As String, ByVal Optional ext As String = ".pdf") As String
        Dim ret As String = String.Empty
        Dim p As Int32 = url.LastIndexOf("/")
        If p > "https://".Length Then
            Dim t As Int32 = url.IndexOf(ext, p)
            If t > 0 Then
                ret = url.Substring(p + 1, t - p - 1) & ext
            End If
        End If
        Return ret
    End Function

    'Private Class CookieHelper
    '    <DllImport("wininet.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    '    Shared Function InternetGetCookieEx(ByVal pchURL As String, ByVal pchCookieName As String, ByVal pchCookieData As StringBuilder, ByRef pcchCookieData As Integer, ByVal dwFlags As Integer, ByVal lpReserved As Object) As Boolean
    '    End Function

    '    Public Shared Function GetCookieString(ByVal url As String) As String
    '        ' Determine the size of the cookie
    '        Dim datasize As Integer = 256
    '        Dim cookieData As New StringBuilder(datasize)

    '        If Not InternetGetCookieEx(url, Nothing, cookieData, datasize, &H2000, Nothing) Then
    '            If datasize < 0 Then
    '                Return Nothing
    '            End If

    '            ' Allocate stringbuilder large enough to hold the cookie
    '            cookieData = New StringBuilder(datasize)
    '            If Not InternetGetCookieEx(url, Nothing, cookieData, datasize, &H2000, Nothing) Then
    '                Return Nothing
    '            End If
    '        End If
    '        Return cookieData.ToString()
    '    End Function
    'End Class

    Private Shared Function findElement(ByVal doc As HtmlDocument, ByVal elementId As String) As HtmlElement
        Dim ret As HtmlElement = Nothing
        Try
            ret = doc.GetElementById(elementId)
        Catch ex As Exception
            Debug.Print("findElement err:{0}", ex.Message)
        End Try

        Return ret
    End Function

    Private Shared Function findElement(doc As HtmlDocument, ByVal href As String, ByVal innerText As String) As HtmlElement
        Dim ret As HtmlElement = Nothing
        For Each element As HtmlElement In doc.Links
            If element.InnerText = innerText AndAlso element.GetAttribute("href") = href Then
                ret = element
                Exit For
            End If
        Next
        Return ret
    End Function

    Private Shared Function getFPXQUrls(ByVal doc As HtmlDocument) As Queue(Of String)
        Dim ret As New Queue(Of String)

        For Each element As HtmlElement In doc.Links
            If element.InnerText = "发票详情" Then
                ret.Enqueue(element.GetAttribute("href"))
            End If
        Next

        Return ret
    End Function

    Private Shared Function getCKFPElement(ByVal doc As HtmlDocument) As HtmlElement
        Dim ret As HtmlElement = Nothing

        For Each element As HtmlElement In doc.Links
            If element.InnerText = "查看发票" Then
                ret = element '.Parent
                Exit For
            End If
        Next

        Return ret
    End Function

    Private Sub btnChangeTitle_Click(sender As Object, e As EventArgs) Handles btnChangeTitle.Click
        'Todo: 自动填写换开表单并提交
        btnChangeTitle.Checked = Not btnChangeTitle.Checked
        If btnChangeTitle.Checked Then
            '填写换开抬头后税号
            Dim sInput As String = Nothing
            sInput = InputBox("The receipt title change to:", Me.Text, My.Settings.ReceiptTitle)
            If Not String.IsNullOrEmpty(sInput) Then
                My.Settings.ReceiptTitle = sInput

                sInput = InputBox("The tax code change to:", Me.Text, My.Settings.TaxCode)
                If Not String.IsNullOrEmpty(sInput) Then
                    My.Settings.TaxCode = sInput
                    My.Settings.Save()
                    MessageBox.Show("Ready to change receipt title to " & My.Settings.ReceiptTitle，
                                    Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("The tax code can't be empty"，
                                    Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                MessageBox.Show("The receipt title can't be empty"，
                                Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub
End Class
