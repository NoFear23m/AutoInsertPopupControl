
Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace ViewModel
    Public MustInherit Class ViewModelBase
        Implements INotifyPropertyChanged, IDisposable



        Private Shared ReadOnly HostProcesses As New List(Of String)({"XDesProc", "devenv", "WDExpress"})
        Public ReadOnly Property IsInDesignMode As Boolean
            Get
                Return HostProcesses.Contains(Process.GetCurrentProcess().ProcessName)
            End Get
        End Property



#Region "INotifyPropertyChanged"

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged


        ''' <summary>
        ''' Prozedur wirft den INotifyPropertyChanged Event welcher in der WPF benötigt wird um die UI zu verständingen
        ''' das eine Änderung an einem Property stattgefunden hat.
        ''' </summary>
        ''' <param name="prop">Das Propertie welche sich geändert hat. Ist Optional das der Parameter "CallerMemberName" verwendet</param>
        Protected Overridable Sub RaisePropertyChanged(<CallerMemberName> Optional ByVal prop As String = "")
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(prop))
        End Sub


#End Region

#Region "IDisposable Support"
        Private _disposedValue As Boolean ' Dient zur Erkennung redundanter Aufrufe.

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposedValue Then
                If disposing Then

                End If

            End If
            _disposedValue = True
        End Sub


        ' Dieser Code wird von Visual Basic hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(disposing As Boolean) weiter oben ein.

            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub


#End Region

    End Class
End Namespace
