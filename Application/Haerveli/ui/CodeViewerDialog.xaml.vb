Public Class CodeViewerDialog

    Public Property Code As String
        Get
            Return GetValue(CodeProperty)
        End Get

        Set(ByVal value As String)
            SetValue(CodeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly CodeProperty As DependencyProperty = _
                           DependencyProperty.Register("Code", _
                           GetType(String), GetType(CodeViewerDialog), _
                           New FrameworkPropertyMetadata(String.Empty))


End Class
