using System;
using System.ComponentModel.DataAnnotations;

namespace StatusMonitor.Shared.Models.Entities
{
	/// <summary>
	/// Represents a current status of a metric (like, "CPU Load"), eq. "Normal operation" or 
	/// "We are investigating the issue". Since labels are not arbitrary, but predefined, we use them as entities 
	/// in the database.
	/// </summary>
	public abstract class Label
	{
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Human readable string
		/// </summary>
		public string Title { get; set; }
	}

	/// <summary>
	/// Represents the label which is set automatically depending on resource status.
	/// </summary>
	public class AutoLabel : Label
	{
		/// <summary>
		/// Returns the highest available health value for the metric
		/// </summary>
		/// <returns>Highest available health value for the metric</returns>
		public static int MaxHealthValue()
		{
			return 2;
		}

		/// <summary>
		/// Returns health value of this particular label
		/// </summary>
		/// <returns>Health value of this particular label</returns>
		public int HealthValue()
		{
			switch ((AutoLabels)Id)
			{
				case AutoLabels.Normal:
					return 2;
				case AutoLabels.Warning:
					return 1;
				case AutoLabels.Critical:
					return 0;
				default:
					throw new ArgumentException("Invalid auto label ID.");
			}
		}
	}
	public enum AutoLabels
	{
		Normal = 1, Warning, Critical
	}

	/// <summary>
	/// Represents a label which is set manually by developers.
	/// </summary>
	public class ManualLabel : Label { }
	public enum ManualLabels
	{
		None = 1, Investigating
	}

	/// <summary>
	/// Represents a stage of a compilation pipeline. Since stages are not arbitrary, but predefined, we use them 
	/// as entities in the database.
	/// </summary>
	public class CompilationStage
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
	}
	public enum CompilationStages
	{
		M4 = 1, SandPiper, Simulation
	}

	/// <summary>
	/// Represents a severity for generic log entries.
	/// </summary>
	public class LogEntrySeverity
	{
		[Key]
		public int Id { get; set; }
		/// <summary>
		/// Human readable description of severity, like "Warning"
		/// </summary>
		public string Description { get; set; }
	}

	public enum LogEntrySeverities
	{
		Debug = 1, Detail, User, Info, Warn, Error, Fatal
	}
}
