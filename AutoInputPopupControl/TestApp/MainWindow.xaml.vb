Class MainWindow
    Private Sub TxtTest_GotFocus(sender As Object, e As RoutedEventArgs)
        'pop.IsPopUpOpen = True
        'Keyboard.Focus(pop)
        'DirectCast(pop.Child, ListBox).Focus()


        'list.SelectedItem = "Test1"
        'list.Focus()




    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TestList = New List(Of AutoInsertPopup.AutoInsertItem) From {
           New AutoInsertPopup.AutoInsertItem("Content 1", "Search 1", "Insert 1"),
           New AutoInsertPopup.AutoInsertItem("Content 2", "Search 2", "Insert 2"),
           New AutoInsertPopup.AutoInsertItem("Content 3", "Search 3", "Insert 3")}
        Me.DataContext = Me
    End Sub

    Public Property TestList As IEnumerable(Of AutoInsertPopup.IAutoInsertItem)

    Private Sub TxtTest_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Down Then
            'pop.FocusListbox()
        End If
    End Sub

    Private Sub Pop_ItemSelected(sender As Object, e As RoutedEventArgs)
        Console.WriteLine("Selected")
    End Sub
End Class
