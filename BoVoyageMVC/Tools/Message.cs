using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyageMVC.Tools
{
    public sealed class Message
    {
        public Message(MessageType messageType,string text)
        {
            MessageType = messageType;
            Text = text;
        }

        public MessageType MessageType { get; private set; }//nao é possivel de mudar as messagens
        public string Text { get; private set; }


    }
    public enum MessageType
    {
        SUCCES,
        ERROR
    }
}