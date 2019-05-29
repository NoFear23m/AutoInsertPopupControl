Imports System.Collections.ObjectModel
Imports System.ComponentModel

Namespace ViewModel
    Public Class DemoListViewModel
        Inherits ViewModelBase


        Public Sub New()

            AviableDemos = New ObservableCollection(Of DemoViewModel)
            AviableDemos.Add(New DemoViewModel() With {
                             .DemoTitle = "Normaler Test (Brief)",
                             .DemoDescription = "Ein normaler Test mit einer List(Of String) für Satzvorlagen eines Breifs.",
                             .DemoContent = New NormalTestViewModel})
            AviableDemos.Add(New DemoViewModel() With {
                             .DemoTitle = "Normaler Test (Brief) mit IAutoInsertItem",
                             .DemoDescription = "Ein normaler Test mit einer List(Of IAutoInsertItem) für Satzvorlagen eines Breifs mit zugriff über kurze Suchstrings.",
                             .DemoContent = New NormalTestExtendedViewModel})
            AviableDemos.Add(New DemoViewModel() With {
                             .DemoTitle = "Test mit Personen mittels ListItemTemplate",
                             .DemoDescription = "Markieren von Personen mittels eigenem DataTemplate für eine Person",
                             .DemoContent = New PersonItemTemplateViewModel})
            AviableDemos.Add(New DemoViewModel() With {
                             .DemoTitle = "Test mit Personen mittels ListItemTemplate und Costum HeaderTemplate",
                             .DemoDescription = "Markieren von Personen mittels eigenem DataTemplate für eine Person und einem eigenen HeaderTemplate",
                             .DemoContent = New PersonsWithCustomHeaderViewModel})
            AviableDemos.Add(New DemoViewModel() With {
                             .DemoTitle = "Test mit WordClound mittels Custom PanelTemplate",
                             .DemoDescription = "Mithilfe eines eigenen PanelTemplates die Liste in eine WordCloud verwandeln",
                             .DemoContent = New CustomPanelTemplateAsCloudViewModel})
            AviableDemos.Add(New DemoViewModel() With {
                             .DemoTitle = "Test mit großer Liste (Wörterbuch)",
                             .DemoDescription = "Mit über 188 000 Einträgen wird hier die Performance getestet",
                             .DemoContent = New DictionaryTestViewModel})
            AviableDemosView = CollectionViewSource.GetDefaultView(AviableDemos)
            AviableDemosView.Filter = AddressOf DoFilter
            AviableDemosView.MoveCurrentToFirst()
        End Sub
        Private Function DoFilter(ByVal obj As Object) As Boolean
            If FilterString Is Nothing Then Return True
            Dim demo = DirectCast(obj, DemoViewModel)
            Return demo.DemoTitle.ToLower.Contains(FilterString.ToLower) OrElse demo.DemoDescription.ToLower.Contains(FilterString.ToLower)
        End Function





        Private _aviableDemos As ObservableCollection(Of DemoViewModel)
        Public Property AviableDemos As ObservableCollection(Of DemoViewModel)
            Get
                Return _aviableDemos
            End Get
            Set(ByVal value As ObservableCollection(Of DemoViewModel))
                _aviableDemos = value
                RaisePropertyChanged()
            End Set
        End Property

        Private _aviableDemosView As ICollectionView
        Public Property AviableDemosView As ICollectionView
            Get
                Return _aviableDemosView
            End Get
            Set(ByVal value As ICollectionView)
                _aviableDemosView = value
                RaisePropertyChanged()
            End Set
        End Property

        Private _selectedDemo As DemoViewModel
        Public Property SelectedDemo As DemoViewModel
            Get
                Return _selectedDemo
            End Get
            Set(ByVal value As DemoViewModel)
                _selectedDemo = Value
                RaisePropertyChanged()
            End Set
        End Property

        Private _filterString As String
        Public Property FilterString As String
            Get
                Return _filterString
            End Get
            Set(ByVal value As String)
                _filterString = value
                RaisePropertyChanged()
                AviableDemosView.Refresh()
            End Set
        End Property

    End Class
End Namespace
