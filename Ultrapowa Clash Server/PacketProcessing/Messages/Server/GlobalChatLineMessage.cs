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
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 24715
    internal class GlobalChatLineMessage : Message
    {
        #region Private Fields

        private readonly int m_vPlayerLevel;
        private int m_vAllianceIcon;
        private long m_vAllianceId;
        private string m_vAllianceName;
        private long m_vCurrentHomeId;
        private bool m_vHasAlliance;
        private long m_vHomeId;
        private int m_vLeagueId;
        private string m_vMessage;
        private string m_vPlayerName;

        #endregion Private Fields

        #region Public Constructors

        public GlobalChatLineMessage(Client client) : base(client)
        {
            SetMessageType(24715);

            m_vMessage = "default";
            m_vPlayerName = "default";
            m_vHomeId = 1;
            m_vCurrentHomeId = 1;
            m_vPlayerLevel = 1;
            m_vHasAlliance = false;
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Encode()
        {
            var pack = new List<byte>();

            pack.AddString(m_vMessage);
            pack.AddString(m_vPlayerName);
            pack.AddInt32(m_vPlayerLevel);
            pack.AddInt32(m_vLeagueId);
            pack.AddInt64(m_vHomeId);
            pack.AddInt64(m_vCurrentHomeId);

            if (!m_vHasAlliance)
            {
                pack.Add(0);
            }
            else
            {
                pack.Add(1);
                pack.AddInt64(m_vAllianceId);
                pack.AddString(m_vAllianceName);
                pack.AddInt32(m_vAllianceIcon);
            }

            Encrypt(pack.ToArray());
        }

        public void SetAlliance(Alliance alliance)
        {
            if (alliance != null)
            {
                m_vHasAlliance = true;
                m_vAllianceId = alliance.GetAllianceId();
                m_vAllianceName = alliance.GetAllianceName();
                m_vAllianceIcon = alliance.GetAllianceBadgeData();
            }
        }

        public void SetChatMessage(string message)
        {
            m_vMessage = message;
        }

        public void SetLeagueId(int leagueId)
        {
            m_vLeagueId = leagueId;
        }

        public void SetPlayerId(long id)
        {
            m_vHomeId = id;
            m_vCurrentHomeId = id;
        }

        public void SetPlayerName(string name)
        {
            m_vPlayerName = name;
        }

        #endregion Public Methods
    }
}