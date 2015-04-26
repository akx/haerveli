Imports System.Collections.ObjectModel

Public Class PatchViewModel
    Inherits ViewModelBase

#Region "Constructors"

    Public Sub New(ByVal project As ProjectViewModel)
        Me.Project = project
        Me.Modulations = New ObservableCollection(Of ModulationViewModel)
        Me.Name = "Untitled Patch"
    End Sub

    Public Sub New(ByVal project As ProjectViewModel, ByVal data As String)
        Me.New(project)
        Me.ConstructFrom(data)
    End Sub

#End Region

#Region "Properties"

    Private _project As ProjectViewModel
    Public Property Project() As ProjectViewModel
        Get
            Return _project
        End Get
        Set(ByVal value As ProjectViewModel)
            _project = value
        End Set
    End Property

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
                           GetType(String), GetType(PatchViewModel), _
                           New FrameworkPropertyMetadata(String.Empty))


    Public Property Name As String
        Get
            Return GetValue(NameProperty)
        End Get

        Set(ByVal value As String)
            SetValue(NameProperty, value)
            NotifyPropertyChanged(Me, "DisplayName")
        End Set
    End Property

    Public Shared ReadOnly NameProperty As DependencyProperty = _
                           DependencyProperty.Register("Name", _
                           GetType(String), GetType(PatchViewModel), _
                           New FrameworkPropertyMetadata(String.Empty))


    Public Property RootOperator As OperatorViewModel
        Get
            Return GetValue(RootOperatorProperty)
        End Get

        Set(ByVal value As OperatorViewModel)
            SetValue(RootOperatorProperty, value)
        End Set
    End Property

    Public Shared ReadOnly RootOperatorProperty As DependencyProperty = _
                           DependencyProperty.Register("RootOperator", _
                           GetType(OperatorViewModel), GetType(PatchViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property Modulations As ObservableCollection(Of ModulationViewModel)
        Get
            Return GetValue(ModulationsProperty)
        End Get

        Set(ByVal value As ObservableCollection(Of ModulationViewModel))
            SetValue(ModulationsProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ModulationsProperty As DependencyProperty = _
                           DependencyProperty.Register("Modulations", _
                           GetType(ObservableCollection(Of ModulationViewModel)), GetType(PatchViewModel), _
                           New FrameworkPropertyMetadata(Nothing))




#End Region

#Region "Methods"

    Private Sub ConstructFrom(ByVal data As String)
        Dim rows As String() = data.Split(New [Char]() {CChar(vbCrLf)})
        For Each r As String In rows
            Dim row As String = r.Trim()
            If row.StartsWith("id") Then
                Me.Id = ReadStringValueFrom(row)
            ElseIf row.StartsWith("name") Then
                Me.Name = ReadStringValueFrom(row)
            ElseIf row.StartsWith("root") Then
                Dim ops = From o As OperatorViewModel In Me.Project.Operators Where o.Id.Equals(ReadStringValueFrom(row)) Select o
                If ops.Count > 0 Then Me.RootOperator = ops(0)
            ElseIf row.StartsWith("modulation") Then
                Me.Modulations.Add(New ModulationViewModel(Me, row))
            End If
        Next
    End Sub

    Public Overrides Function ToString() As String
        Dim result As String = String.Empty
        Dim rootId As String = String.Empty
        If RootOperator IsNot Nothing Then rootId = RootOperator.Id
        result &= "patch" & vbCrLf
        result &= vbTab & "id = " & Id & vbCrLf
        result &= vbTab & "name = " & Name & vbCrLf
        result &= vbTab & "root = " & rootId & vbCrLf
        For Each m As ModulationViewModel In Modulations
            result &= m.ToString() & vbCrLf
        Next
        Return result
    End Function


    Public Sub RemoveModulation(ByVal modulation As ModulationViewModel)
        Modulations.Remove(modulation)
    End Sub

    Public Sub AddNewModulation()
        Modulations.Add(New ModulationViewModel(Me))
    End Sub

#End Region

End Class

