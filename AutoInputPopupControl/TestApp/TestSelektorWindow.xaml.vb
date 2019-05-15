Public Class TestSelektorWindow




    Private Sub NormalTests_Click(sender As Object, e As RoutedEventArgs)
        Dim win As New NormalTests
        win.ShowDialog()
    End Sub

    Private Sub PersonsTests_Click(sender As Object, e As RoutedEventArgs)
        Dim win As New PersonsTestsWindow
        win.ShowDialog()
    End Sub
End Class
