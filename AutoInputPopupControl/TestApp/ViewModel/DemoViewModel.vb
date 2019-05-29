Namespace ViewModel
    Public Class DemoViewModel
        Inherits ViewModelBase

        Private _demoTitle As String
        Public Property DemoTitle As String
            Get
                Return _demoTitle
            End Get
            Set(ByVal value As String)
                _demoTitle = Value
                RaisePropertyChanged()
            End Set
        End Property


        Private _demoDescription As String
        Public Property DemoDescription As String
            Get
                Return _demoDescription
            End Get
            Set(ByVal value As String)
                _demoDescription = Value
                RaisePropertyChanged()
            End Set
        End Property

        Private _demoContent As Object
        Public Property DemoContent As Object
            Get
                Return _demoContent
            End Get
            Set(ByVal value As Object)
                _demoContent = Value
                RaisePropertyChanged()
            End Set
        End Property



    End Class
End Namespace
