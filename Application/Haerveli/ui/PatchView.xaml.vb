Public Class PatchView

    Public Property Patch As PatchViewModel
        Get
            Return GetValue(PatchProperty)
        End Get

        Set(ByVal value As PatchViewModel)
            SetValue(PatchProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PatchProperty As DependencyProperty = _
                           DependencyProperty.Register("Patch", _
                           GetType(PatchViewModel), GetType(PatchView), _
                           New FrameworkPropertyMetadata(Nothing))


    Private Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Patch.AddNewModulation()
    End Sub

End Class
