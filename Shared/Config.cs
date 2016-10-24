using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class Config
    {
        /// <summary>
        /// Set to false to switch to MSMQ rather than Azure.
        /// Note: for MSMQ you will need to switch on Windows Feature Microsoft Message Queue (MSMQ) Server Core,
        ///       and set the Distributed Transaction Coordinator windows service to start automatically.
        /// </summary>
        public static readonly bool USE_AZURE_INSTEAD_OF_MSMQ = true;
    }
}
