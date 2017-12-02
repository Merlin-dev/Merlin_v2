﻿using Merlin.Concurrent;
using Merlin.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Merlin.Communication
{
    public static class CommunicationService
    {
        public static CommunicationClient Client { get; private set; }

        public static void Initialize()
        {
            int port = 2424; //TODO: Read from file or pass as param when injecting, this will be instance specific so we can handle more clients at once

            Client = CommunicationManager.CreateClient(IPAddress.Loopback, port);

            //Start packet receiver
            ConcurrentTaskManager.RunAsync(() => {
                while (true)
                {
                    object obj = Client.Receive();
                    if(obj is ThreadCommand tc)
                    {
                        ThreadManager.ExecuteThreadCommand(tc);
                    }
                }
            }, "Packet Receiver");
        }
    }
}
