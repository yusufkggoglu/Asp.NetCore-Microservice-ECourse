using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Messages
{
    public class CourseNameChangedEvent
    {
        public string CourseId;
        public string UpdatedName { get; set; }

    }
}
