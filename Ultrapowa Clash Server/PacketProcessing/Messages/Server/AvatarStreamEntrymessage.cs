﻿using System.Collections.Generic;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24412
    internal class AvatarStreamEntryMessage : Message
    {
        #region Private Fields

        private AvatarStreamEntry m_vAvatarStreamEntry;

        #endregion Private Fields

        #region Public Constructors

        public AvatarStreamEntryMessage(Client client) : base(client)
        {
            SetMessageType(24412);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var pack = new List<byte>();

            pack.AddRange(m_vAvatarStreamEntry.Encode());

            Encrypt(pack.ToArray());
        }

        public void SetAvatarStreamEntry(AvatarStreamEntry entry)
        {
            m_vAvatarStreamEntry = entry;
        }

        #endregion Public Methods
    }
}