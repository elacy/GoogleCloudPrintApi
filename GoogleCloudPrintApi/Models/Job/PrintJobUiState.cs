﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCloudPrintApi.Models.Job
{
    public class PrintJobUiState
    {
        public PrintJobUiState(SummaryType summary, string progress, string cause)
        {
            Summary = summary;
            Progress = progress;
            Cause = cause;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum SummaryType
        {
            DRAFT = 0,
            QUEUED = 1,
            IN_PROGRESS = 2,
            PAUSED = 3,
            DONE = 4,
            CANCELLED = 5,
            ERROR = 6,
            EXPIRED = 7
        }

        public SummaryType Summary { get; private set; }

        public string Progress { get; private set; }

        public string Cause { get; private set; }
    }
}
