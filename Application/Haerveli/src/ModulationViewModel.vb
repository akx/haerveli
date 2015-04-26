Imports System.Collections.ObjectModel

Public Class ModulationViewModel
    Inherits ViewModelBase

#Region "Constructors"

    Public Sub New(ByVal patch As PatchViewModel)
        _patch = patch
    End Sub

    Public Sub New(ByVal patch As PatchViewModel, ByVal data As String)
        Me.New(patch)
        Me.ConstructFrom(data)
    End Sub

#End Region

#Region "Properties"

    Private _patch As PatchViewModel
    Public ReadOnly Property Patch() As PatchViewModel
        Get
            Return _patch
        End Get
    End Property


    Public ReadOnly Property AvailableOperators() As ObservableCollection(Of OperatorViewModel)
        Get
            Return _patch.Project.Operators
        End Get
    End Property


    Public Property Source As OperatorViewModel
        Get
            Return GetValue(SourceProperty)
        End Get

        Set(ByVal value As OperatorViewModel)
            SetValue(SourceProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SourceProperty As DependencyProperty = _
                           DependencyProperty.Register("Source", _
                           GetType(OperatorViewModel), GetType(ModulationViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


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
                           GetType(OperatorViewModel), GetType(ModulationViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


#End Region

#Region "Methods"

    Private Sub ConstructFrom(ByVal data As String)
        Dim md As String() = data.Split(New String() {":", "+="}, StringSplitOptions.RemoveEmptyEntries)
        If md.Length <> 3 Then Return

        Dim targetId As String = md(1).Trim()
        Dim sourceId As String = md(2).Trim()

        Dim targetOp = From o As OperatorViewModel In Me.AvailableOperators Where o.Id.Equals(targetId) Select o
        Dim sourceOp = From o As OperatorViewModel In Me.AvailableOperators Where o.Id.Equals(sourceId) Select o

        If targetOp.Count > 0 Then Me.Target = targetOp(0)
        If sourceOp.Count > 0 Then Me.Source = sourceOp(0)
    End Sub

    Public Overrides Function ToString() As String
        Dim targetId As String = String.Empty
        Dim sourceId As String = String.Empty
        If Target IsNot Nothing Then targetId = Target.Id
        If Source IsNot Nothing Then sourceId = Source.Id
        Dim result As String = vbTab & "modulation : " & targetId & " += " & sourceId
        Return result
    End Function

    Public Sub Remove()
        _patch.RemoveModulation(Me)
        _patch = Nothing
    End Sub

#End Region

End Class
