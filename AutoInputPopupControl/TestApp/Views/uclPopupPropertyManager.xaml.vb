Public Class uclPopupPropertyManager

    Public Sub New()

        ' Dieser Aufruf ist für den Designer erforderlich.
        InitializeComponent()

        ' Fügen Sie Initialisierungen nach dem InitializeComponent()-Aufruf hinzu.
        AviableKeys = [Enum].GetNames(GetType(Key)).ToList
    End Sub


    Public Property TargetPopupControl As AutoInsertPopup.AutoInsertPopupControl
        Get
            Return CType(GetValue(TargetPopupControlProperty), AutoInsertPopup.AutoInsertPopupControl)
        End Get

        Set(ByVal value As AutoInsertPopup.AutoInsertPopupControl)
            SetValue(TargetPopupControlProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TargetPopupControlProperty As DependencyProperty =
                           DependencyProperty.Register("TargetPopupControl",
                           GetType(AutoInsertPopup.AutoInsertPopupControl), GetType(uclPopupPropertyManager),
                           New PropertyMetadata(Nothing))


    Public Property AviableKeys As List(Of String)


End Class
