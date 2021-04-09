using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Visma_task.Enums
{
    public enum BookTakingStatus
    {
        [Description("Book is already taken")] BookAlradyTaken,
        [Description("Book is taken successfuly")] SuccessfullyTaken,
        [Description("Specified time is too long")] SpecifiedTimeTooLong,
        [Description("Customer reached his book taking limit")] ReachedBookLimit
    }
}
