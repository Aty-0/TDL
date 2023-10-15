using System;

namespace TDL.Game.Base
{
    public enum TDLStatusTask
    {
        Pending,
        InProgress,
        Completed,
    }

    public enum TDLPriority
    {
        VeryHigh,
        High,
        Medium, 
        Low,
        VeryLow,
    }

    public class TDLTask
    {
        public TDLPriority priority { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public TDLStatusTask status { get; set; }
    }
}
