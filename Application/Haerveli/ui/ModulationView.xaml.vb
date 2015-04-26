Public Class ModulationView

    Public Property ViewModel As ModulationViewModel
        Get
            Return GetValue(ViewModelProperty)
        End Get

        Set(ByVal value As ModulationViewModel)
            SetValue(ViewModelProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ViewModelProperty As DependencyProperty = _
                           DependencyProperty.Register("ViewModel", _
                           GetType(ModulationViewModel), GetType(ModulationView), _
                           New FrameworkPropertyMetadata(Nothing))


    Private Sub DeleteButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.ViewModel.Remove()
    End Sub

End Class
