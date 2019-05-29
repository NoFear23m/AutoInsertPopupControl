Public Class RichtextViewer

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.

    End Sub

    Public Property ResourceName As String
        Get
            Return CType(GetValue(ResourceNameProperty), String)
        End Get

        Set(ByVal value As String)
            SetValue(ResourceNameProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ResourceNameProperty As DependencyProperty =
                           DependencyProperty.Register("ResourceName",
                           GetType(String), GetType(RichtextViewer),
                           New PropertyMetadata(Nothing))



End Class
