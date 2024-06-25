<!-- default file list -->
*Files to look at*:

* [Window1.xaml](./CS/UnboundCheckColumn/Window1.xaml) (VB: [Window1.xaml](./VB/UnboundCheckColumn/Window1.xaml))
* [Window1.xaml.cs](./CS/UnboundCheckColumn/Window1.xaml.cs) (VB: [Window1.xaml.vb](./VB/UnboundCheckColumn/Window1.xaml.vb))
<!-- default file list end -->
# How to select/unselect all check boxes using the check box in the GridColumn header


<p>Starting with version <strong>14.2</strong>, GridControl provides <a href="https://documentation.devexpress.com/WPF/CustomDocument17808.aspx">Selector Column</a> out of the box. To show this column, set the <a href="https://documentation.devexpress.com/WPF/DevExpressXpfGridTableView_ShowCheckBoxSelectorColumntopic.aspx">TableView.ShowCheckBoxSelectorColumn</a> property to true. <br><br>For version older than <strong>14.2</strong>, to achieve this functionality, it is necessary to create a check edit unbound column, place a check edit in the column header, and select/unselect all check boxes using one check box in the header. You can get more information about this approach in the following KB article: <a href="https://www.devexpress.com/Support/Center/p/KA18610">How to implement multiple row selection behavior via a checked column</a>.</p>

<br/>


