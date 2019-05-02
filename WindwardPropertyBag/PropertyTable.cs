// Created by Windward Studios - no copyright is claimed. This code can be used in
// any manner by anyone for any reason. There is no copyright of any kind on it. You may
// use it in commercial products. You may change it without sharing those changes.
// We ask that you keep the "created by Windward Studios" in a comment at the top.

using System.Collections;

namespace WindwardPropertyBag
{
	/// <summary>
	/// An extension of PropertyBag that manages a table of property values, in
	/// addition to firing events when property values are requested or set.
	/// </summary>
	public class PropertyTable : PropertyBag
	{
		/// <summary>
		/// Initializes a new instance of the PropertyTable class.
		/// </summary>
		public PropertyTable()
		{
			PropertyValues = new Hashtable();
		}

		/// <summary>
		/// The property values being held.
		/// </summary>
		public Hashtable PropertyValues { get; }

		/// <summary>
		/// Gets or sets the value of the property with the specified name.
		/// <p>In C#, this property is the indexer of the PropertyTable class.</p>
		/// </summary>
		public object this[string key]
		{
			get => PropertyValues[key];
			set => PropertyValues[key] = value;
		}

		/// <summary>
		/// This member overrides PropertyBag.OnGetValue.
		/// </summary>
		protected internal override void OnGetValue(PropertySpecificationEventArgs e)
		{
			e.Value = PropertyValues[e.Property.Name];
			base.OnGetValue(e);
		}

		/// <summary>
		/// This member overrides PropertyBag.OnSetValue.
		/// </summary>
		protected internal override void OnSetValue(PropertySpecificationEventArgs e)
		{
			PropertyValues[e.Property.Name] = e.Value;
			base.OnSetValue(e);
		}
	}
}