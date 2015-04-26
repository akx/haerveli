Public Class ParameterView

    Public Property ViewModel As ParameterViewModel
        Get
            Return GetValue(ViewModelProperty)
        End Get

        Set(ByVal value As ParameterViewModel)
            SetValue(ViewModelProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ViewModelProperty As DependencyProperty = _
                           DependencyProperty.Register("ViewModel", _
                           GetType(ParameterViewModel), GetType(ParameterView), _
                           New FrameworkPropertyMetadata(Nothing))



    Private Sub DeleteButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        ViewModel.Note.Parameters.Remove(ViewModel)
    End Sub

End Class
