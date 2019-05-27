Imports System.Globalization

Namespace Converter
    Public Class ShortToThicknessConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return CShort(DirectCast(value, Thickness).Left)

        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return New Thickness(CShort(value))

        End Function
    End Class
End Namespace
