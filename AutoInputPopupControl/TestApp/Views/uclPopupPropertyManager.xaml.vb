Public Class uclPopupPropertyManager



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




End Class
