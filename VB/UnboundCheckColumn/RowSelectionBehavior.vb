Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Grid
Imports DevExpress.Data
Imports DevExpress.Utils
Imports System.Windows.Interactivity
Imports System.Windows
Imports System.Windows.Markup
Imports System
Imports System.Collections.Generic
Namespace UnboundCheckColumn
	Public Class SelectAllColumn
		Inherits GridColumn
		Public Const CheckHeaderTemplate As String = "" & ControlChars.CrLf & "         <DataTemplate " & ControlChars.CrLf & "xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""" & ControlChars.CrLf & "xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""" & ControlChars.CrLf & "xmlns:dxe=""http://schemas.devexpress.com/winfx/2008/xaml/editors""" & ControlChars.CrLf & "xmlns:dxg=""http://schemas.devexpress.com/winfx/2008/xaml/grid""" & ControlChars.CrLf & ">" & ControlChars.CrLf & "            <dxe:CheckEdit IsChecked=""{Binding " & ControlChars.CrLf & "Path=DataContext.RowSelectionBehavior.IsAllRowsSelected, Mode=TwoWay, " & ControlChars.CrLf & "RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type dxg:GridColumnHeader}}}"" />" & ControlChars.CrLf & "        </DataTemplate>" & ControlChars.CrLf & ""
		Public Const CheckCellTemplate As String = "" & ControlChars.CrLf & "                        <DataTemplate" & ControlChars.CrLf & "xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""" & ControlChars.CrLf & "xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""" & ControlChars.CrLf & "xmlns:dxe=""http://schemas.devexpress.com/winfx/2008/xaml/editors""" & ControlChars.CrLf & "xmlns:dxg=""http://schemas.devexpress.com/winfx/2008/xaml/grid""" & ControlChars.CrLf & ">" & ControlChars.CrLf & "                            <CheckBox IsChecked=""{Binding Data.Selected}""" & ControlChars.CrLf & "                                      HorizontalAlignment=""Center""" & ControlChars.CrLf & "                                      VerticalAlignment=""Center"" />" & ControlChars.CrLf & "                        </DataTemplate>" & ControlChars.CrLf & ""

		Public Shared ReadOnly RowSelectionBehaviorProperty As DependencyProperty = DependencyProperty.Register("RowSelectionBehavior", GetType(RowSelectionBehavior), GetType(SelectAllColumn), New PropertyMetadata(Nothing))
		Public Property RowSelectionBehavior() As RowSelectionBehavior
			Get
				Return CType(GetValue(RowSelectionBehaviorProperty), RowSelectionBehavior)
			End Get
			Set(ByVal value As RowSelectionBehavior)
				SetValue(RowSelectionBehaviorProperty, value)
			End Set
		End Property
		Public ReadOnly Property GridView() As TableView
			Get
				Return CType(View, TableView)
			End Get
		End Property
		Public ReadOnly Property Grid() As GridControl
			Get
				Return GridView.Grid
			End Get
		End Property
		Public Sub New()
			FieldName = "Selected"
			Width = 20
			UnboundType = UnboundColumnType.Boolean
			AllowSorting = DefaultBoolean.False
			AllowAutoFilter = False
			AllowBestFit = DefaultBoolean.False
			AllowColumnFiltering = DefaultBoolean.False
			AllowGrouping = DefaultBoolean.False
			HeaderTemplate = CType(XamlReader.Parse(SelectAllColumn.CheckHeaderTemplate), DataTemplate)
			CellTemplate = CType(XamlReader.Parse(SelectAllColumn.CheckCellTemplate), DataTemplate)
		End Sub
		Protected Overrides Sub OnOwnerChanged()
			MyBase.OnOwnerChanged()
			If Grid Is Nothing Then
				Return
			End If
            For Each b As Behavior In System.Windows.Interactivity.Interaction.GetBehaviors(Grid)
                If TypeOf b Is RowSelectionBehavior Then
                    RowSelectionBehavior = CType(b, RowSelectionBehavior)
                    Exit For
                End If
            Next b
			If RowSelectionBehavior IsNot Nothing Then
				FieldName = RowSelectionBehavior.CheckUnboundColumnFieldName
			End If
		End Sub
	End Class
	Public Class RowSelectionBehavior
		Inherits Behavior(Of GridControl)
        Public Shared ReadOnly IsAllRowsSelectedProperty As DependencyProperty = DependencyProperty.Register("IsAllRowsSelected", GetType(Boolean?), GetType(RowSelectionBehavior), New PropertyMetadata(False, Function(d, e) (CType(d, RowSelectionBehavior)).OnIsAllRowsSelectedChanged()))
		Public Shared ReadOnly CheckUnboundColumnFieldNameProperty As DependencyProperty = DependencyProperty.Register("CheckUnboundColumnFieldName", GetType(String), GetType(RowSelectionBehavior), New PropertyMetadata("Selected"))
		Public Property IsAllRowsSelected() As Boolean?
			Get
				Return CType(GetValue(IsAllRowsSelectedProperty), Boolean?)
			End Get
			Set(ByVal value? As Boolean)
				SetValue(IsAllRowsSelectedProperty, value)
			End Set
		End Property
		Public Property CheckUnboundColumnFieldName() As String
			Get
				Return CStr(GetValue(CheckUnboundColumnFieldNameProperty))
			End Get
			Set(ByVal value As String)
				SetValue(CheckUnboundColumnFieldNameProperty, value)
			End Set
		End Property

        Private Function OnIsAllRowsSelectedChanged() As Boolean
            If IsAllRowsSelected Is Nothing Then
                Return True
            End If
            lockUpdates = True
            SetIsSelectedForAllRows(IsAllRowsSelected.Value)
            AssociatedObject.RefreshData()
            lockUpdates = False
            Return True
        End Function
        Private Sub SetIsSelectedForAllRows(ByVal value As Boolean)
            For i As Integer = 0 To AssociatedObject.VisibleRowCount - 1
                Dim handle As Integer = AssociatedObject.GetRowHandleByVisibleIndex(i)
                If AssociatedObject.IsGroupRowHandle(handle) Then
                    Continue For
                End If
                AssociatedObject.SetCellValue(handle, CheckUnboundColumnFieldName, value)
            Next i
        End Sub
        Private lockUpdates As Boolean = False
        Public Sub New()
            selectedValues = New Dictionary(Of Object, Boolean)()
        End Sub
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.CustomUnboundColumnData, AddressOf AssociatedObject_CustomUnboundColumnData
        End Sub
        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.CustomUnboundColumnData, AddressOf AssociatedObject_CustomUnboundColumnData
        End Sub
        Private selectedValues As Dictionary(Of Object, Boolean)
        Private Sub AssociatedObject_CustomUnboundColumnData(ByVal sender As Object, ByVal e As GridColumnDataEventArgs)
            If e.Column.FieldName = CheckUnboundColumnFieldName Then
                Dim key As Object = AssociatedObject.GetRowHandleByListIndex(e.ListSourceRowIndex)
                If e.IsGetData Then
                    e.Value = GetIsSelected(key)
                End If
                If e.IsSetData Then
                    SetIsSelected(key, CBool(e.Value))
                End If
            End If
        End Sub
        Private Function GetIsSelected(ByVal key As Object) As Boolean
            Dim isSelected As Boolean
            If selectedValues.TryGetValue(key, isSelected) Then
                Return isSelected
            End If
            Return False
        End Function
        Private Sub SetIsSelected(ByVal key As Object, ByVal value As Boolean)
            If value Then
                selectedValues(key) = value
            Else
                selectedValues.Remove(key)
            End If

            If lockUpdates Then
                Return
            End If
            Dim allSelected As Boolean = True
            Dim allUnselected As Boolean = True
            allSelected = selectedValues.Count = AssociatedObject.VisibleRowCount
            allUnselected = selectedValues.Count = 0
            'for(int i = 0; i < AssociatedObject.VisibleRowCount; i++) {
            '    int handle = AssociatedObject.GetRowHandleByVisibleIndex(i);
            '    if(AssociatedObject.IsGroupRowHandle(handle)) continue;
            '    bool b = (bool)AssociatedObject.GetCellValue(handle, CheckUnboundColumnFieldName);
            '    if(b) allUnselected = false;
            '    else allSelected = false;
            '}

            If (Not allSelected) AndAlso (Not allUnselected) Then
                IsAllRowsSelected = Nothing
            End If
            If allSelected Then
                IsAllRowsSelected = True
            End If
            If allUnselected Then
                IsAllRowsSelected = False
            End If
        End Sub
    End Class
End Namespace