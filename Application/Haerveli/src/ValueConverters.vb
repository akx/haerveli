Imports System.ComponentModel

Public Class EnumDescriptionConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        If value Is Nothing Then Return Binding.DoNothing
        Dim desc As String = String.Empty
        Try
            Dim mi As System.Reflection.MemberInfo() = value.GetType().GetMember(value.ToString())
            If mi.Length > 0 Then
                desc = mi(0).GetCustomAttributes(True).OfType(Of DescriptionAttribute)().First.Description
            End If
        Catch ex As Exception
            Return Binding.DoNothing
        End Try
        Return desc
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Return Binding.DoNothing
    End Function
End Class


Public Class BooleanInverterConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        If Not TypeOf value Is Boolean Then Return Binding.DoNothing
        Return Not CBool(value)
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Return Binding.DoNothing
    End Function
End Class