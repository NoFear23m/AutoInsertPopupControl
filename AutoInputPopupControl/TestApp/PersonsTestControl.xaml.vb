Public Class PersonsTestControl
    Private Sub Pop_ItemSelected(sender As Object, e As RoutedEventArgs)
        Dim person As PersonViewModel = CType(DirectCast(sender, AutoInsertPopup.AutoInsertPopupControl).SelectedInsertListItem.ViewContent, PersonViewModel)
        Dim vm As PersonsViewModel = CType(Me.DataContext, PersonsViewModel)
        If Not vm.MarkedPersons.Any(Function(p) p.FullName = person.FullName) Then
            vm.MarkedPersons.Add(CType(DirectCast(sender, AutoInsertPopup.AutoInsertPopupControl).SelectedInsertListItem.ViewContent, PersonViewModel))
        End If
    End Sub
End Class
