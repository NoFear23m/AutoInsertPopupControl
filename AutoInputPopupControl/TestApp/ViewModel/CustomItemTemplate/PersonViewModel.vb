﻿Namespace ViewModel
    Public Class PersonViewModel
        Inherits ViewModelBase

        Public Sub New()

        End Sub

        Public Sub New(fName As String, lName As String, mail As String, image As String)
            FirstName = fName : LastName = lName : Mailaddress = mail : PersonImage = image
        End Sub

        Public Property FirstName As String
        Public Property LastName As String
        Public Property Mailaddress As String
        Public Property PersonImage As String

        Public ReadOnly Property FullName As String
            Get
                Return $"{FirstName} {LastName}"
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return FullName
        End Function

    End Class
End Namespace
