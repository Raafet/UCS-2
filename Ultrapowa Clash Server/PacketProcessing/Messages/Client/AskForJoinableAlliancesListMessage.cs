﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using UCS.Core;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    internal class AskForJoinableAlliancesListMessage : Message
    {
        #region Private Fields

        private const int m_vAllianceLimit = 40;

        #endregion Private Fields

        #region Public Constructors

        public AskForJoinableAlliancesListMessage(Client client, BinaryReader br)
            : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
        }

        public override void Process(Level level)
        {
            var alliances = ObjectManager.GetInMemoryAlliances();
            var joinableAlliances = new List<Alliance>();
            var i = 0;
            var j = 0;
            while (j < m_vAllianceLimit && i < alliances.Count)
            {
                if (alliances[i].GetAllianceMembers().Count != 0 && !alliances[i].IsAllianceFull())
                {
                    joinableAlliances.Add(alliances[i]);
                    j++;
                }
                i++;
            }
            joinableAlliances = joinableAlliances.ToList();

            var p = new JoinableAllianceListMessage(Client);
            p.SetJoinableAlliances(joinableAlliances);
            PacketManager.ProcessOutgoingPacket(p);
        }

        #endregion Public Methods
    }
}