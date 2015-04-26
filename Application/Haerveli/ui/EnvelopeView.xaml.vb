Public Class EnvelopeView

    Public Property ViewModel As EnvelopeViewModel
        Get
            Return GetValue(ViewModelProperty)
        End Get

        Set(ByVal value As EnvelopeViewModel)
            SetValue(ViewModelProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ViewModelProperty As DependencyProperty = _
                           DependencyProperty.Register("ViewModel", _
                           GetType(EnvelopeViewModel), GetType(EnvelopeView), _
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
