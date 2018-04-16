// Developer Express Code Central Example:
// How to implement an unbound editable check column
// 
// This example demonstrates how to emulate row selection via an unbound column
// with a checkbox. The grid's built-in editing is used. It's enabled when the
// view's NavigationStyle property is set to CellNavigation. Note: If you want this
// feature to be natively supported by the DXGrid control, please track the
// corresponding suggestion: .
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E1263

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Grid;
using System.Windows.Interactivity;
using DevExpress.Data;
using DevExpress.Utils;

namespace UnboundCheckColumn
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            grid.ItemsSource = TestDataList.Create();
            grid2.ItemsSource = TestDataList.Create();
        }
    }

    public class TestDataList : List<TestDataItem> {
        public static TestDataList Create() {
            TestDataList res = new TestDataList();
            for(int i = 0; i < 5000; i++) {
                TestDataItem item = new TestDataItem();
                item.ID = i;
                item.Value = "A";
                res.Add(item);
            }
            for(int i = 0; i < 5000; i++) {
                TestDataItem item = new TestDataItem();
                item.ID = i;
                item.Value = "B";
                res.Add(item);
            }
            return res;
        }
    }
    public class TestDataItem {
        public int ID { get; set; }
        public string Value { get; set; }
    }
}
