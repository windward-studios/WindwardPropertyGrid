// Created by Windward Studios - no copyright is claimed. This code can be used in
// any manner by anyone for any reason. There is no copyright of any kind on it. You may
// use it in commercial products. You may change it without sharing those changes.
// We ask that you keep the "created by Windward Studios" in a comment at the top.

using System.Collections.Generic;
using DevExpress.XtraEditors;
using WindwardPropertyBag;

namespace SampleUsePropertyGrid
{
	public partial class Form1 : XtraForm
	{
		public Form1()
		{
			InitializeComponent();

			// sample data
			PropertyTable propTable = new PropertyTable();

			// string
			PropertySpecification propSpec = new PropertySpecification("Name", typeof(string), "simple", null);
			propTable.PropertySpecifications.Add(propSpec);
			propTable["Name"] = "dave";
			// integer
			propSpec = new PropertySpecification("Age", typeof(string), "simple", null);
			propTable.PropertySpecifications.Add(propSpec);
			propTable["Age"] = 25;
			// password
			propSpec = new PropertySpecification("Password", typeof(string), "simple", null);
			propSpec.IsPassword = true;
			propTable.PropertySpecifications.Add(propSpec);
			propTable["Password"] = "secret";

			// enum
			List<string> allowed = new List<string>(new string[]{"Kiawe", "Lucky", "Cody", "Zoe", "Roscoe"});
			propSpec = new PropertySpecification("Enum", typeof(string), "list", "", "", allowed, null);
			propSpec.IsValueListPlus = false;
			propTable.PropertySpecifications.Add(propSpec);
			// enum plus
			allowed = new List<string>(new string[] { "Hewey", "Dewey", "Louie" });
			propSpec = new PropertySpecification("EnumPlus", typeof(string), "list", "", "", allowed, null);
			propSpec.IsValueListPlus = true;
			propTable.PropertySpecifications.Add(propSpec);
			// enum flags
			allowed = new List<string>(new string[] { "Roger Rabbit", "Jessica Rabbit", "Ariel", "Belle" });
			propSpec = new PropertySpecification("EnumFlags", typeof(string), "list", "", "", allowed, null);
			propSpec.IsValueListFlags = true;
			propTable.PropertySpecifications.Add(propSpec);


			// assign it to the grid
			propGrid.SelectedObject = propTable;
			propGrid.ResetBindings();
			propGrid.RetrieveFields();
			propGrid.Update();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
