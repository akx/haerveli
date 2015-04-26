Public Class OperatorView

    Public Property ViewModel As OperatorViewModel
        Get
            Return GetValue(ViewModelProperty)
        End Get

        Set(ByVal value As OperatorViewModel)
            SetValue(ViewModelProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ViewModelProperty As DependencyProperty = _
                           DependencyProperty.Register("ViewModel", _
                           GetType(OperatorViewModel), GetType(OperatorView), _
                           New FrameworkPropertyMetadata(Nothing))


    Private Sub Slider_PreviewMouseWheel(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseWheelEventArgs)
        Dim slider As Slider = CType(sender, Slider)
        Dim tick As Double = slider.TickFrequency
        If e.Delta > 0 Then
            slider.Value += tick
        Else
            slider.Value -= tick
        End If
        slider.Value = Math.Round(slider.Value, 5)
    End Sub
End Class
