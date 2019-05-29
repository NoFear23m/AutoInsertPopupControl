Imports System.Globalization

Namespace Converter
    Public Class FilterStrategyToIndexConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return CInt(value)
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Select Case CInt(value)
                Case 0
                    Return AutoInsertPopup.FilterMethod.Contains
                Case 1
                    Return AutoInsertPopup.FilterMethod.StartsWith
                Case Else
                    Throw New Exception("Unexpected value for FilterStrategy")
            End Select
        End Function
    End Class
End Namespace
