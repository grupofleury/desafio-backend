using Exam.UI.Tests.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exam.UI.Tests
{
    public abstract class ApplicationTestBase
    {
        protected Fake Fake { get; } = new Fake();
    }
}
