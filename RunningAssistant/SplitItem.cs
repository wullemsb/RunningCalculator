using System;

namespace RunningAssistant
{
	public class SplitItem
	{
		public double Hours { get; set; }
		public double Minutes { get; set; }
		public double Seconds { get; set; }
		public double Distance { get; set; }

		public override string ToString ()
		{
			return string.Format ("[SplitItem: Hours={0}, Minutes={1}, Seconds={2}, Distance={3}]", Hours, Minutes, Seconds, Distance);
		}
	}
}

