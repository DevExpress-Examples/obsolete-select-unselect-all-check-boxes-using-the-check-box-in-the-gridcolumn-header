Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows

Namespace UnboundCheckColumn
	''' <summary>
	''' Interaction logic for Window1.xaml
	''' </summary>
	Partial Public Class Window1
		Inherits Window
		Public Sub New()
			InitializeComponent()
			grid.ItemsSource = TestDataList.Create()
		End Sub

		Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			view.AllowSelectionSynchronize = Not view.AllowSelectionSynchronize
		End Sub
	End Class

	Public Class TestDataList
		Inherits List(Of TestDataItem)
		Public Shared Function Create() As TestDataList
			Dim res As New TestDataList()
			For i As Integer = 0 To 999
				Dim item As New TestDataItem()
				item.ID = i
				item.Value = "A"
				res.Add(item)
			Next i
			For i As Integer = 0 To 999
				Dim item As New TestDataItem()
				item.ID = i
				item.Value = "B"
				res.Add(item)
			Next i
			Return res
		End Function
	End Class
	Public Class TestDataItem
		Private privateID As Integer
		Public Property ID() As Integer
			Get
				Return privateID
			End Get
			Set(ByVal value As Integer)
				privateID = value
			End Set
		End Property
		Private privateValue As String
		Public Property Value() As String
			Get
				Return privateValue
			End Get
			Set(ByVal value As String)
				privateValue = value
			End Set
		End Property
	End Class
End Namespace
