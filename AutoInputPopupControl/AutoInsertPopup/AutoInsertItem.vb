Imports AutoInsertPopup

Public Class AutoInsertItem
    Implements IAutoInsertItem

    Public Sub New()
        Me.New("", "", "")
    End Sub

    Public Sub New(content As Object)
        Me.New(content, content.ToString, content.ToString)
    End Sub

    Public Sub New(item As IAutoInsertItem)
        Me.New(item.ViewContent, item.SearchStringContent, item.TextBoxInsertString)
        Me.SortingIndex = item.SortingIndex
    End Sub
    Public Sub New(contentString As String, searchString As String)
        Me.New(contentString, searchString, contentString)
    End Sub
    Public Sub New(content As Object, searchString As String)
        Me.New(content, searchString, searchString)
    End Sub

    Public Sub New(content As Object, searchString As String, insertString As String)
        Me.ViewContent = content : SearchStringContent = searchString : TextBoxInsertString = insertString
    End Sub

    Public Property ViewContent As Object Implements IAutoInsertItem.ViewContent

    Public Property SearchStringContent As String Implements IAutoInsertItem.SearchStringContent

    Public Property SortingIndex As Integer = 0 Implements IAutoInsertItem.SortingIndex

    Public Property TextBoxInsertString As String Implements IAutoInsertItem.TextBoxInsertString

End Class
