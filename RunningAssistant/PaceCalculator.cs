using System;
using System.Collections.Generic;
using System.Text;

namespace RunningAssistant
{
	public class PaceCalculator
	{
		public PaceCalculationResult Calc(PaceCalculateParameters parameters)
		{
			var hours = parameters.Hours;
			var minutes = parameters.Minutes;
			var seconds = parameters.Seconds;
			var distance = parameters.Distance;

			var timeInSeconds = GetTimeInSeconds(hours, minutes, seconds);
			var averageSpeed = CalculateAverageSpeed(distance, timeInSeconds);
			var pace = CalculatePace(timeInSeconds, distance);
			var numberOfLaps = CalculateNumberOfLaps(distance);

			var splitItems = CalculateSplitItems(distance, pace, numberOfLaps, hours, minutes, seconds);

			return new PaceCalculationResult{
				Pace = pace,
				NumberOfLaps = numberOfLaps,
				SplitItems = splitItems
			};
		}

		private Pace CalculatePace(int timeInSeconds, double distance)
		{
			var timeDistance = timeInSeconds / (distance / 1000);
			var m = Math.Floor(timeDistance / 60);
			var s = timeDistance - m * 60;

			if (Math.Round(s, 0) == 60)
			{
				m += 1;
				s = 0;
			}
			return new Pace { Minutes = m, Seconds = s };            
		}

		private int GetTimeInSeconds(int hours, int minutes, int seconds)
		{
			return 3600 * hours + 60 * minutes + seconds;
		}

		private double CalculateAverageSpeed(double distance, int timeInSeconds)
		{
			return 3600 * ((distance / 1000) / timeInSeconds);
		}

		private double CalculateNumberOfLaps(double distance)
		{
			return distance / 400;
		}

		private IEnumerable<SplitItem> CalculateSplitItems(double distance, Pace pace, double numberOfLaps, int hours, int minutes, int seconds)
		{
			var meters = 0;
			var splitItemList = new List<SplitItem>();
			for (var i = 0; i < Math.Floor(numberOfLaps); i++)
			{
				meters = (i + 1) * 400;
				var mPer400 = Math.Floor(pace.Minutes / 2.5);
				var sPer400 = (((pace.Minutes / 2.5) - mPer400) * 60) + (pace.Seconds / 2.5);
				var totalSecondsForCurrentMeters = (mPer400 * 60 + sPer400) * (i + 1);
				var min = Math.Floor(totalSecondsForCurrentMeters / 60);
				var sec = Math.Floor(totalSecondsForCurrentMeters - (min * 60));

				var splitItem = new SplitItem();
				splitItem.Hours = (min >= 60) ? Math.Floor(min / 60) : 0;
				splitItem.Minutes = min - (splitItem.Hours * 60);
				splitItem.Seconds = sec;
				splitItem.Distance = meters;
				splitItemList.Add(splitItem);
			}
			var delta = (distance) - Math.Floor(numberOfLaps) * 400;
			if (delta > 0)
			{
				var splitItem = new SplitItem
				{
					Hours = hours,
					Minutes = minutes,
					Seconds = seconds,
					Distance = distance
				};
				splitItemList.Add(splitItem);
			}

			return splitItemList;
		}
	}

}

