Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Windows.Controls
Imports System.Xml.Serialization
Imports DevExpress.Xpf.PivotGrid

Namespace DXPivotGrid_CustomFormatting
	Partial Public Class MainPage
		Inherits UserControl
		Private dataFileName As String = "nwind.xml"
		Public Sub New()
			InitializeComponent()

			' Parses an XML file and creates a collection of data items.
            Dim assembly As System.Reflection.Assembly = _
                System.Reflection.Assembly.GetExecutingAssembly()
			Dim stream As Stream = assembly.GetManifestResourceStream(dataFileName)
			Dim s As New XmlSerializer(GetType(OrderData))
			Dim dataSource As Object = s.Deserialize(stream)

			' Binds a pivot grid to this collection.
			pivotGridControl1.DataSource = dataSource
		End Sub

        Private Sub pivotGridControl1_CustomCellDisplayText(ByVal sender As Object, _
                                                            ByVal e As PivotCellDisplayTextEventArgs)
            If e.RowValueType <> FieldValueType.GrandTotal OrElse _
                e.ColumnValueType = FieldValueType.GrandTotal OrElse _
                e.ColumnValueType = FieldValueType.Total Then
                Return
            End If
            If Convert.ToSingle(e.Value) < 4000 Then
                e.DisplayText = "Low"
            ElseIf Convert.ToSingle(e.Value) > 5000 Then
                e.DisplayText = "High"
            Else
                e.DisplayText = "Middle"
            End If
        End Sub
	End Class
End Namespace