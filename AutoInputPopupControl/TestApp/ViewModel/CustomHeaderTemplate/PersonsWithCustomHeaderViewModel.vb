Imports System.Collections.ObjectModel

Namespace ViewModel
    Public Class PersonsWithCustomHeaderViewModel
        Inherits ViewModelBase

        Public Sub New()
            Persons = New ObservableCollection(Of AutoInsertPopup.IAutoInsertItem)


            Persons.Add(New AutoInsertPopup.AutoInsertItem(
                        New PersonViewModel("Bill", "Gates", "bill@microsoft.com",
                                            "https://upload.wikimedia.org/wikipedia/commons/thumb/0/01/Bill_Gates_July_2014.jpg/220px-Bill_Gates_July_2014.jpg")))
            Persons.Add(New AutoInsertPopup.AutoInsertItem(
                        New PersonViewModel("Steve", "Jobs", "jobs@apple.com",
                                            "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f5/Steve_Jobs_Headshot_2010-CROP2.jpg/170px-Steve_Jobs_Headshot_2010-CROP2.jpg")))
        End Sub



        Private _persons As ObservableCollection(Of AutoInsertPopup.IAutoInsertItem)
        Public Property Persons As ObservableCollection(Of AutoInsertPopup.IAutoInsertItem)
            Get
                Return _persons
            End Get
            Set(ByVal value As ObservableCollection(Of AutoInsertPopup.IAutoInsertItem))
                _persons = value
                RaisePropertyChanged()
            End Set
        End Property






    End Class
End Namespace
