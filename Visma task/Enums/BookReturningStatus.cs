using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Visma_task.Enums
{
    public enum BookReturningStatus
    {
        [Description("Negative feedback for book holder")] ReturnedAfterEstimatedTime,
        [Description("Book is returned on time")] ReturnedOnTime,
    }
}
