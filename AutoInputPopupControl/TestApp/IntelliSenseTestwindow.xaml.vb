Public Class IntelliSenseTestwindow
    Private Sub IntelliSenseTestwindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TestList = New List(Of AutoInsertPopup.AutoInsertItem) From {
          New AutoInsertPopup.AutoInsertItem("If"),
          New AutoInsertPopup.AutoInsertItem("Then"),
          New AutoInsertPopup.AutoInsertItem("Else"),
          New AutoInsertPopup.AutoInsertItem("Public"),
          New AutoInsertPopup.AutoInsertItem("Private"),
          New AutoInsertPopup.AutoInsertItem("Friend"),
          New AutoInsertPopup.AutoInsertItem("Property"),
          New AutoInsertPopup.AutoInsertItem("Integer"),
          New AutoInsertPopup.AutoInsertItem("Int16"),
          New AutoInsertPopup.AutoInsertItem("Int32"),
          New AutoInsertPopup.AutoInsertItem("String"),
          New AutoInsertPopup.AutoInsertItem("Double"),
          New AutoInsertPopup.AutoInsertItem("Char"),
          New AutoInsertPopup.AutoInsertItem("Boolean"),
          New AutoInsertPopup.AutoInsertItem("Sub"),
          New AutoInsertPopup.AutoInsertItem("Function"),
          New AutoInsertPopup.AutoInsertItem("End"),
          New AutoInsertPopup.AutoInsertItem("As"),
          New AutoInsertPopup.AutoInsertItem("List"),
          New AutoInsertPopup.AutoInsertItem("ObserVableCollection"),
          New AutoInsertPopup.AutoInsertItem("Get"),
          New AutoInsertPopup.AutoInsertItem("Set"),
          New AutoInsertPopup.AutoInsertItem("Object"),
          New AutoInsertPopup.AutoInsertItem("Control"),
          New AutoInsertPopup.AutoInsertItem("Contains"),
          New AutoInsertPopup.AutoInsertItem("Is")}

        Me.DataContext = Me
    End Sub

    Public Property TestList As IEnumerable(Of AutoInsertPopup.IAutoInsertItem)


End Class
