﻿using System.Collections.Generic;
using System.Linq;
using FusionPlusPlus.Model;

namespace FusionPlusPlus.Services
{
	internal class LogAggregator
	{
		internal List<AggregateLogItem> Aggregate(List<LogItem> logs)
		{
			var result = new List<AggregateLogItem>();

			AggregateLogItem aggregate = null;

			var orderedLogs = logs
				.OrderBy(l => l.DisplayName)
				.ThenBy(l => l.TimeStampUtc)
				.ToList();

			foreach (var log in orderedLogs)
			{
				if (aggregate?.CanAggregate(log) ?? false)
				{
					aggregate.AddItem(log);
					continue;
				}

				aggregate = new AggregateLogItem(log);
				result.Add(aggregate);
			}

			return result;
		}
	}
}