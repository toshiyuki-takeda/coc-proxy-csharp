﻿using System;
using System.Net.Sockets;

namespace coc_proxy_csharp
{
    public class Protocol
    {
        protected static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                State state = (State)ar.AsyncState;
                Socket socket = state.socket;
                int bytesReceived = socket.EndReceive(ar);
                PacketReceiver.receive(bytesReceived, socket, state);
                socket.BeginReceive(state.buffer, 0, State.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        protected static void SendCallback(IAsyncResult ar)
        {
            try
            {
                State state = (State)ar.AsyncState;
                Socket socket = state.socket;
                int bytesSent = socket.EndSend(ar);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
