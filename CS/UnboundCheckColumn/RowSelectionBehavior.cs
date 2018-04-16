using DevExpress.Xpf.Grid;
using DevExpress.Data;
using DevExpress.Utils;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Markup;
using System;
using System.Collections.Generic;
namespace UnboundCheckColumn {
    public class SelectAllColumn : GridColumn {
        public const string CheckHeaderTemplate = @"
         <DataTemplate 
xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
xmlns:dxe=""http://schemas.devexpress.com/winfx/2008/xaml/editors""
xmlns:dxg=""http://schemas.devexpress.com/winfx/2008/xaml/grid""
>
            <dxe:CheckEdit IsChecked=""{Binding 
Path=DataContext.RowSelectionBehavior.IsAllRowsSelected, Mode=TwoWay, 
RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type dxg:GridColumnHeader}}}"" />
        </DataTemplate>
";
        public const string CheckCellTemplate = @"
                        <DataTemplate
xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""
xmlns:dxe=""http://schemas.devexpress.com/winfx/2008/xaml/editors""
xmlns:dxg=""http://schemas.devexpress.com/winfx/2008/xaml/grid""
>
                            <dxe:CheckEdit x:Name=""PART_Editor""
                                      HorizontalAlignment=""Center""
                                      VerticalAlignment=""Center"" />
                        </DataTemplate>
";

        public static readonly DependencyProperty RowSelectionBehaviorProperty =
            DependencyProperty.Register("RowSelectionBehavior", typeof(RowSelectionBehavior), typeof(SelectAllColumn), new PropertyMetadata(null));
        public RowSelectionBehavior RowSelectionBehavior {
            get { return (RowSelectionBehavior)GetValue(RowSelectionBehaviorProperty); }
            set { SetValue(RowSelectionBehaviorProperty, value); }
        }
        public TableView GridView { get { return (TableView)View; } }
        public GridControl Grid { get { return GridView.Grid; } }
        public SelectAllColumn() {
            FieldName = "Selected";
            Width = 20;
            UnboundType = UnboundColumnType.Boolean;
            AllowSorting = DefaultBoolean.False;
            AllowAutoFilter = false;
            AllowBestFit = DefaultBoolean.False;
            AllowColumnFiltering = DefaultBoolean.False;
            AllowGrouping = DefaultBoolean.False;
            HeaderTemplate = (DataTemplate)XamlReader.Parse(SelectAllColumn.CheckHeaderTemplate);
            CellTemplate = (DataTemplate)XamlReader.Parse(SelectAllColumn.CheckCellTemplate);
        }
        protected override void OnOwnerChanged() {
            base.OnOwnerChanged();
            if(Grid == null) return;
            foreach(Behavior b in Interaction.GetBehaviors(Grid)) {
                if(b is RowSelectionBehavior) {
                    RowSelectionBehavior = (RowSelectionBehavior)b;
                    break;
                }
            }
            if(RowSelectionBehavior != null) FieldName = RowSelectionBehavior.CheckUnboundColumnFieldName;
        }
    }
    public class RowSelectionBehavior : Behavior<GridControl> {
        public static readonly DependencyProperty IsAllRowsSelectedProperty =
            DependencyProperty.Register("IsAllRowsSelected", typeof(bool?), typeof(RowSelectionBehavior),
            new PropertyMetadata(false, (d, e) => ((RowSelectionBehavior)d).OnIsAllRowsSelectedChanged()));
        public static readonly DependencyProperty CheckUnboundColumnFieldNameProperty =
            DependencyProperty.Register("CheckUnboundColumnFieldName", typeof(string), typeof(RowSelectionBehavior),
            new PropertyMetadata("Selected"));
        public bool? IsAllRowsSelected {
            get { return (bool?)GetValue(IsAllRowsSelectedProperty); }
            set { SetValue(IsAllRowsSelectedProperty, value); }
        }
        public string CheckUnboundColumnFieldName {
            get { return (string)GetValue(CheckUnboundColumnFieldNameProperty); }
            set { SetValue(CheckUnboundColumnFieldNameProperty, value); }
        }

        void OnIsAllRowsSelectedChanged() {
            if(IsAllRowsSelected == null) return;
            lockUpdates = true;
            SetIsSelectedForAllRows(IsAllRowsSelected.Value);
            AssociatedObject.RefreshData();
            lockUpdates = false;
        }
        void SetIsSelectedForAllRows(bool value) {
            for(int i = 0; i < AssociatedObject.VisibleRowCount; i++) {
                int handle = AssociatedObject.GetRowHandleByVisibleIndex(i);
                if(AssociatedObject.IsGroupRowHandle(handle)) continue;
                //int ind = AssociatedObject.GetRowListIndex(handle);
                AssociatedObject.SetCellValue(handle, CheckUnboundColumnFieldName, value);
            }
        }
        bool lockUpdates = false;
        public RowSelectionBehavior() {
            selectedValues = new Dictionary<int, bool>();
        }
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.CustomUnboundColumnData += new GridColumnDataEventHandler(AssociatedObject_CustomUnboundColumnData);
            AssociatedObject.FilterChanged += new RoutedEventHandler(AssociatedObject_FilterChanged);
        }

        void AssociatedObject_FilterChanged(object sender, RoutedEventArgs e) {
            UpdateIsAllRowsSelectedProperty();
        }
        protected override void OnDetaching() {
            AssociatedObject.CustomUnboundColumnData -= new GridColumnDataEventHandler(AssociatedObject_CustomUnboundColumnData);
            AssociatedObject.FilterChanged -= new RoutedEventHandler(AssociatedObject_FilterChanged);
            base.OnDetaching();
        }
        Dictionary<int, bool> selectedValues;
        void AssociatedObject_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e) {
            if(e.Column.FieldName == CheckUnboundColumnFieldName) {
                if(e.IsGetData) {
                    e.Value = GetIsSelected(e.ListSourceRowIndex);
                }
                if(e.IsSetData) {
                    SetIsSelected(e.ListSourceRowIndex, (bool)e.Value);
                }
            }
        }
        void UpdateIsAllRowsSelectedProperty() {
            if (lockUpdates) return;
            bool allSelected = true;
            bool allUnselected = true;
            allSelected = selectedValues.Count >= AssociatedObject.VisibleRowCount;
            allUnselected = selectedValues.Count == 0;

            if (!allSelected && !allUnselected)
                IsAllRowsSelected = null;
            if (allSelected)
                IsAllRowsSelected = true;
            if (allUnselected)
                IsAllRowsSelected = false;
        }
        bool GetIsSelected(int key) {
            bool isSelected;
            if(selectedValues.TryGetValue(key, out isSelected))
                return isSelected;
            return false;
        }
        void SetIsSelected(int key, bool value) {
            if(value) selectedValues[key] = value;
            else selectedValues.Remove(key);
            UpdateIsAllRowsSelectedProperty();
        }
    }
}