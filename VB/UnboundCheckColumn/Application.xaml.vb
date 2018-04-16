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
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Windows

Namespace UnboundCheckColumn
	''' <summary>
	''' Interaction logic for App.xaml
	''' </summary>
	Partial Public Class App
		Inherits Application
	End Class
End Namespace
