Imports System.ComponentModel

Public MustInherit Class ViewModelBase
    Inherits DependencyObject
    Implements INotifyPropertyChanged

#Region "Methods"

    Protected Function ReadStringValueFrom(ByVal row As String) As String
        Dim result As String = String.Empty
        Dim data As String() = row.Split(New [Char]() {CChar("=")})
        If data.Length > 1 Then
            result = data.Last().Trim()
        End If
        Return result
    End Function

    Protected Function ReadIntegerValueFrom(ByVal row As String) As Integer
        Dim result As Integer = 0
        Integer.TryParse(ReadStringValueFrom(row), result)
        Return result
    End Function

    Protected Function ReadDoubleValueFrom(ByVal row As String) As Double
        Dim result As Double = 0
        Double.TryParse(ReadStringValueFrom(row), result)
        Return result
    End Function

#End Region


#Region "INotifyPropertyChanged implementation"

    Protected Sub NotifyPropertyChanged(ByVal sender As Object, ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

#End Region

End Class
