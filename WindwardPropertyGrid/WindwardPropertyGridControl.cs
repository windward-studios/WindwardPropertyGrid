// Created by Windward Studios - no copyright is claimed. This code can be used in
// any manner by anyone for any reason. There is no copyright of any kind on it. You may
// use it in commercial products. You may change it without sharing those changes.
// We ask that you keep the "created by Windward Studios" in a comment at the top.

using DevExpress.XtraEditors.Repository;
using DevExpress.XtraVerticalGrid;
using WindwardPropertyBag;

namespace WindwardPropertyGrid
{
	public partial class WindwardPropertyGridControl : PropertyGridControl
	{
		public WindwardPropertyGridControl()
		{
			InitializeComponent();
		}

		private void WindwardPropertyGridControl_CustomRecordCellEdit(object sender, DevExpress.XtraVerticalGrid.Events.GetCustomRowCellEditEventArgs e)
		{
			if (GetPropertyDescriptor(e.Row) is PropertySpecificationDescriptor desc && desc.Specification.IsValueListFlags)
			{
				RepositoryItemCheckedComboBoxEdit riFlags = new RepositoryItemCheckedComboBoxEdit();
				riFlags.Items.AddRange(desc.Specification.ValueList.ToArray());
				e.RepositoryItem = riFlags;
			}
		}
	}
}
