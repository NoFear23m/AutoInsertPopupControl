Public Class NormalTests
    Private Sub NormalTests_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TestList = New List(Of AutoInsertPopup.AutoInsertItem) From {
           New AutoInsertPopup.AutoInsertItem("Content 1", "Content 1", "Content 1"),
           New AutoInsertPopup.AutoInsertItem("Content 2", "Content 2", "Content 2"),
           New AutoInsertPopup.AutoInsertItem("Content 3", "Content 3", "Content 3")}

        Dim lines() As String = IO.File.ReadAllLines(My.Application.Info.DirectoryPath & "\Data\deutsch.txt")

        T4List = New List(Of String)
        T4List.AddRange(lines.Where(Function(o) o.Length > 3).OrderBy(Function(x) x.Length))

        Me.DataContext = Me
    End Sub

    Public Property TestList As IEnumerable(Of AutoInsertPopup.IAutoInsertItem)
    Public Property T4List As List(Of String)
End Class
