Imports System.ComponentModel

Public Enum ModulationType
    <Description("Frequency Mod")> _
    FM = 0
    <Description("Phase Mod")> _
    PM = 1
    <Description("Amplitude Mod")> _
    AM = 2
    <Description("Additive")> _
    Additive = 3
    <Description("Ring Mod")> _
    RM = 4
End Enum


Public Class OperatorViewModel
    Inherits ViewModelBase

#Region "Constructors"

    Public Sub New()
        Me.AmpEnvelope = New EnvelopeViewModel(EnvelopeType.Amplitude)
        Me.PitchEnvelope = New EnvelopeViewModel(EnvelopeType.Pitch)
        Me.PitchEnvelope.Amount = 0.0
        Me.Name = "Untitled Operator"
    End Sub

    Public Sub New(ByVal data As String)
        Me.New()
        Me.ConstructFrom(data)
    End Sub

#End Region

#Region "Properties"

    Public Property Id As String
        Get
            Return GetValue(IdProperty)
        End Get

        Set(ByVal value As String)
            SetValue(IdProperty, value)
        End Set
    End Property

    Public Shared ReadOnly IdProperty As DependencyProperty = _
                               DependencyProperty.Register("Id", _
                               GetType(String), GetType(OperatorViewModel), _
                               New FrameworkPropertyMetadata(String.Empty))



    Public Property Name As String
        Get
            Return GetValue(NameProperty)
        End Get

        Set(ByVal value As String)
            SetValue(NameProperty, value)
        End Set
    End Property

    Public Shared ReadOnly NameProperty As DependencyProperty = _
                           DependencyProperty.Register("Name", _
                           GetType(String), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(String.Empty))



    Public Property KeyFollow As Boolean
        Get
            Return GetValue(KeyFollowProperty)
        End Get

        Set(ByVal value As Boolean)
            SetValue(KeyFollowProperty, value)
        End Set
    End Property

    Public Shared ReadOnly KeyFollowProperty As DependencyProperty = _
                           DependencyProperty.Register("KeyFollow", _
                           GetType(Boolean), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(True))


    Public Property OscillatorLevel As Double
        Get
            Return GetValue(OscillatorLevelProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(OscillatorLevelProperty, value)
        End Set
    End Property

    Public Shared ReadOnly OscillatorLevelProperty As DependencyProperty = _
                           DependencyProperty.Register("OscillatorLevel", _
                           GetType(Double), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(1.0))


    Public Property OscillatorFrequency As Double
        Get
            Return GetValue(OscillatorFrequencyProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(OscillatorFrequencyProperty, value)
        End Set
    End Property

    Public Shared ReadOnly OscillatorFrequencyProperty As DependencyProperty = _
                           DependencyProperty.Register("OscillatorFrequency", _
                           GetType(Double), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(440.0))


    Public Property OscillatorPhase As Double
        Get
            Return GetValue(OscillatorPhaseProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(OscillatorPhaseProperty, value)
        End Set
    End Property

    Public Shared ReadOnly OscillatorPhaseProperty As DependencyProperty = _
                           DependencyProperty.Register("OscillatorPhase", _
                           GetType(Double), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(0.0))


    Public Property KeyOffset As Integer
        Get
            Return GetValue(KeyOffsetProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(KeyOffsetProperty, value)
        End Set
    End Property

    Public Shared ReadOnly KeyOffsetProperty As DependencyProperty = _
                           DependencyProperty.Register("KeyOffset", _
                           GetType(Integer), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(0))


    Public Property OctaveOffset As Integer
        Get
            Return GetValue(OctaveOffsetProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(OctaveOffsetProperty, value)
        End Set
    End Property

    Public Shared ReadOnly OctaveOffsetProperty As DependencyProperty = _
                           DependencyProperty.Register("OctaveOffset", _
                           GetType(Integer), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(0))


    Public Property ModulatorType As ModulationType
        Get
            Return GetValue(ModulatorTypeProperty)
        End Get

        Set(ByVal value As ModulationType)
            SetValue(ModulatorTypeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ModulatorTypeProperty As DependencyProperty = _
                           DependencyProperty.Register("ModulatorType", _
                           GetType(ModulationType), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(ModulationType.FM))


    Public Property AmpEnvelope As EnvelopeViewModel
        Get
            Return GetValue(AmpEnvelopeProperty)
        End Get

        Set(ByVal value As EnvelopeViewModel)
            SetValue(AmpEnvelopeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly AmpEnvelopeProperty As DependencyProperty = _
                           DependencyProperty.Register("AmpEnvelope", _
                           GetType(EnvelopeViewModel), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property PitchEnvelope As EnvelopeViewModel
        Get
            Return GetValue(PitchEnvelopeProperty)
        End Get

        Set(ByVal value As EnvelopeViewModel)
            SetValue(PitchEnvelopeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PitchEnvelopeProperty As DependencyProperty = _
                           DependencyProperty.Register("PitchEnvelope", _
                           GetType(EnvelopeViewModel), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property Sharpness As Double
        Get
            Return GetValue(SharpnessProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(SharpnessProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SharpnessProperty As DependencyProperty = _
                           DependencyProperty.Register("Sharpness", _
                           GetType(Double), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(0.0))


    Public Property NoiseLevel As Double
        Get
            Return GetValue(NoiseLevelProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(NoiseLevelProperty, value)
        End Set
    End Property

    Public Shared ReadOnly NoiseLevelProperty As DependencyProperty = _
                           DependencyProperty.Register("NoiseLevel", _
                           GetType(Double), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(0.0))


    Public Property Resolution As Integer
        Get
            Return GetValue(ResolutionProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(ResolutionProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ResolutionProperty As DependencyProperty = _
                           DependencyProperty.Register("Resolution", _
                           GetType(Integer), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(0))


    Public Property Distortion As Double
        Get
            Return GetValue(DistortionProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(DistortionProperty, value)
        End Set
    End Property

    Public Shared ReadOnly DistortionProperty As DependencyProperty = _
                           DependencyProperty.Register("Distortion", _
                           GetType(Double), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(1.0))


    Public Property PanningFrequency As Double
        Get
            Return GetValue(PanningFrequencyProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(PanningFrequencyProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PanningFrequencyProperty As DependencyProperty = _
                           DependencyProperty.Register("PanningFrequency", _
                           GetType(Double), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(0.0))


    Public Property PanningLevel As Double
        Get
            Return GetValue(PanningLevelProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(PanningLevelProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PanningLevelProperty As DependencyProperty = _
                           DependencyProperty.Register("PanningLevel", _
                           GetType(Double), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(0.0))


    Public Property PanningOffset As Double
        Get
            Return GetValue(PanningOffsetProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(PanningOffsetProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PanningOffsetProperty As DependencyProperty = _
                           DependencyProperty.Register("PanningOffset", _
                           GetType(Double), GetType(OperatorViewModel), _
                           New FrameworkPropertyMetadata(0.0))


    Public ReadOnly Property ModulationTypeValues() As IEnumerable(Of ModulationType)
        Get
            Return [Enum].GetValues(GetType(ModulationType))
        End Get
    End Property

#End Region

#Region "Methods"

    Private Sub ConstructFrom(ByVal data As String)
        Me.Name = String.Empty

        Dim rows As String() = data.Split(New [Char]() {CChar(vbCrLf)})
        For Each r As String In rows
            Dim row As String = r.Trim()

            If row.StartsWith("id") Then
                Me.Id = ReadStringValueFrom(row)

            ElseIf row.StartsWith("name") Then
                Me.Name = ReadStringValueFrom(row)

            ElseIf row.StartsWith("key_follow") Then
                Me.KeyFollow = Convert.ToBoolean(ReadIntegerValueFrom(row))

            ElseIf row.StartsWith("oscillator_level") Then
                Me.OscillatorLevel = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("oscillator_frequency") Then
                Me.OscillatorFrequency = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("oscillator_phase") Then
                Me.OscillatorPhase = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("key_offset") Then
                Me.KeyOffset = ReadIntegerValueFrom(row)

            ElseIf row.StartsWith("octave_offset") Then
                Me.OctaveOffset = ReadIntegerValueFrom(row)

            ElseIf row.StartsWith("modulator_type") Then
                [Enum].TryParse(ReadIntegerValueFrom(row), Me.ModulatorType)

            ElseIf row.StartsWith("amp_attack") Then
                Me.AmpEnvelope.Attack = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("amp_decay") Then
                Me.AmpEnvelope.Decay = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("amp_sustain") Then
                Me.AmpEnvelope.Sustain = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("amp_release ") Then
                Me.AmpEnvelope.Release = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("amp_release_point") Then
                Me.AmpEnvelope.ReleasePoint = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("amp_envelope_amount") Then
                Me.AmpEnvelope.Amount = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("amp_envelope_polarity") Then
                [Enum].TryParse(ReadIntegerValueFrom(row), Me.AmpEnvelope.Polarity)

            ElseIf row.StartsWith("amp_envelope_shape") Then
                Me.AmpEnvelope.Shape = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("pitch_attack") Then
                Me.PitchEnvelope.Attack = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("pitch_decay") Then
                Me.PitchEnvelope.Decay = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("pitch_sustain") Then
                Me.PitchEnvelope.Sustain = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("pitch_release ") Then
                Me.PitchEnvelope.Release = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("pitch_release_point") Then
                Me.PitchEnvelope.ReleasePoint = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("pitch_envelope_amount") Then
                Me.PitchEnvelope.Amount = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("pitch_envelope_polarity") Then
                [Enum].TryParse(ReadIntegerValueFrom(row), Me.PitchEnvelope.Polarity)

            ElseIf row.StartsWith("pitch_envelope_shape") Then
                Me.PitchEnvelope.Shape = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("sharpness") Then
                Me.Sharpness = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("noise_level") Then
                Me.NoiseLevel = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("distortion") Then
                Me.Distortion = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("resolution") Then
                Me.Resolution = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("panning_frequency") Then
                Me.PanningFrequency = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("panning_level") Then
                Me.PanningLevel = ReadDoubleValueFrom(row)

            ElseIf row.StartsWith("panning_offset") Then
                Me.PanningOffset = ReadDoubleValueFrom(row)

            End If
        Next

        If String.IsNullOrEmpty(Me.Name) Then Me.Name = Me.Id

    End Sub

    Public Overrides Function ToString() As String
        Dim result As String = String.Empty
        result &= "operator" & vbCrLf
        result &= vbTab & "id = " & Me.Id & vbCrLf
        result &= vbTab & "name = " & Me.Name & vbCrLf
        result &= vbTab & "key_follow = " & Convert.ToInt32(Me.KeyFollow) & vbCrLf
        result &= vbTab & "oscillator_level = " & Me.OscillatorLevel.ToString() & vbCrLf
        result &= vbTab & "oscillator_frequency = " & Me.OscillatorFrequency.ToString() & vbCrLf
        result &= vbTab & "oscillator_phase = " & Me.OscillatorPhase.ToString() & vbCrLf
        result &= vbTab & "key_offset = " & Me.KeyOffset.ToString() & vbCrLf
        result &= vbTab & "octave_offset = " & Me.OctaveOffset.ToString() & vbCrLf
        result &= vbTab & "modulator_type = " & CType(Me.ModulatorType, Integer).ToString & vbCrLf
        result &= Me.AmpEnvelope.ToString()
        result &= Me.PitchEnvelope.ToString()
        result &= vbTab & "sharpness = " & Me.Sharpness.ToString() & vbCrLf
        result &= vbTab & "noise_level = " & Me.NoiseLevel.ToString() & vbCrLf
        result &= vbTab & "distortion = " & Me.Distortion.ToString() & vbCrLf
        result &= vbTab & "resolution = " & Me.Resolution.ToString() & vbCrLf
        result &= vbTab & "panning_frequency = " & Me.PanningFrequency.ToString() & vbCrLf
        result &= vbTab & "panning_level = " & Me.PanningLevel.ToString() & vbCrLf
        result &= vbTab & "panning_offset = " & Me.PanningOffset.ToString() & vbCrLf
        Return result
    End Function

#End Region

End Class
