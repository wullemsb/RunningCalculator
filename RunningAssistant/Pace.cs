using System;

namespace RunningAssistant
{
	public class Pace
	{
		public double Minutes { get; set; }
		public double Seconds { get; set; }

		public override string ToString()
		{
			return Minutes.ToString("0#") + ':' + Seconds.ToString("0#");
		}
	}
}

