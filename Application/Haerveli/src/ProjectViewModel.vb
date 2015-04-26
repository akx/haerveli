Imports System.Collections.ObjectModel

Public Class ProjectViewModel
    Inherits ViewModelBase

#Region "Constructors"

    Public Sub New()
        Me.Patches = New ObservableCollection(Of PatchViewModel)
        Me.Operators = New ObservableCollection(Of OperatorViewModel)
        Me.SelectedSequence = New SequenceViewModel(Me)
    End Sub

    Public Sub New(ByVal projectData As String)
        Me.New()
        ConstructFrom(projectData)
    End Sub

#End Region

#Region "Properties"

    Public Property FileName As String
        Get
            Return GetValue(FileNameProperty)
        End Get

        Set(ByVal value As String)
            SetValue(FileNameProperty, value)
        End Set
    End Property

    Public Shared ReadOnly FileNameProperty As DependencyProperty = _
                           DependencyProperty.Register("FileName", _
                           GetType(String), GetType(ProjectViewModel), _
                           New FrameworkPropertyMetadata(String.Empty))


    Public Property Bpm As Double
        Get
            Return GetValue(BpmProperty)
        End Get

        Set(ByVal value As Double)
            SetValue(BpmProperty, value)
        End Set
    End Property

    Public Shared ReadOnly BpmProperty As DependencyProperty = _
                           DependencyProperty.Register("Bpm", _
                           GetType(Double), GetType(ProjectViewModel), _
                           New FrameworkPropertyMetadata(120.0))


    Public Property Patches As ObservableCollection(Of PatchViewModel)
        Get
            Return GetValue(PatchesProperty)
        End Get

        Set(ByVal value As ObservableCollection(Of PatchViewModel))
            SetValue(PatchesProperty, value)
        End Set
    End Property

    Public Shared ReadOnly PatchesProperty As DependencyProperty = _
                           DependencyProperty.Register("Patches", _
                           GetType(ObservableCollection(Of PatchViewModel)), GetType(ProjectViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property Operators As ObservableCollection(Of OperatorViewModel)
        Get
            Return GetValue(OperatorsProperty)
        End Get

        Set(ByVal value As ObservableCollection(Of OperatorViewModel))
            SetValue(OperatorsProperty, value)
        End Set
    End Property

    Public Shared ReadOnly OperatorsProperty As DependencyProperty = _
                           DependencyProperty.Register("Operators", _
                           GetType(ObservableCollection(Of OperatorViewModel)), GetType(ProjectViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property SelectedPatch As PatchViewModel
        Get
            Return GetValue(SelectedPatchProperty)
        End Get

        Set(ByVal value As PatchViewModel)
            SetValue(SelectedPatchProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SelectedPatchProperty As DependencyProperty = _
                           DependencyProperty.Register("SelectedPatch", _
                           GetType(PatchViewModel), GetType(ProjectViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property SelectedOperator As OperatorViewModel
        Get
            Return GetValue(SelectedOperatorProperty)
        End Get

        Set(ByVal value As OperatorViewModel)
            SetValue(SelectedOperatorProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SelectedOperatorProperty As DependencyProperty = _
                           DependencyProperty.Register("SelectedOperator", _
                           GetType(OperatorViewModel), GetType(ProjectViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


    Public Property SelectedSequence As SequenceViewModel
        Get
            Return GetValue(SelectedSequenceProperty)
        End Get

        Set(ByVal value As SequenceViewModel)
            SetValue(SelectedSequenceProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SelectedSequenceProperty As DependencyProperty = _
                           DependencyProperty.Register("SelectedSequence", _
                           GetType(SequenceViewModel), GetType(ProjectViewModel), _
                           New FrameworkPropertyMetadata(Nothing))


#End Region

#Region "Methods"

    Private Sub ConstructFrom(ByVal data As String)

        Dim settingsData, patchData, operatorData, sequenceData As String

        settingsData = data.Substring(0, (data.IndexOf("patch") + 1))
        patchData = data.Substring(data.IndexOf("patch"), (data.IndexOf("operator") - data.IndexOf("patch")))
        operatorData = data.Substring(data.IndexOf("operator"), (data.IndexOf("note") - data.IndexOf("operator")))
        sequenceData = data.Substring(data.IndexOf("note"))

        Dim patches As String() = patchData.Split(New String() {"patch" & vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        Dim operators As String() = operatorData.Split(New String() {"operator" & vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        Dim notes As String() = sequenceData.Split(New String() {"note" & vbCrLf}, StringSplitOptions.RemoveEmptyEntries)

        ' Settings
        Me.ReadSettingsFrom(settingsData)

        ' Operators
        For Each od As String In operators
            Me.Operators.Add(New OperatorViewModel(od))
        Next

        ' Patches
        For Each pd As String In patches
            Me.Patches.Add(New PatchViewModel(Me, pd))
        Next

        ' Sequence
        Me.SelectedSequence = New SequenceViewModel(Me, sequenceData)

        If Me.Patches.Count() > 0 Then Me.SelectedPatch = Me.Patches.First()

        If Me.Operators.Count() > 0 Then Me.SelectedOperator = Me.Operators.First()
    End Sub


    Private Sub ReadSettingsFrom(ByVal data As String)
        Dim rows As String() = data.Split(New [Char]() {CChar(vbCrLf)})
        For Each r As String In rows
            Dim row As String = r.Trim()
            If row.StartsWith("bpm") Then
                Me.Bpm = ReadDoubleValueFrom(row)
            End If
        Next
    End Sub


    Public Overrides Function ToString() As String
        Dim result As String = String.Empty

        ' Settings
        result &= "settings" & vbCrLf
        result &= vbTab & "bpm = " & Me.Bpm & vbCrLf & vbCrLf

        ' Patches
        For Each p As PatchViewModel In Me.Patches
            result &= p.ToString() & vbCrLf
        Next

        ' Operators
        For Each o As OperatorViewModel In Me.Operators
            result &= o.ToString() & vbCrLf
        Next

        ' Sequence
        result &= Me.SelectedSequence.ToString() & vbCrLf

        result &= "end" & vbCrLf

        Return result
    End Function


    Public Sub AddNewOperator()
        Dim idHeader As String = "op"
        Dim idNumber As Integer = Operators.Count + 1
        Dim id As String = idHeader & idNumber

        Dim isUnique As Boolean = False
        While Not isUnique
            Dim ids = From o As OperatorViewModel In Me.Operators Where o.Id.Equals(id) Select o
            If ids.Count = 0 Then
                isUnique = True
            Else
                idNumber += 1
                id = idHeader & idNumber
            End If
        End While

        Dim op As New OperatorViewModel()
        op.Id = id
        op.Name = "Operator " & idNumber
        Me.Operators.Add(op)
        Me.SelectedOperator = op
    End Sub


    Public Sub AddNewPatch()
        Dim idHeader As String = "patch"
        Dim idNumber As Integer = Patches.Count + 1
        Dim id As String = idHeader & idNumber

        Dim isUnique As Boolean = False
        While Not isUnique
            Dim ids = From p As PatchViewModel In Me.Patches Where p.Id.Equals(id) Select p
            If ids.Count = 0 Then
                isUnique = True
            Else
                idNumber += 1
                id = idHeader & idNumber
            End If
        End While

        Dim patch As New PatchViewModel(Me)
        patch.Id = id
        patch.Name = "Patch " & idNumber
        Me.Patches.Add(patch)
        Me.SelectedPatch = patch
    End Sub


    Public Sub RemoveOperator(ByVal value As OperatorViewModel)
        Me.Operators.Remove(value)
        If Me.Operators.Count > 0 Then Me.SelectedOperator = Me.Operators.First
    End Sub


    Public Sub RemovePatch(ByVal value As PatchViewModel)
        Me.Patches.Remove(value)
        If Me.Patches.Count > 0 Then Me.SelectedPatch = Me.Patches.First
    End Sub

#End Region

End Class
