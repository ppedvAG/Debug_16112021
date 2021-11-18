using System;

namespace HalloAsync_WPF
{
    class Dinge
    {
        public Dinge()
        {
            WerfeEx();
        }

        private static void WerfeEx()
        {
            throw new ExecutionEngineException();
        }
    }
}
