﻿using System;
using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class AskForAllianceDataMessage : Message
    {
        #region Private Fields

        private long m_vAllianceId;

        #endregion Private Fields

        #region Public Constructors

        public AskForAllianceDataMessage(Client client, BinaryReader br)
            : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                m_vAllianceId = br.ReadInt64WithEndian();
            }
        }

        public override void Process(Level level)
        {
            var alliance = ObjectManager.GetAlliance(m_vAllianceId);
            Console.WriteLine("ID DU CLAN : " + m_vAllianceId);
            if (alliance != null)
                PacketManager.ProcessOutgoingPacket(new AllianceDataMessage(Client, alliance));
        }

        #endregion Public Methods
    }
}