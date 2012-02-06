using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Sogeti.Pattern.Data
{
	/// <summary>
	/// Extension methods for <see cref="IDbCommand"/>
	/// </summary>
	public static class CommandExtensions
	{
		/// <summary>
		/// Add a paramater to a command.
		/// </summary>
		/// <param name="command">Command to add a parameter to</param>
		/// <param name="name">Name of the parameter</param>
		/// <param name="value">Value to set</param>
		/// <exception cref="ArgumentNullException">command/name</exception>
		public static void AddParameter(this IDbCommand command, string name, object value)
		{
			if (command == null) throw new ArgumentNullException("command");
			if (name == null) throw new ArgumentNullException("name");
			IDataParameter p = command.CreateParameter();
			p.ParameterName = name;
			p.Value = value;
			command.Parameters.Add(p);
		}
	}
}
