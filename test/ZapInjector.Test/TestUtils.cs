using System;

namespace ZapInjector.Test
{
    public static class TestUtils
    {
        public static Action Act(Action action) => action;
    }
}