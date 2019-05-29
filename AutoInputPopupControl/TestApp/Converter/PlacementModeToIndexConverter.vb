Imports System.Globalization

Namespace Converter
    Public Class PlacementModeToIndexConverter
        Implements IValueConverter

        Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
            Return CInt(value)
        End Function

        Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
            Select Case CInt(value)
                Case 0
                    Return Primitives.PlacementMode.Absolute
                Case 1
                    Return Primitives.PlacementMode.Relative
                Case 2
                    Return Primitives.PlacementMode.Bottom
                Case 3
                    Return Primitives.PlacementMode.Center
                Case 4
                    Return Primitives.PlacementMode.Right
                Case 5
                    Return Primitives.PlacementMode.AbsolutePoint
                Case 6
                    Return Primitives.PlacementMode.RelativePoint
                Case 7
                    Return Primitives.PlacementMode.Mouse
                Case 8
                    Return Primitives.PlacementMode.MousePoint
                Case 9
                    Return Primitives.PlacementMode.Left
                Case 10
                    Return Primitives.PlacementMode.Top
                Case 11
                    Return Primitives.PlacementMode.Custom
                Case Else
                    Throw New Exception("Unexpected value for PlacementMode")
            End Select
        End Function
    End Class
End Namespace
