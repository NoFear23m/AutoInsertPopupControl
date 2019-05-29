Imports System.Globalization

Namespace Converter
    Public Class PopupAnimationToIndexConverter
        Implements IValueConverter


        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return CInt(value)
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Select Case CInt(value)
                Case 0
                    Return Primitives.PopupAnimation.None
                Case 1
                    Return Primitives.PopupAnimation.Fade
                Case 2
                    Return Primitives.PopupAnimation.Slide
                Case 3
                    Return Primitives.PopupAnimation.Scroll
                Case Else
                    Throw New Exception("Unexpected value for PopupAnimation")
            End Select
        End Function
    End Class
End Namespace
