using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            view.AllowSelectionSynchronize = !view.AllowSelectionSynchronize;
        }
    }

    public class TestDataList : List<TestDataItem> {
        public static TestDataList Create() {
            TestDataList res = new TestDataList();
            for(int i = 0; i < 1000; i++) {
                TestDataItem item = new TestDataItem();
                item.ID = i;
                item.Value = "A";
                res.Add(item);
            }
            for(int i = 0; i < 1000; i++) {
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
