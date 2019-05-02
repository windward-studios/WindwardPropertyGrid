// Created by Windward Studios - no copyright is claimed. This code can be used in
// any manner by anyone for any reason. There is no copyright of any kind on it. You may
// use it in commercial products. You may change it without sharing those changes.
// We ask that you keep the "created by Windward Studios" in a comment at the top.

using System.Collections.Specialized;
using System.Configuration;

namespace WindwardPropertyBag
{
	/// <summary>
	/// Used to set code coverage breakpoints in the code in DEBUG mode only.
	/// </summary>
	public static class Trap
	{
		private static bool stopOnBreak;

		static Trap()
		{
#if DEBUG
			string strTrap = ConfigurationManager.AppSettings["trap"];
			if (! string.IsNullOrEmpty(strTrap))
			{
				stopOnBreak = strTrap.Trim().ToLower() == "true";
			}
			else
				stopOnBreak = true;
#endif // DEBUG
		}

		/// <summary>Will break in to the debugger (debug builds only).</summary>
		public static void trap()
		{
#if DEBUG
			if (stopOnBreak)
				System.Diagnostics.Debugger.Break();
#endif
		}

		/// <summary>Will break in to the debugger if breakOn is true (debug builds only).</summary>
		/// <param name="breakOn">Will break if this boolean value is true.</param>
		public static void trap(bool breakOn)
		{
#if DEBUG
			if (stopOnBreak && breakOn)
				System.Diagnostics.Debugger.Break();
#endif
		}
	}
}
