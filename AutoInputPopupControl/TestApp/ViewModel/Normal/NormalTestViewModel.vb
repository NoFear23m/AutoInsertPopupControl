Namespace ViewModel
    Public Class NormalTestViewModel
        Inherits ViewModelBase

        Public Sub New()
            TestList = New List(Of String) From {
                "Sehr Geehrter Herr",
                "Sehr Geehrte Frau",
                "Mit freundlichen Grüßen",
                "Leider ist das von Ihnen gewünschte Produkt im Moment nicht verfügbar",
                "Wir danken Ihnen für Ihre gute 5 Sterne Bewertung und hoffen Sie bald wieder bei uns begrüßen zu dürfen",
                "Es tut uns leid das Sie mit dem von uns zugesandtem Produkt nicht zufrienden sind.",
                "Gerne tauschen wir das PRodukt gegen ein gleichwertiges anderes Produkt aus.",
                "Die Sendungsnummer lautet: ",
                $"Patschka Sascha {Environment.NewLine} Musterfirma {Environment.NewLine} Musterstrasse 123", "Vielen Dank für Ihre werte Anfrage"}

        End Sub


        Public Property TestList As List(Of String)

    End Class
End Namespace
