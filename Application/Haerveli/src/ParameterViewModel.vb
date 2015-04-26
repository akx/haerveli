Imports System.ComponentModel
Imports System.Collections.ObjectModel

Public Enum ParameterName
    <Description("Mod. Type")> _
    oscillator_mod_type
    <Description("Osc. Level")> _
    oscillator_level
    <Description("Osc. Phase")> _
    oscillator_phase
    <Description("Key Follow")> _
    oscillator_follow
    <Description("Osc. Freq.")> _
    oscillator_frequency
    <Description("Key Offset")> _
    oscillator_offset_k
    <Description("Oct. Offset")> _
    oscillator_offset_o
    <Description("Amp: Polarity")> _
    amp_envelope_polarity
    <Description("Amp: Amount")> _
    amp_envelope_amount
    <Description("Amp: Attack")> _
    amp_attack
    <Description("Amp: Decay")> _
    amp_decay
    <Description("Amp: Sustain")> _
    amp_sustain
    <Description("Amp: Release")> _
    amp_release
    <Description("Amp: Shape")> _
    amp_envelope_shape
    <Description("Pitch: Polarity")> _
    pitch_envelope_polarity
    <Description("Pitch: Amount")> _
    pitch_envelope_amount
    <Description("Pitch: Attack")> _
    pitch_attack
    <Description("Pitch: Decay")> _
    pitch_decay
    <Description("Pitch: Sustain")> _
    pitch_sustain
    <Description("Pitch: Release")> _
    pitch_release
    <Description("Pitch: Shape")> _
    pitch_envelope_shape
    <Description("Pan: Offset")> _
    panning_offset
    <Description("Pan: Osc. Freq.")> _
    panning_frequency
    <Description("Pan: Osc. Level")> _
    panning_level
    <Description("FX: Sharpness")> _
    sharpness
    <Description("FX: Noise")> _
    noise_level
    <Description("FX: Shape")> _
    distortion
    <Description("FX: Resolution")> _
    resolution
End Enum

Public Class ParameterViewModel
    Inherits ViewModelBase

#Region "Constructors"

    Public Sub New(ByVal note As NoteViewModel)
        _note = note
    End Sub

    Public Sub New(ByVal note As NoteViewModel, ByVal data As String)
        Me.New(note)
        Me.ConstructFrom(data)
    End Sub

#End Region

#Region "Properties"

    Private _note As NoteViewModel
    Public ReadOnly Property Note() As NoteViewModel
        Get
            Return _note
        End Get
    End Property


    Public ReadOnly Property AvailableOperators() As ObservableCollection(Of OperatorViewModel)
        Get
            Return Me.Note.Project.Operators
        End Get
    End Property


    Public Property Target As OperatorViewModel
        Get
            Return GetValue(TargetProperty)
        End Get

        Set(ByVal value As OperatorViewModel)
            SetValue(TargetProperty, value)
        End Set
    End Property

    Public Shared ReadOnly TargetProperty As DependencyProperty = _
                           DependencyProperty.Register("Target", _
                           GetType(OperatorViewModel), GetType(ParameterViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property ParameterId As ParameterName
        Get
            Return GetValue(ParameterIdProperty)
        End Get

        Set(ByVal value As ParameterName)
            SetValue(ParameterIdProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ParameterIdProperty As DependencyProperty = _
                           DependencyProperty.Register("ParameterId", _
                           GetType(ParameterName), GetType(ParameterViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property Value As Double
        Get
            Return GetValue(ValueProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(ValueProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ValueProperty As DependencyProperty = _
                           DependencyProperty.Register("Value", _
                           GetType(Double), GetType(ParameterViewModel), _
                           New FrameworkPropertyMetadata(0.0))


    Public ReadOnly Property ParameterNameValues() As IEnumerable(Of ParameterName)
        Get
            Return [Enum].GetValues(GetType(ParameterName))
        End Get
    End Property

#End Region

#Region "Methods"

    Private Sub ConstructFrom(ByVal data As String)
        Dim md As String() = data.Split(New String() {":", "->", "="}, StringSplitOptions.RemoveEmptyEntries)
        If md.Length <> 4 Then Return
        Dim targetId As String = md(1).Trim()
        Dim ops = From op As OperatorViewModel In Me.AvailableOperators Where op.Id.Equals(targetId) Select op
        If ops.Count > 0 Then Me.Target = ops(0)
        [Enum].TryParse(md(2).Trim(), Me.ParameterId)
        Double.TryParse(md(3).Trim(), Me.Value)
    End Sub

    Public Overrides Function ToString() As String
        Dim result As String = vbTab & "parameter : " & Target.Id & " -> " & ParameterId.ToString() & " = " & Value.ToString()
        Return result
    End Function

#End Region

End Class
