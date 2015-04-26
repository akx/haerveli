Imports System.ComponentModel
Imports System.Collections.ObjectModel

Public Enum KeyName
    <Description("- ")> _
    Unassigned = -1
    <Description("C ")> _
    C = 0
    <Description("C#")> _
    Cis = 1
    <Description("D ")> _
    D = 2
    <Description("D#")> _
    Dis = 3
    <Description("E ")> _
    E = 4
    <Description("F ")> _
    F = 5
    <Description("F#")> _
    Fis = 6
    <Description("G ")> _
    G = 7
    <Description("G#")> _
    Gis = 8
    <Description("A ")> _
    A = 9
    <Description("A#")> _
    Ais = 10
    <Description("B ")> _
    B = 11
End Enum


Public Class NoteViewModel
    Inherits ViewModelBase

#Region "Constructors"

    Public Sub New(ByVal project As ProjectViewModel)
        _project = project
        Me.Parameters = New ObservableCollection(Of ParameterViewModel)
    End Sub

    Public Sub New(ByVal project As ProjectViewModel, ByVal data As String)
        Me.New(project)
        Me.ConstructFrom(data)
    End Sub

#End Region

#Region "Properties"

    Private _project As ProjectViewModel
    Public ReadOnly Property Project() As ProjectViewModel
        Get
            Return _project
        End Get
    End Property


    Public ReadOnly Property AvailablePatches As ObservableCollection(Of PatchViewModel)
        Get
            Return _project.Patches
        End Get
    End Property


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
                           GetType(PatchViewModel), GetType(NoteViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property DurationPpqn As Integer
        Get
            Return GetValue(DurationPpqnProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(DurationPpqnProperty, value)
        End Set
    End Property

    Public Shared ReadOnly DurationPpqnProperty As DependencyProperty = _
                           DependencyProperty.Register("DurationPpqn", _
                           GetType(Integer), GetType(NoteViewModel), _
                           New FrameworkPropertyMetadata(0))


    Public Property ReleasePointPpqn As Integer
        Get
            Return GetValue(ReleasePointPpqnProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(ReleasePointPpqnProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ReleasePointPpqnProperty As DependencyProperty = _
                           DependencyProperty.Register("ReleasePointPpqn", _
                           GetType(Integer), GetType(NoteViewModel), _
                           New FrameworkPropertyMetadata(0))


    Public Property Key As KeyName
        Get
            Return GetValue(KeyProperty)
        End Get

        Set(ByVal value As KeyName)
            SetValue(KeyProperty, value)
        End Set
    End Property

    Public Shared ReadOnly KeyProperty As DependencyProperty = _
                           DependencyProperty.Register("Key", _
                           GetType(KeyName), GetType(NoteViewModel), _
                           New FrameworkPropertyMetadata(KeyName.Unassigned))


    Public Property Octave As Integer
        Get
            Return GetValue(OctaveProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(OctaveProperty, value)
        End Set
    End Property

    Public Shared ReadOnly OctaveProperty As DependencyProperty = _
                           DependencyProperty.Register("Octave", _
                           GetType(Integer), GetType(NoteViewModel), _
                           New FrameworkPropertyMetadata(4))


    Public Property Parameters As ObservableCollection(Of ParameterViewModel)
        Get
            Return GetValue(ParametersProperty)
        End Get

        Set(ByVal value As ObservableCollection(Of ParameterViewModel))
            SetValue(ParametersProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ParametersProperty As DependencyProperty = _
                           DependencyProperty.Register("Parameters", _
                           GetType(ObservableCollection(Of ParameterViewModel)), GetType(NoteViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public ReadOnly Property KeyNameValues() As IEnumerable(Of KeyName)
        Get
            Return [Enum].GetValues(GetType(KeyName))
        End Get
    End Property
#End Region

#Region "Methods"

    Private Sub ConstructFrom(ByVal data As String)
        Dim rows As String() = data.Split(New [Char]() {CChar(vbCrLf)})
        For Each r As String In rows
            Dim row As String = r.Trim()
            If row.StartsWith("patch_id") Then
                Dim id As String = ReadStringValueFrom(row)
                Dim patches = From p As PatchViewModel In AvailablePatches Where p.Id.Equals(id) Select p
                If patches.Count > 0 Then Me.Patch = patches(0)
            ElseIf row.StartsWith("duration_ppqn") Then
                Me.DurationPpqn = ReadIntegerValueFrom(row)
            ElseIf row.StartsWith("release_point_ppqn") Then
                Me.ReleasePointPpqn = ReadIntegerValueFrom(row)
            ElseIf row.StartsWith("key") Then
                [Enum].TryParse(ReadIntegerValueFrom(row), Me.Key)
            ElseIf row.StartsWith("octave") Then
                Me.Octave = ReadIntegerValueFrom(row)
            ElseIf row.StartsWith("parameter") Then
                Me.Parameters.Add(New ParameterViewModel(Me, row))
            End If
        Next
    End Sub

    Public Overrides Function ToString() As String
        Dim result As String = "note" & vbCrLf
        result &= vbTab & "patch_id = " & Me.Patch.Id & vbCrLf
        result &= vbTab & "duration_ppqn = " & Me.DurationPpqn.ToString() & vbCrLf
        result &= vbTab & "release_point_ppqn = " & Me.ReleasePointPpqn.ToString() & vbCrLf
        result &= vbTab & "key = " & CInt(Me.Key).ToString() & vbCrLf
        result &= vbTab & "octave = " & Me.Octave.ToString() & vbCrLf
        For Each p As ParameterViewModel In Me.Parameters
            result &= p.ToString() & vbCrLf
        Next
        Return result
    End Function

#End Region

End Class
