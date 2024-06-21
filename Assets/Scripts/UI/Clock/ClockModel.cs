using System;
using Infrastructure;

namespace UI.Clock
{
    public class ClockModel
    {
        public ReactiveProperty<DateTimeOffset> Date = new ReactiveProperty<DateTimeOffset>(DateTimeOffset.Now);
    }
}