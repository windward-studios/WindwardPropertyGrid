//////////////////////////////////////////////////////////////////////
// Algorithmia is (c) 2018 Solutions Design. All rights reserved.
// https://github.com/SolutionsDesign/Algorithmia
//////////////////////////////////////////////////////////////////////
// COPYRIGHTS:
// Additional tweaks 2019 by Windward Studios - no copyright is claimed on our work.
// Copyright (c) 2018 Solutions Design. All rights reserved. (Algorithmia)
// Copyright (c) 2018 Tony Allowatt (property bag code)
// 
// The Algorithmia library sourcecode and its accompanying tools, tests and support code
// are released under the following license: (BSD2)
// ----------------------------------------------------------------------
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met: 
//
// 1) Redistributions of source code must retain the above copyright notice, this list of 
//    conditions and the following disclaimer. 
// 2) Redistributions in binary form must reproduce the above copyright notice, this list of 
//    conditions and the following disclaimer in the documentation and/or other materials 
//    provided with the distribution. 
// 
// THIS SOFTWARE IS PROVIDED BY SOLUTIONS DESIGN ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL SOLUTIONS DESIGN OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
//
// The views and conclusions contained in the software and documentation are those of the authors 
// and should not be interpreted as representing official policies, either expressed or implied, 
// of Solutions Design. 
//
//////////////////////////////////////////////////////////////////////
// Contributers to the code:
//      - David Thielen
//		- Tony Allowatt 
//		- Frans Bouma [FB]
//////////////////////////////////////////////////////////////////////
// The code in this file and related property bag files are based on the work of Tony Allowatt
// which can be found here: http://www.codeproject.com/KB/miscctrl/bending_property.aspx .
// I ([FB]) re-implemented/ported the code for .NET 3.5, though the credits go to Tony Allowatt for the 
// initial idea and ground work
//////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;

namespace WindwardPropertyBag
{
	/// <summary>
	/// Class which is used to specify a single property definition.
	/// </summary>
	public class PropertySpecification
	{
		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="propertyType">A Type that represents the type of the property.</param>
		/// <param name="tag">User defined data attached to this object.</param>
		public PropertySpecification(string name, Type propertyType, object tag)
			: this(name, propertyType, string.Empty, string.Empty, null, null, null, null, tag)
		{
		}

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="propertyType">A Type that represents the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the property grid.</param>
		/// <param name="tag">User defined data attached to this object.</param>
		public PropertySpecification(string name, Type propertyType, string category, object tag)
			: this(name, propertyType, category, string.Empty, null, null, null, null, tag)
		{
		}

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="propertyType">A Type that represents the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the property grid.</param>
		/// <param name="tag">User defined data attached to this object.</param>
		public PropertySpecification(string name, Type propertyType, string category, string description, object tag)
			: this(name, propertyType, category, description, null, null, null, null, tag)
		{
		}


		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="propertyType">The fully qualified name of the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is no default value.</param>
		/// <param name="tag">User defined data attached to this object.</param>
		public PropertySpecification(string name, Type propertyType, string category, string description, object defaultValue, object tag)
			: this(name, propertyType, category, description, defaultValue, null, null, null, tag)
		{
		}

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="propertyType">The fully qualified name of the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is no default value.</param>
		/// <param name="valueList">The allowed values for an enum type.</param>
		/// <param name="tag">User defined data attached to this object.</param>
		public PropertySpecification(string name, Type propertyType, string category, string description, object defaultValue, List<string> valueList, object tag)
					: this(name, propertyType, category, description, defaultValue, valueList, null, 
					valueList != null ? typeof(PropertySpecificationValuesListTypeConverter) : null, tag)
		{
		}

		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="propertyType">The fully qualified name of the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is no default value.</param>
		/// <param name="valueList">The allowed values for an enum type.</param>
		/// <param name="editorType">The Type that represents the type of the editor for this property.  This type must derive from UITypeEditor.</param>
		/// <param name="tag">User defined data attached to this object.</param>
		public PropertySpecification(string name, Type propertyType, string category, string description, object defaultValue,
					List<string> valueList, Type editorType, object tag)
					: this(name, propertyType, category, description, defaultValue, valueList, editorType, 
					valueList != null ? typeof(PropertySpecificationValuesListTypeConverter) : null, tag)
		{
		}


