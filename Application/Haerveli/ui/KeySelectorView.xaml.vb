Public Class KeySelectorView

    Public Property Note As NoteViewModel
        Get
            Return GetValue(NoteProperty)
        End Get

        Set(ByVal value As NoteViewModel)
            SetValue(NoteProperty, value)
        End Set
    End Property

    Public Shared ReadOnly NoteProperty As DependencyProperty = _
                           DependencyProperty.Register("Note", _
                           GetType(NoteViewModel), GetType(KeySelectorView), _
                           New FrameworkPropertyMetadata(Nothing))


    Private Sub CButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            CButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.C
    End Sub

    Private Sub DButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            DButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.D
    End Sub

    Private Sub EButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            EButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.E
    End Sub

    Private Sub FButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            FButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.F
    End Sub

    Private Sub GButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            GButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.G
    End Sub

    Private Sub AButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            AButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.A
    End Sub

    Private Sub BButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            BButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.B
    End Sub

    Private Sub CisButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            CisButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.Cis
    End Sub

    Private Sub DisButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            DisButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.Dis
    End Sub

    Private Sub FisButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            FisButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.Fis
    End Sub

    Private Sub GisButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            GisButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.Gis
    End Sub

    Private Sub AisButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            AisButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.Ais
    End Sub

    Private Sub RestButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then
            RestButton.IsChecked = Nothing
            Return
        End If
        Note.Key = KeyName.Unassigned
    End Sub


    Protected Overrides Sub OnPropertyChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        MyBase.OnPropertyChanged(e)

        If e.Property Is NoteProperty Then
            If Note Is Nothing Then
                If RestButton IsNot Nothing Then
                    RestButton.IsChecked = True
                    RestButton.IsChecked = Nothing
                End If
                Return
            End If

            Select Case Note.Key
                Case KeyName.C
                    CButton.IsChecked = True
                Case KeyName.Cis
                    CisButton.IsChecked = True
                Case KeyName.D
                    DButton.IsChecked = True
                Case KeyName.Dis
                    DisButton.IsChecked = True
                Case KeyName.E
                    EButton.IsChecked = True
                Case KeyName.F
                    FButton.IsChecked = True
                Case KeyName.Fis
                    FisButton.IsChecked = True
                Case KeyName.G
                    GButton.IsChecked = True
                Case KeyName.Gis
                    GisButton.IsChecked = True
                Case KeyName.A
                    AButton.IsChecked = True
                Case KeyName.Ais
                    AisButton.IsChecked = True
                Case KeyName.B
                    BButton.IsChecked = True
                Case KeyName.Unassigned
                    RestButton.IsChecked = True
            End Select
        End If
    End Sub

    Private Sub DecreaseButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then Return
        Note.Octave -= 1
    End Sub

    Private Sub IncreaseButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Note Is Nothing Then Return
        Note.Octave += 1
    End Sub

End Class
