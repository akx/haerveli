Public Class SequenceView

    Public Property Project As ProjectViewModel
        Get
            Return GetValue(ProjectProperty)
        End Get

        Set(ByVal value As ProjectViewModel)
            SetValue(ProjectProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ProjectProperty As DependencyProperty = _
                           DependencyProperty.Register("Project", _
                           GetType(ProjectViewModel), GetType(SequenceView), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property ViewModel As SequenceViewModel
        Get
            Return GetValue(ViewModelProperty)
        End Get

        Set(ByVal value As SequenceViewModel)
            SetValue(ViewModelProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ViewModelProperty As DependencyProperty = _
                           DependencyProperty.Register("ViewModel", _
                           GetType(SequenceViewModel), GetType(SequenceView), _
                           New FrameworkPropertyMetadata(Nothing))



    Public Property SelectedNote As NoteViewModel
        Get
            Return GetValue(SelectedNoteProperty)
        End Get

        Set(ByVal value As NoteViewModel)
            SetValue(SelectedNoteProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SelectedNoteProperty As DependencyProperty = _
                           DependencyProperty.Register("SelectedNote", _
                           GetType(NoteViewModel), GetType(SequenceView), _
                           New FrameworkPropertyMetadata(Nothing))



    Public Property SelectedParameter As ParameterViewModel
        Get
            Return GetValue(SelectedParameterProperty)
        End Get

        Set(ByVal value As ParameterViewModel)
            SetValue(SelectedParameterProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SelectedParameterProperty As DependencyProperty = _
                           DependencyProperty.Register("SelectedParameter", _
                           GetType(ParameterViewModel), GetType(SequenceView), _
                           New FrameworkPropertyMetadata(Nothing))

    Private Sub ShiftLeftButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Me.SelectedNote Is Nothing Then Return
        If Me.ViewModel.Notes.Count < 2 Then Return
        Dim note As NoteViewModel = Me.SelectedNote
        Dim selectedIndex As Integer = Me.ViewModel.Notes.IndexOf(Me.SelectedNote)
        Dim newIndex As Integer = 0
        If selectedIndex > 0 Then
            Me.ViewModel.Notes.Remove(note)
            newIndex = selectedIndex - 1
            Me.ViewModel.Notes.Insert(newIndex, note)
        Else
            Me.ViewModel.Notes.Remove(note)
            newIndex = Me.ViewModel.Notes.IndexOf(Me.ViewModel.Notes.Last) + 1
            Me.ViewModel.Notes.Insert(newIndex, note)
        End If
        Me.SelectedNote = note
    End Sub

    Private Sub ShiftRightButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Me.SelectedNote Is Nothing Then Return
        If Me.ViewModel.Notes.Count < 2 Then Return
        Dim note As NoteViewModel = Me.SelectedNote
        Dim selectedIndex As Integer = Me.ViewModel.Notes.IndexOf(Me.SelectedNote)
        Dim newIndex As Integer = 0
        Dim lastIndex As Integer = Me.ViewModel.Notes.IndexOf(Me.ViewModel.Notes.Last)
        If selectedIndex < lastIndex Then
            Me.ViewModel.Notes.Remove(note)
            newIndex = selectedIndex + 1
            Me.ViewModel.Notes.Insert(newIndex, note)
        Else
            Me.ViewModel.Notes.Remove(note)
            newIndex = 0
            Me.ViewModel.Notes.Insert(newIndex, note)
        End If
        Me.SelectedNote = note
    End Sub

    Private Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim note As New NoteViewModel(Me.Project)
        note.Key = KeyName.Unassigned
        note.DurationPpqn = 6
        note.ReleasePointPpqn = 6
        note.Patch = Me.Project.SelectedPatch
        If Me.SelectedNote IsNot Nothing Then
            Dim selectedIndex As Integer = Me.ViewModel.Notes.IndexOf(Me.SelectedNote)
            Me.ViewModel.Notes.Insert(selectedIndex + 1, note)
        Else
            Me.ViewModel.Notes.Add(note)
        End If
        Me.SelectedNote = note
        Me.ViewModel.UpdateSequenceLength()
    End Sub

    Private Sub DeleteButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Me.SelectedNote IsNot Nothing Then
            Dim selectedIndex As Integer = Me.ViewModel.Notes.IndexOf(Me.SelectedNote)
            Me.ViewModel.Notes.Remove(Me.SelectedNote)
            If selectedIndex < Me.ViewModel.Notes.Count() Then
                Me.SelectedNote = Me.ViewModel.Notes(selectedIndex)
            ElseIf Me.ViewModel.Notes.Count > 0 Then
                Me.SelectedNote = Me.ViewModel.Notes.Last()
            Else
                Me.SelectedNote = Nothing
            End If
        End If
        Me.ViewModel.UpdateSequenceLength()
    End Sub

    Private Sub DecreaseBpmButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Project.Bpm -= 1
    End Sub

    Private Sub IncreaseBpmButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Project.Bpm += 1
    End Sub

    Private Sub AddParameterButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Me.SelectedNote Is Nothing Then Return
        Dim param As New ParameterViewModel(Me.SelectedNote)
        param.Target = Me.Project.SelectedOperator
        param.ParameterId = ParameterName.oscillator_level
        Me.SelectedNote.Parameters.Add(param)
    End Sub
End Class
