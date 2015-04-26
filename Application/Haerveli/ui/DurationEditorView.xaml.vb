Public Class DurationEditorView

#Region "Properties"

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
                           GetType(NoteViewModel), GetType(DurationEditorView), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property Steps As Integer
        Get
            Return GetValue(StepsProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(StepsProperty, value)
        End Set
    End Property

    Public Shared ReadOnly StepsProperty As DependencyProperty = _
                           DependencyProperty.Register("Steps", _
                           GetType(Integer), GetType(DurationEditorView), _
                           New FrameworkPropertyMetadata(0))


    Public Property Parts As Integer
        Get
            Return GetValue(PartsProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(PartsProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PartsProperty As DependencyProperty = _
                           DependencyProperty.Register("Parts", _
                           GetType(Integer), GetType(DurationEditorView), _
                           New FrameworkPropertyMetadata(0))


    Public Property TypeFlag As Integer
        Get
            Return GetValue(TypeFlagProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(TypeFlagProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TypeFlagProperty As DependencyProperty = _
                           DependencyProperty.Register("TypeFlag", _
                           GetType(Integer), GetType(DurationEditorView), _
                           New FrameworkPropertyMetadata(0))


#End Region

    Protected Overrides Sub OnPropertyChanged(ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        MyBase.OnPropertyChanged(e)
        If Note Is Nothing Then Return

        If e.Property Is NoteProperty Then
            Dim ppqn As Integer = 0
            Select Case TypeFlag
                Case 0
                    ppqn = Note.DurationPpqn
                Case 1
                    ppqn = Note.ReleasePointPpqn
            End Select
            Me.Steps = Math.Floor(ppqn / 6.0)
            Me.Parts = ppqn Mod 6

        ElseIf (e.Property Is StepsProperty) Or (e.Property Is PartsProperty) Then
            UpdatePpqn()
        End If

    End Sub


    Private Sub UpdatePpqn()
        If Note Is Nothing Then Return

        Dim ppqn As Integer = Steps * 6 + Parts
        Select Case TypeFlag
            Case 0
                Note.DurationPpqn = ppqn
            Case 1
                Note.ReleasePointPpqn = ppqn
        End Select

        Note.Project.SelectedSequence.UpdateSequenceLength()
    End Sub

    Private Sub DecreaseStepButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Steps > 0 Then Steps -= 1
    End Sub

    Private Sub IncreaseStepButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Steps += 1
    End Sub

    Private Sub DecreasePartButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Parts > 0 Then Parts -= 1
    End Sub

    Private Sub IncreasePartButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Parts < 5 Then Parts += 1
    End Sub
End Class
