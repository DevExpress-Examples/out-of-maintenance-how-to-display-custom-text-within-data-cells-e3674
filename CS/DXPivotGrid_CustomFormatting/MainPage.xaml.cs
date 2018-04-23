using System;
using System.IO;
using System.Reflection;
using System.Windows.Controls;
using System.Xml.Serialization;
using DevExpress.Xpf.PivotGrid;

namespace DXPivotGrid_CustomFormatting {
    public partial class MainPage : UserControl {
        string dataFileName = "DXPivotGrid_CustomFormatting.nwind.xml";
        public MainPage() {
            InitializeComponent();

            // Parses an XML file and creates a collection of data items.
            Assembly assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(dataFileName);
            XmlSerializer s = new XmlSerializer(typeof(OrderData));
            object dataSource = s.Deserialize(stream);

            // Binds a pivot grid to this collection.
            pivotGridControl1.DataSource = dataSource;
        }

        private void pivotGridControl1_CustomCellDisplayText(object sender, 
            PivotCellDisplayTextEventArgs e) {
            if (e.RowValueType != FieldValueType.GrandTotal ||
                e.ColumnValueType == FieldValueType.GrandTotal ||
                e.ColumnValueType == FieldValueType.Total) return;
            if (Convert.ToSingle(e.Value) < 4000)
                e.DisplayText = "Low";
            else if (Convert.ToSingle(e.Value) > 5000)
                e.DisplayText = "High";
            else
                e.DisplayText = "Middle";
        }
    }
}