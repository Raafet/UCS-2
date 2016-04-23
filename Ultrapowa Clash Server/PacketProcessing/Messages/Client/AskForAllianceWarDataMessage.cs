﻿/*
 * Program : Ultrapowa Clash Server
 * Description : A C# Writted 'Clash of Clans' Server Emulator !
 *
 * Authors:  Jean-Baptiste Martin <Ultrapowa at Ultrapowa.com>,
 *           And the Official Ultrapowa Developement Team
 *
 * Copyright (c) 2016  UltraPowa
 * All Rights Reserved.
 */

using System.IO;
using UCS.Logic;
using UCS.Network;

namespace UCS.PacketProcessing
{
    // 14331
    internal class AskForAllianceWarDataMessage : Message
    {
        #region Public Constructors

        public AskForAllianceWarDataMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
            }
        }

        public override void Process(Level level)
        {
            PacketManager.ProcessOutgoingPacket(new AllianceWarDataMessage(Client));
        }

        #endregion Public Methods
    }
}