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
using System.ComponentModel;

namespace WindwardPropertyBag
{
	/// <summary>
	/// Class which represents the property specification as an actual property descriptor and which is returned to 
	/// callers of the ICustomTypeDescriptor interface methods on the PropertyBag
	/// </summary>
	public class PropertySpecificationDescriptor : PropertyDescriptor
	{

		private PropertyBag ContainingBag { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PropertySpecificationDescriptor"/> class.
		/// </summary>
		/// <param name="specification">The specification.</param>
		/// <param name="containingBag">The containing bag.</param>
		/// <param name="propertyAttributes">The property attributes.</param>
		public PropertySpecificationDescriptor(PropertySpecification specification, PropertyBag containingBag, Attribute[] propertyAttributes) :
			base(specification.Name, propertyAttributes)
		{
			ContainingBag = containingBag;
			Specification = specification;
		}


		/// <summary>
		/// When overridden in a derived class, returns whether resetting an object changes its value.
		/// </summary>
		/// <param name="component">The component to test for reset capability.</param>
		/// <returns>
		/// true if resetting the component changes its value; otherwise, false.
		/// </returns>
		public override bool CanResetValue(object component)
		{
			return Specification.DefaultValue != null && !this.GetValue(component).Equals(Specification.DefaultValue);
		}

		/// <summary>
		/// Gets an editor of the specified type.
		/// </summary>
		/// <returns>
		/// An instance of the requested editor type, or null if an editor cannot be found.
		/// </returns>
		/// <param name="editorBaseType">The base type of editor, which is used to differentiate between multiple editors that a property supports. </param>
		public override object GetEditor(Type editorBaseType)
		{
			if (Specification.EditorType == null)
				return base.GetEditor((editorBaseType));
			Type editorType = Type.GetType(Specification.EditorType.AssemblyQualifiedName);
			object editor = Activator.CreateInstance(editorType);
			return editor;
		}

		/// <summary>
		/// When overridden in a derived class, gets the current value of the property on a component.
		/// </summary>
		/// <param name="component">The component with the property for which to retrieve the value.</param>
		/// <returns>
		/// The value of a property for a given component.
		/// </returns>
		public override object GetValue(object component)
		{
			// Have the property bag raise an event to get the current value of the property.
			PropertySpecificationEventArgs e = new PropertySpecificationEventArgs(Specification, null);
			ContainingBag.OnGetValue(e);
			if (Specification != null)
			{
				if (e.Value == null)
				{
					if (Specification.DefaultValue != null)
						e.Value = Specification.DefaultValue;
				}
				else
				{
					if (Specification.PropertyType.IsEnum)
					{
						Trap.trap();
						e.Value = Enum.ToObject(Specification.PropertyType, e.Value);
					}
				}
			}
			return e.Value;
		}


		/// <summary>
		/// When overridden in a derived class, resets the value for this property of the component to the default value.
		/// </summary>
		/// <param name="component">The component with the property value that is to be reset to the default value.</param>
		public override void ResetValue(object component)
		{
			SetValue(component, Specification.DefaultValue);
		}


		/// <summary>
		/// When overridden in a derived class, sets the value of the component to a different value.
		/// </summary>
		/// <param name="component">The component with the property value that is to be set.</param>
		/// <param name="value">The new value.</param>
		public override void SetValue(object component, object value)
		{
			// Have the property bag raise an event to set the current value of the property.
			object valueToUse = value;
			if (Specification != null)
			{
				if (Specification.PropertyType == typeof(string))
				{
					if (Specification.ConvertEmptyStringToNull && ((valueToUse as string) == string.Empty))
					{
						Trap.trap();
						valueToUse = null;
					}
				}
				else
				{
					if (Specification.PropertyType.IsEnum)
					{
						Trap.trap();
						valueToUse = Convert.ToInt32(valueToUse);
					}
				}
			}
			PropertySpecificationEventArgs e = new PropertySpecificationEventArgs(Specification, valueToUse);
			ContainingBag.OnSetValue(e);
		}

		/// <summary>
		/// When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.
		/// </summary>
		/// <param name="component">The component with the property to be examined for persistence.</param>
		/// <returns>
		/// true if the property should be persisted; otherwise, false.
		/// </returns>
		public override bool ShouldSerializeValue(object component)
		{
			object val = this.GetValue(component);
			return Specification.DefaultValue != null && val != null && !val.Equals(Specification.DefaultValue);
		}


		#region Class Property Declarations
		/// <summary>
		/// Gets the PropertySpecification which was the source of this descriptor
		/// </summary>
		public PropertySpecification Specification { get; }

		/// <summary>
		/// When overridden in a derived class, gets the type of the component this property is bound to.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Type"/> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)"/> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)"/> methods are invoked, the object specified might be an instance of this type.
		/// </returns>
		public override Type ComponentType
		{
			get { return Specification.GetType(); }
		}

		/// <summary>
		/// When overridden in a derived class, gets a value indicating whether this property is read-only.
		/// </summary>
		/// <value></value>
		/// <returns>true if the property is read-only; otherwise, false.
		/// </returns>
		public override bool IsReadOnly
		{
			get { return (Attributes.Matches(ReadOnlyAttribute.Yes)); }
		}

		/// <summary>
		/// When overridden in a derived class, gets the type of the property.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// A <see cref="T:System.Type"/> that represents the type of the property.
		/// </returns>
		public override Type PropertyType
		{
			get { return Specification.PropertyType; }
		}
		#endregion
	}
}