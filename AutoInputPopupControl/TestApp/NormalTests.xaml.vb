Public Class NormalTests
    Private Sub NormalTests_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TestList = New List(Of AutoInsertPopup.AutoInsertItem) From {
           New AutoInsertPopup.AutoInsertItem("Sehr Geehrter Herr ", "anrede"),
           New AutoInsertPopup.AutoInsertItem("Sehr Geehrte Frau", "anrede"),
           New AutoInsertPopup.AutoInsertItem("Mit freundlichen Grüßen", "mfg"),
           New AutoInsertPopup.AutoInsertItem("Leider ist das von Ihnen gewünschte Produdkt im Moment nicht verfügbar"),
           New AutoInsertPopup.AutoInsertItem($"Patschka Sascha {Environment.NewLine} Musterfirma {Environment.NewLine} Musterstrasse 123", "signatur"),
           New AutoInsertPopup.AutoInsertItem("Vielen Dank für Ihre werte Anfrage", "anfrage")}

        Dim lines() As String = IO.File.ReadAllLines(My.Application.Info.DirectoryPath & "\Data\deutsch.txt")

        T4List = New List(Of String)
        T4List.AddRange(lines.Where(Function(o) o.Length > 3).OrderBy(Function(x) x.Length))

        Me.DataContext = Me
    End Sub

    Public Property TestList As IEnumerable(Of AutoInsertPopup.IAutoInsertItem)
    Public Property T4List As List(Of String)
End Class
