Namespace ViewModel
    Public Class NormalTestExtendedViewModel
        Inherits ViewModelBase

        Public Sub New()
            TestList = New List(Of AutoInsertPopup.AutoInsertItem) From {
                       New AutoInsertPopup.AutoInsertItem("Sehr Geehrter Herr ", "anrede"),
                       New AutoInsertPopup.AutoInsertItem("Sehr Geehrte Frau", "anrede"),
                       New AutoInsertPopup.AutoInsertItem("Mit freundlichen Grüßen", "mfg"),
                       New AutoInsertPopup.AutoInsertItem("Leider ist das von Ihnen gewünschte Produdkt im Moment nicht verfügbar"),
                       New AutoInsertPopup.AutoInsertItem($"Patschka Sascha {Environment.NewLine} Musterfirma {Environment.NewLine} Musterstrasse 123", "signatur"),
                       New AutoInsertPopup.AutoInsertItem("Vielen Dank für Ihre werte Anfrage", "anfrage")}
        End Sub

        Public Property TestList As IEnumerable(Of AutoInsertPopup.IAutoInsertItem)

    End Class
End Namespace
