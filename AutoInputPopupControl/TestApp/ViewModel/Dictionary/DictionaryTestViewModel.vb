Namespace ViewModel
    Public Class DictionaryTestViewModel
        Inherits ViewModelBase

        Public Sub New()
            Dim lines() As String = IO.File.ReadAllLines(My.Application.Info.DirectoryPath & "\Data\deutsch.txt")

            T4List = New List(Of String)
            T4List.AddRange(lines.Where(Function(o) o.Length > 3).OrderBy(Function(x) x.Length))
        End Sub

        Public Property T4List As List(Of String)

    End Class
End Namespace
