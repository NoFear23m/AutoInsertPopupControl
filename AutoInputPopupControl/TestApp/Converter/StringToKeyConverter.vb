Imports System.Globalization

Namespace Converter
    Public Class StringToKeyConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return value.ToString
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Dim keyString As String = value.ToString
            Return DirectCast([Enum].Parse(GetType(Key), keyString), Key)
        End Function
    End Class
End Namespace
