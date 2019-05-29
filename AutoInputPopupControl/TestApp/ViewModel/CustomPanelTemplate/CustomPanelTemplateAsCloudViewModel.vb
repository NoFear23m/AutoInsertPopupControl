Imports AutoInsertPopup

Namespace ViewModel
    Public Class CustomPanelTemplateAsCloudViewModel
        Inherits ViewModelBase


        Public Sub New()
            WordList = New List(Of IAutoInsertItem)

            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "XAML", .Size = 10}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "WPF", .Size = 10}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "Visual Basic", .Size = 9}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "C#", .Size = 9}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "Templates", .Size = 8}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "Controls", .Size = 8}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "ControlTemplate", .Size = 6}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "DataTemplate", .Size = 6}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "ResourceDictionary", .Size = 4}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "UserControl", .Size = 4}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "Code", .Size = 8}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "Developer", .Size = 6}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "Build", .Size = 3}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "Debug", .Size = 4}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "Debugging", .Size = 1}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "Visual Studio", .Size = 5}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "IDE", .Size = 1}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "UWP", .Size = 3}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "Xamarin", .Size = 2}))
            WordList.Add(New AutoInsertItem(New WordCloudItem() With {.Caption = "WinForms", .Size = 1}))

        End Sub



        Private _wordList As List(Of IAutoInsertItem)
        Public Property WordList As List(Of IAutoInsertItem)
            Get
                Return _wordList
            End Get
            Set(ByVal value As List(Of IAutoInsertItem))
                _wordList = value
                RaisePropertyChanged()
            End Set
        End Property


    End Class

    Public Class WordCloudItem
        Public Property Caption As String
        Public Property Size As Integer

        Public Overrides Function ToString() As String
            Return Caption
        End Function
    End Class

End Namespace
