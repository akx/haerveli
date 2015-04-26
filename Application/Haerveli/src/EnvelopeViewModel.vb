Imports System.ComponentModel

Public Enum EnvelopePolarity
    <Description("Normal")> _
    Normal = 0
    <Description("Inverted")> _
    Inverted = 1
End Enum

Public Enum EnvelopeType
    <Description("Amplitude")> _
    Amplitude = 0
    <Description("Pitch")> _
    Pitch = 1
End Enum

Public Class EnvelopeViewModel
    Inherits ViewModelBase

#Region "Constructors"

    Public Sub New(ByVal type As EnvelopeType)
        Me.EnvelopeType = type
    End Sub

#End Region

#Region "Properties"

    Public Property EnvelopeType As EnvelopeType
        Get
            Return GetValue(EnvelopeTypeProperty)
        End Get

        Set(ByVal value As EnvelopeType)
            SetValue(EnvelopeTypeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly EnvelopeTypeProperty As DependencyProperty = _
                           DependencyProperty.Register("EnvelopeType", _
                           GetType(EnvelopeType), GetType(EnvelopeViewModel), _
                           New FrameworkPropertyMetadata(EnvelopeType.Amplitude))


    Public Property Attack As Double
        Get
            Return GetValue(AttackProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(AttackProperty, value)
        End Set
    End Property

    Public Shared ReadOnly AttackProperty As DependencyProperty = _
                           DependencyProperty.Register("Attack", _
                           GetType(Double), GetType(EnvelopeViewModel), _
                           New FrameworkPropertyMetadata(0.0))


    Public Property Decay As Double
        Get
            Return GetValue(DecayProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(DecayProperty, value)
        End Set
    End Property

    Public Shared ReadOnly DecayProperty As DependencyProperty = _
                           DependencyProperty.Register("Decay", _
                           GetType(Double), GetType(EnvelopeViewModel), _
                           New FrameworkPropertyMetadata(0.0))


    Public Property Sustain As Double
        Get
            Return GetValue(SustainProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(SustainProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SustainProperty As DependencyProperty = _
                           DependencyProperty.Register("Sustain", _
                           GetType(Double), GetType(EnvelopeViewModel), _
                           New FrameworkPropertyMetadata(1.0))


    Public Property Release As Double
        Get
            Return GetValue(ReleaseProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(ReleaseProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ReleaseProperty As DependencyProperty = _
                           DependencyProperty.Register("Release", _
                           GetType(Double), GetType(EnvelopeViewModel), _
                           New FrameworkPropertyMetadata(0.0))


    Public Property ReleasePoint As Double
        Get
            Return GetValue(ReleasePointProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(ReleasePointProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ReleasePointProperty As DependencyProperty = _
                           DependencyProperty.Register("ReleasePoint", _
                           GetType(Double), GetType(EnvelopeViewModel), _
                           New FrameworkPropertyMetadata(0.5))


    Public Property Polarity As EnvelopePolarity
        Get
            Return GetValue(PolarityProperty)
        End Get

        Set(ByVal value As EnvelopePolarity)
            SetValue(PolarityProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PolarityProperty As DependencyProperty = _
                           DependencyProperty.Register("Polarity", _
                           GetType(EnvelopePolarity), GetType(EnvelopeViewModel), _
                           New FrameworkPropertyMetadata(EnvelopePolarity.Normal))


    Public Property Shape As Double
        Get
            Return GetValue(ShapeProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(ShapeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ShapeProperty As DependencyProperty = _
                           DependencyProperty.Register("Shape", _
                           GetType(Double), GetType(EnvelopeViewModel), _
                           New FrameworkPropertyMetadata(3.0))


    Public Property Amount As Double
        Get
            Return GetValue(AmountProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(AmountProperty, value)
        End Set
    End Property

    Public Shared ReadOnly AmountProperty As DependencyProperty = _
                           DependencyProperty.Register("Amount", _
                           GetType(Double), GetType(EnvelopeViewModel), _
                           New FrameworkPropertyMetadata(1.0))


    Public ReadOnly Property EnvelopePolarityValues() As IEnumerable(Of EnvelopePolarity)
        Get
            Return [Enum].GetValues(GetType(EnvelopePolarity))
        End Get
    End Property


    Public ReadOnly Property EnvelopeTypeValues() As IEnumerable(Of EnvelopeType)
        Get
            Return [Enum].GetValues(GetType(EnvelopeType))
        End Get
    End Property

#End Region

#Region "Methods"

    Public Overrides Function ToString() As String
        Dim result As String = String.Empty
        Dim header As String = String.Empty
        If Me.EnvelopeType = Haerveli.EnvelopeType.Amplitude Then
            header = "amp_"
        Else
            header = "pitch_"
        End If
        result &= vbTab & header & "attack = " & Me.Attack.ToString() & vbCrLf
        result &= vbTab & header & "decay = " & Me.Decay.ToString() & vbCrLf
        result &= vbTab & header & "sustain = " & Me.Sustain.ToString() & vbCrLf
        result &= vbTab & header & "release = " & Me.Release.ToString() & vbCrLf
        result &= vbTab & header & "release_point = " & Me.ReleasePoint.ToString() & vbCrLf
        result &= vbTab & header & "envelope_polarity = " & CType(Me.Polarity, Integer).ToString() & vbCrLf
        result &= vbTab & header & "envelope_shape = " & Me.Shape.ToString() & vbCrLf
        result &= vbTab & header & "envelope_amount = " & Me.Amount.ToString() & vbCrLf
        Return result
    End Function

#End Region

End Class
