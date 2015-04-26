Imports System.Collections.ObjectModel

Public Class SequenceViewModel
    Inherits ViewModelBase

#Region "Constructors"

    Public Sub New(ByVal project As ProjectViewModel)
        Me.Project = project
        Me.Notes = New ObservableCollection(Of NoteViewModel)
    End Sub

    Public Sub New(ByVal project As ProjectViewModel, ByVal data As String)
        Me.New(project)
        Me.ConstructFrom(data)
    End Sub

#End Region

#Region "Properties"

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
                           GetType(ProjectViewModel), GetType(SequenceViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property Notes As ObservableCollection(Of NoteViewModel)
        Get
            Return GetValue(NotesProperty)
        End Get

        Set(ByVal value As ObservableCollection(Of NoteViewModel))
            SetValue(NotesProperty, value)
        End Set
    End Property

    Public Shared ReadOnly NotesProperty As DependencyProperty = _
                           DependencyProperty.Register("Notes", _
                           GetType(ObservableCollection(Of NoteViewModel)), GetType(SequenceViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property SequenceLength As Double
        Get
            Return GetValue(SequenceLengthProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(SequenceLengthProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SequenceLengthProperty As DependencyProperty = _
                           DependencyProperty.Register("SequenceLength", _
                           GetType(Double), GetType(SequenceViewModel), _
                           New FrameworkPropertyMetadata(0.0))

#End Region

#Region "Methods"

    Public Sub UpdateSequenceLength()
        Dim totalPpqn = (From n As NoteViewModel In Notes Select n.DurationPpqn).Sum()
        Dim quarterNotes As Integer = CInt(Math.Floor(totalPpqn / 6.0))
        Dim ppqn As Integer = totalPpqn - (quarterNotes * 6)
        SequenceLength = quarterNotes + (ppqn / 10.0)
    End Sub

    Private Sub ConstructFrom(ByVal data As String)
        Dim notes As String() = data.Split(New String() {"note"}, StringSplitOptions.RemoveEmptyEntries)
        For Each n As String In notes
            Me.Notes.Add(New NoteViewModel(Project, n))
        Next
        UpdateSequenceLength()
    End Sub

    Public Overrides Function ToString() As String
        Dim result As String = String.Empty
        For Each n As NoteViewModel In Me.Notes
            result &= n.ToString()
        Next
        Return result
    End Function

#End Region

End Class
