Imports AutoInsertPopup

Public Class AutoInsertItem
    Implements IAutoInsertItem

    Public Sub New()
        Me.New("", "", "")
    End Sub

    Public Sub New(content As Object)
        Me.New(content, content.ToString, content.ToString)
    End Sub

    Public Sub New(content As Object, stringContent As String)
        Me.New(content, stringContent, stringContent)
    End Sub

    Public Sub New(content As Object, stringContent As String, insertString As String)
        Me.ViewContent = content : SearchStringContent = stringContent : TextBoxInsertString = insertString
    End Sub

    Public Property ViewContent As Object Implements IAutoInsertItem.ViewContent

    Public Property SearchStringContent As String Implements IAutoInsertItem.SearchStringContent

    Public Property SortingIndex As Integer = 0 Implements IAutoInsertItem.SortingIndex

    Public Property TextBoxInsertString As String Implements IAutoInsertItem.TextBoxInsertString

End Class
