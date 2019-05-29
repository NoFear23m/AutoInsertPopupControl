Imports System.Globalization

Namespace Converter
    Public Class ResourceConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Dim resKey = value.ToString
            Return My.Resources.ResourceManager.GetObject(resKey)

        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Return value
        End Function
    End Class
End Namespace
