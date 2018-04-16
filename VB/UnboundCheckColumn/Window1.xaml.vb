' Developer Express Code Central Example:
' How to implement an unbound editable check column
' 
' This example demonstrates how to emulate row selection via an unbound column
' with a checkbox. The grid's built-in editing is used. It's enabled when the
' view's NavigationStyle property is set to CellNavigation. Note: If you want this
' feature to be natively supported by the DXGrid control, please track the
' corresponding suggestion: .
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E1263


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Windows.Shapes
Imports DevExpress.Xpf.Grid
Imports System.Windows.Interactivity
Imports DevExpress.Data
Imports DevExpress.Utils

Namespace UnboundCheckColumn
	''' <summary>
	''' Interaction logic for Window1.xaml
	''' </summary>
	Partial Public Class Window1
		Inherits Window
		Public Sub New()
			InitializeComponent()
			grid.ItemsSource = TestDataList.Create()
			grid2.ItemsSource = TestDataList.Create()
		End Sub
	End Class

	Public Class TestDataList
		Inherits List(Of TestDataItem)
		Public Shared Function Create() As TestDataList
			Dim res As New TestDataList()
			For i As Integer = 0 To 4999
				Dim item As New TestDataItem()
				item.ID = i
				item.Value = "A"
				res.Add(item)
			Next i
			For i As Integer = 0 To 4999
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
