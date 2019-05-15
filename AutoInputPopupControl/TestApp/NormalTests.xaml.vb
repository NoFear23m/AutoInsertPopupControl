Public Class NormalTests
    Private Sub NormalTests_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TestList = New List(Of AutoInsertPopup.AutoInsertItem) From {
           New AutoInsertPopup.AutoInsertItem("Content 1", "Content 1", "Content 1"),
           New AutoInsertPopup.AutoInsertItem("Content 2", "Content 2", "Content 2"),
           New AutoInsertPopup.AutoInsertItem("Content 3", "Content 3", "Content 3")}

        Me.DataContext = Me
    End Sub

    Public Property TestList As IEnumerable(Of AutoInsertPopup.IAutoInsertItem)
End Class
