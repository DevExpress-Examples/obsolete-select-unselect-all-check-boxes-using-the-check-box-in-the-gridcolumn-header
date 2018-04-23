Imports System.Collections.Generic
Imports System.Windows

Namespace UnboundCheckColumn
    Partial Public Class Window1
        Inherits Window

        Public Sub New()
            InitializeComponent()
            grid.ItemsSource = TestDataList.Create()
        End Sub
    End Class

    Public Class TestDataList
        Inherits List(Of TestDataItem)

        Public Shared Function Create() As TestDataList
            Dim res As New TestDataList()
            For i As Integer = 0 To 4
                Dim item As New TestDataItem()
                item.ID = i
                item.Value = "A"
                res.Add(item)
            Next i
            For i As Integer = 0 To 4
                Dim item As New TestDataItem()
                item.ID = i
                item.Value = "B"
                res.Add(item)
            Next i
            For i As Integer = 0 To 4
                Dim item As New TestDataItem()
                item.ID = i
                item.Value = "C"
                res.Add(item)
            Next i
            Return res
        End Function
    End Class
    Public Class TestDataItem
        Public Property ID() As Integer
        Public Property Value() As String
    End Class
End Namespace
