Imports System.Globalization

Namespace Converter
    Public Class ColorToBrushConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            If value Is Nothing Then Return Nothing
            Select Case value.GetType
                Case GetType(SolidColorBrush)
                    Return DirectCast(value, SolidColorBrush).Color
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            If value Is Nothing Then Return Nothing
            Return New SolidColorBrush(DirectCast(value, Color))
        End Function
    End Class
End Namespace
