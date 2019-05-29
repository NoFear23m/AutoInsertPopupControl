Public Class RichtextViewer



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
