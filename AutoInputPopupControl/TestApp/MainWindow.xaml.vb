Class MainWindow


    Private Sub TxtTest_GotFocus(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TestList = New List(Of AutoInsertPopup.AutoInsertItem) From {
           New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Helmut", "Baumgartner", "helmut.baumgartner@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/file_1431426288636.jpg"), "helmut baumgartner", "Helmut Baumgartner"),
           New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Manuela", "Chala", "manuela.chala@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=6200/file_1431426318122.jpg"), "manuela chala", "Manuela Chala"),
           New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Bianca", "Dallinger", "bianca.dallinger@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/upload8523090150931023799.jpg"), "bianca dallinger", "Bianca Dallinger")}
        TestList1 = New List(Of AutoInsertPopup.AutoInsertItem) From {
           New AutoInsertPopup.AutoInsertItem("Content 1", "Content 1", "Content 1"),
           New AutoInsertPopup.AutoInsertItem("Content 2", "Content 2", "Content 2"),
           New AutoInsertPopup.AutoInsertItem("Content 3", "Content 3", "Content 3")}
        Me.DataContext = Me
    End Sub

    Public Property TestList As IEnumerable(Of AutoInsertPopup.IAutoInsertItem)
    Public Property TestList1 As IEnumerable(Of AutoInsertPopup.IAutoInsertItem)
    Private Sub TxtTest_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Down Then
            'pop.FocusListbox()
        End If
    End Sub

    Private Sub Pop_ItemSelected(sender As Object, e As RoutedEventArgs)
        'Console.WriteLine("Selected")
    End Sub

    Private _selectItemCommand As ICommand
    Public Property SelectItemCommand() As ICommand
        Get
            If _selectItemCommand Is Nothing Then _selectItemCommand = New AutoInsertPopup.RelayCommand(AddressOf Test)
            Return _selectItemCommand
        End Get
        Set(ByVal value As ICommand)
            _selectItemCommand = value
        End Set
    End Property





    Private Sub Test(obj As Object)
        MessageBox.Show("Huhu")
    End Sub
End Class
