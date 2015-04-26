Imports System.IO


Class MainWindow

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture

#If DEBUG Then

        System.Diagnostics.PresentationTraceSources.DataBindingSource.Switch.Level = System.Diagnostics.SourceLevels.Critical

#End If

    End Sub

    Private Const ProductName As String = "Haerveli"

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
                           GetType(ProjectViewModel), GetType(MainWindow), _
                           New FrameworkPropertyMetadata(Nothing))


    Private _engineFilePath As String = String.Empty
    Private _tempCodeFileName As String = String.Empty
    Private _tempSoundFileName As String = String.Empty


    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Initialize()
    End Sub


    Private Sub Initialize()
        CreateNewProject()
        Dim cursorPath As String = My.Application.Info.DirectoryPath & "\Resources\CustomCursor.cur"
        Try
            Me.Cursor = New System.Windows.Input.Cursor(cursorPath)
        Catch ex As Exception
            ' nada
        End Try

        _engineFilePath = My.Application.Info.DirectoryPath & "\FmSynth.exe"
        Trace.WriteLine(_engineFilePath)
        _tempCodeFileName = System.IO.Path.GetTempPath & "hrvl_temp_code.txt"
        Trace.WriteLine(_tempCodeFileName)
        _tempSoundFileName = System.IO.Path.GetTempPath & "hrvl_out.wav"
        Trace.WriteLine(_tempSoundFileName)
    End Sub


    Private Sub UpdateTitle()
        If String.IsNullOrEmpty(Me.Project.FileName) = False Then
            Me.Title = System.IO.Path.GetFileName(Me.Project.FileName) & " - " & ProductName
        Else
            Me.Title = "Untitled - " & ProductName
        End If
    End Sub

    Private Sub CreateNewProject()
        Me.Project = New ProjectViewModel()
        Me.Project.AddNewOperator()
        Me.Project.AddNewPatch()
        Me.Project.SelectedPatch = Me.Project.Patches.First()
        Me.Project.SelectedOperator = Me.Project.Operators.First()
        Me.Project.SelectedPatch.RootOperator = Me.Project.SelectedOperator
        UpdateTitle()
    End Sub

    Private Sub NewButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        CreateNewProject()
    End Sub

    Private Sub OpenButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim folder As String = My.Settings("ProjectFolder")
        If String.IsNullOrEmpty(folder) Then folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        Dim openFileDialog1 As New Forms.OpenFileDialog()
        openFileDialog1.InitialDirectory = folder
        openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        openFileDialog1.FilterIndex = 1
        If openFileDialog1.ShowDialog() = Forms.DialogResult.OK Then
            Try
                Using sr As New StreamReader(openFileDialog1.FileName)
                    Dim data As String
                    data = sr.ReadToEnd()
                    Dim newProject As New ProjectViewModel(data)
                    newProject.FileName = openFileDialog1.FileName
                    Me.Project = newProject
                    My.Settings("ProjectFolder") = System.IO.Path.GetDirectoryName(openFileDialog1.FileName)
                    My.Settings.Save()
                End Using
            Catch Ex As Exception
                MessageBox.Show("Cannot read file from disk. Original error: " & Ex.Message)
            End Try
        End If
        UpdateTitle()
    End Sub


    Private Sub SaveProject()
        Using outfile As New StreamWriter(Me.Project.FileName)
            outfile.Write(Project.ToString())
        End Using
    End Sub


    Private Sub SaveProjectAs()
        Dim folder As String = My.Settings("ProjectFolder")
        If String.IsNullOrEmpty(folder) Then folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        Dim saveFileDialog1 As New Forms.SaveFileDialog()
        saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.InitialDirectory = folder
        If String.IsNullOrEmpty(Me.Project.FileName) = False Then
            saveFileDialog1.FileName = System.IO.Path.GetFileName(Me.Project.FileName)
        Else
            saveFileDialog1.FileName = "New project.txt"
        End If

        If saveFileDialog1.ShowDialog() = Forms.DialogResult.OK Then
            Me.Project.FileName = saveFileDialog1.FileName
            SaveProject()
            My.Settings("ProjectFolder") = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName)
            My.Settings.Save()
        End If
    End Sub


    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Me.Project Is Nothing Then Return
        If System.IO.File.Exists(Me.Project.FileName) Then
            SaveProject()
            MessageBox.Show("Project saved!")
        Else
            SaveProjectAs()
        End If
    End Sub


    Private Sub SaveAsButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        SaveProjectAs()
        UpdateTitle()
    End Sub


    Private Sub ViewCodeButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim dialog As New CodeViewerDialog()
        dialog.Code = Me.Project.ToString()
        dialog.ShowDialog()
    End Sub


    Private Sub FullScreenButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Me.WindowStyle = Windows.WindowStyle.None Then
            Me.WindowStyle = Windows.WindowStyle.SingleBorderWindow
            Me.ResizeMode = Windows.ResizeMode.CanResize
            Me.WindowState = Windows.WindowState.Normal
        Else
            Me.WindowState = Windows.WindowState.Normal
            Me.WindowStyle = Windows.WindowStyle.None
            Me.ResizeMode = Windows.ResizeMode.NoResize
            Me.WindowState = Windows.WindowState.Maximized
        End If
    End Sub

    Private Sub GenerateSound()
        If Project.SelectedSequence.SequenceLength = 0 Then
            MessageBox.Show("Unable to generate sound. Sequence is empty.")
            Return
        End If

        Using outfile As New StreamWriter(_tempCodeFileName)
            outfile.Write(Project.ToString())
        End Using

        Dim proc As New Process()
        proc.StartInfo.FileName = """" & _engineFilePath & """"
        proc.StartInfo.Arguments = """" & _tempCodeFileName & """ """ & _tempSoundFileName & """"

        proc.Start()
        proc.WaitForExit()
        proc.Close()

        My.Computer.Audio.Stop()
        My.Computer.Audio.Play(_tempSoundFileName, AudioPlayMode.BackgroundLoop)
    End Sub

    Private Sub GenerateButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        GenerateSound()
    End Sub

    Private Sub SaveSoundButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If File.Exists(_tempSoundFileName) = False Then
            MessageBox.Show("No sound to save.")
            Return
        End If

        Dim folder As String = My.Settings("SoundFolder")
        If String.IsNullOrEmpty(folder) Then folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        Dim saveFileDialog1 As New Forms.SaveFileDialog()
        saveFileDialog1.Filter = "wav files (*.wav)|*.wav|All files (*.*)|*.*"
        saveFileDialog1.FilterIndex = 1
        saveFileDialog1.RestoreDirectory = True
        saveFileDialog1.InitialDirectory = folder
        If String.IsNullOrEmpty(Me.Project.FileName) = False Then
            saveFileDialog1.FileName = System.IO.Path.GetFileNameWithoutExtension(Me.Project.FileName) & ".wav"
        Else
            saveFileDialog1.FileName = "New sound.wav"
        End If

        If saveFileDialog1.ShowDialog() = Forms.DialogResult.OK Then
            Try
                System.IO.File.Copy(_tempSoundFileName, saveFileDialog1.FileName, True)
                My.Settings("SoundFolder") = System.IO.Path.GetDirectoryName(saveFileDialog1.FileName)
                My.Settings.Save()
            Catch ex As Exception
                MessageBox.Show("Error saving sound: " & vbCrLf & ex.Message)
            End Try
        End If
    End Sub

    Private Sub PlayButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If File.Exists(_tempSoundFileName) = False Then
            MessageBox.Show("No sound to play.")
            Return
        End If
        Try
            My.Computer.Audio.Play(_tempSoundFileName, AudioPlayMode.BackgroundLoop)
        Catch ex As Exception
            MessageBox.Show("An error occurred when playing sound.")
        End Try
    End Sub

    Private Sub StopPlayback()
        If File.Exists(_tempSoundFileName) = False Then
            MessageBox.Show("No sound to stop.")
            Return
        End If
        Try
            My.Computer.Audio.Stop()
        Catch ex As Exception
            MessageBox.Show("An error occurred when stopping sound.")
        End Try
    End Sub

    Private Sub StopButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        StopPlayback()
    End Sub

    Private Sub NewPatchButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Project.AddNewPatch()
    End Sub

    Private Sub DeletePatchButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Project.RemovePatch(Me.Project.SelectedPatch)
    End Sub

    Private Sub NewOperatorButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Project.AddNewOperator()
    End Sub

    Private Sub DeleteOperatorButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Me.Project.RemoveOperator(Me.Project.SelectedOperator)
    End Sub

End Class
