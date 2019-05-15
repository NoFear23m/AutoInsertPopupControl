Imports System.Collections.ObjectModel

Public Class PersonsTestsWindow
    Private Sub PersonsTestsWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        TestList = New List(Of AutoInsertPopup.AutoInsertItem) From {
           New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Helmut", "Baumgartner", "helmut.baumgartner@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/file_1431426288636.jpg"), "helmut baumgartner", "Helmut Baumgartner"),
           New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Wolfgang", "Dissauer", "wolfgang.dissauer@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/file_1431426339231.jpg"), "wolfang dissauer", "Wolfgang Dissauer"),
                      New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Erich", "Ebner", "ebner.erich@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=file_1431426359183.jpg"), "ebner erich", "Ebner Erich"),
                      New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Andreas", "Eidler", "andreas.eidler@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/file_1431426384223.jpg"), "andreas eidler", "Andreas Eidler"),
                      New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Kerstin", "Fuchs", "kerstin.fuchs@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/file_1431426429435.jpg"), "kerstin fuchs", "Kerstin Fuchs"),
           New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Bianca", "Dallinger", "bianca.dallinger@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/upload8523090150931023799.jpg"), "bianca dallinger", "Bianca Dallinger")}

        TestList.First.SortingIndex = -2

        TestList1 = New List(Of AutoInsertPopup.AutoInsertItem) From {
           New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Helmut", "Baumgartner", "helmut.baumgartner@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/file_1431426288636.jpg"), "001", "helmut.baumgartner@porsche.co.at"),
           New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Wolfgang", "Dissauer", "wolfgang.dissauer@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/file_1431426339231.jpg"), "002", "wolfgang.dissauer@porsche.co.at"),
                      New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Erich", "Ebner", "ebner.erich@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=file_1431426359183.jpg"), "003", "ebner.erich@porsche.co.at"),
                      New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Andreas", "Eidler", "andreas.eidler@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/file_1431426384223.jpg"), "004", "andreas.eidler@porsche.co.at"),
                      New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Kerstin", "Fuchs", "kerstin.fuchs@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/file_1431426429435.jpg"), "005", "kerstin.fuchs@porsche.co.at"),
           New AutoInsertPopup.AutoInsertItem(
           New PersonViewModel("Bianca", "Dallinger", "bianca.dallinger@porsche.co.at", "https://www.porschewrneustadt.at/@@poi.teamimages?image=200/upload8523090150931023799.jpg"), "006", "bianca.dallinger@porsche.co.at")}



        MarkedPersons = New ObservableCollection(Of PersonViewModel)
        Me.DataContext = Me
    End Sub


    Public Property TestList As IEnumerable(Of AutoInsertPopup.IAutoInsertItem)
    Public Property TestList1 As IEnumerable(Of AutoInsertPopup.IAutoInsertItem)
    Public Property MarkedPersons As ObservableCollection(Of PersonViewModel)

    Private Sub Pop_ItemSelected(sender As Object, e As RoutedEventArgs)
        Dim person As PersonViewModel = DirectCast(sender, AutoInsertPopup.AutoInsertPopupControl).SelectedInsertListItem.ViewContent
        If Not MarkedPersons.Any(Function(p) p.FullName = person.FullName) Then
            MarkedPersons.Add(DirectCast(sender, AutoInsertPopup.AutoInsertPopupControl).SelectedInsertListItem.ViewContent)
        End If
    End Sub
End Class
