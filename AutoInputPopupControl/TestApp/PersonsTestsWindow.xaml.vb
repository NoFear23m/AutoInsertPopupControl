Imports System.Collections.ObjectModel

Public Class PersonsTestsWindow
    Private Sub PersonsTestsWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded

        Me.DataContext = New PersonsViewModel
    End Sub





End Class
