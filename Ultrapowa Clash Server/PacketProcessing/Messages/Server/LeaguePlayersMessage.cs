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

using System.Collections.Generic;
using UCS.Helpers;

namespace UCS.PacketProcessing
{
    internal class LeaguePlayersMessage : Message
    {
        #region Public Constructors

        public LeaguePlayersMessage(Client client) : base(client)
        {
            SetMessageType(24503);
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var data = new List<byte>();
            data.AddInt32(1);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            data.AddInt32(0);
            Encrypt(data.ToArray());
        }

        #endregion Public Methods
    }
}