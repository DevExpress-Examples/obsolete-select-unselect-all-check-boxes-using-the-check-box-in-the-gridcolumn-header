using System.Collections.Generic;
using System.Windows;

namespace UnboundCheckColumn
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            grid.ItemsSource = TestDataList.Create();
        }
    }

    public class TestDataList : List<TestDataItem> {
        public static TestDataList Create() {
            TestDataList res = new TestDataList();
            for(int i = 0; i < 5; i++) {
                TestDataItem item = new TestDataItem();
                item.ID = i;
                item.Value = "A";
                res.Add(item);
            }
            for(int i = 0; i < 5; i++) {
                TestDataItem item = new TestDataItem();
                item.ID = i;
                item.Value = "B";
                res.Add(item);
            }
            for (int i = 0; i < 5; i++) {
                TestDataItem item = new TestDataItem();
                item.ID = i;
                item.Value = "C";
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
