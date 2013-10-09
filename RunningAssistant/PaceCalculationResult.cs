using System;
using System.Collections.Generic;

namespace RunningAssistant
{
	public class PaceCalculationResult
	{
		public Pace Pace {
			get;
			set;
		}

		public double NumberOfLaps {
			get;
			set;
		}

		public IEnumerable<SplitItem> SplitItems {
			get;
			set;
		}

		public override string ToString ()
		{
			return string.Format ("[Pace={0}, NumberOfLaps={1}]", Pace, NumberOfLaps);
		}
	}
}