		/// <summary>
		/// Initializes a new instance of the PropertySpec class.
		/// </summary>
		/// <param name="name">The name of the property displayed in the property grid.</param>
		/// <param name="propertyType">The fully qualified name of the type of the property.</param>
		/// <param name="category">The category under which the property is displayed in the property grid.</param>
		/// <param name="description">A string that is displayed in the help area of the property grid.</param>
		/// <param name="defaultValue">The default value of the property, or null if there is no default value.</param>
		/// <param name="valueList">The allowed values for an enum type.</param>
		/// <param name="editorType">The Type that represents the type of the editor for this property.  This type must derive from UITypeEditor.</param>
		/// <param name="typeConverterType">The Type that represents the type of the type converter for this property.  This type must derive from TypeConverter.</param>
		/// <param name="tag">User defined data attached to this object.</param>
		public PropertySpecification(string name, Type propertyType, string category, string description, object defaultValue,
											List<string> valueList, Type editorType, Type typeConverterType, object tag)
		{
			// means calling it wrong (passing allowed as default)
			Trap.trap(defaultValue != null && defaultValue.GetType().IsArray);
			this.Name = name;
			this.PropertyType = propertyType;
			this.Category = category;
			this.Description = description;
			this.DefaultValue = defaultValue;
			this.Attributes = new List<Attribute>();
			this.ValueList = valueList;
			this.EditorType = editorType;
			this.TypeConverterType = typeConverterType;
			this.Tag = tag;
		}


		#region Class Property Declarations
		/// <summary>
		/// Gets or sets a collection of additional Attributes for this property.  This can be used to specify attributes beyond those supported intrinsically
		/// by the PropertySpecification class, such as ReadOnly and Browsable.
		/// </summary>
		public List<Attribute> Attributes { get; set; }

		/// <summary>
		/// Gets or sets the category name of this property.
		/// </summary>
		public string Category { get; set; }

		/// <summary>
		/// Gets or sets the type converter type for this property.
		/// </summary>
		public Type TypeConverterType { get; set; }

		/// <summary>
		/// Gets or sets the default value of this property.
		/// </summary>
		public object DefaultValue { get; set; }

		/// <summary>
		/// Gets or sets the help text description of this property.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the editor type for this property.
		/// </summary>
		public Type EditorType { get; set; }

		/// <summary>
		/// Gets or sets the name of this property.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the type of this property.
		/// </summary>
		public Type PropertyType { get; set; }

		/// <summary>
		/// Gets or sets the values list to use for properties which have to have their value picked from a pre-fabricated list. 
		/// To use a ValueList, specify a string-typed property and use the PropertySpecificationValuesListTypeConverter as type converter.
		/// </summary>
		public List<string> ValueList { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether an empty string (if the specification is for a string property) has to be converted to null (so 
		/// the default value is used) or should be kept as-is.
		/// </summary>
		public bool ConvertEmptyStringToNull { get; set; }

		/// <summary>
		/// User defined data attached to this object.
		/// </summary>
		public object Tag { get; set; }

		/// <summary>
		/// This should use the password character instead of showing the text entered.
		/// </summary>
		public bool IsPassword { get; set; }

		/// <summary>
		/// A ValueList is provided, but any string can be entered, not just items in the list.
		/// </summary>
		public bool IsValueListPlus { get; set; }

		/// <summary>
		/// The provided ValueList is flag values. Multiple items on the list can be selected.
		/// </summary>
		public bool IsValueListFlags { get; set; }

		#endregion
	}
}