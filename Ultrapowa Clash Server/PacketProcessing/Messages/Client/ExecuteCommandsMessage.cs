﻿using System;
using System.IO;
using UCS.Core;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Packet 14102
    internal class ExecuteCommandsMessage : Message
    {
        #region Public Fields

        public uint Checksum;

        public byte[] NestedCommands;
        public uint NumberOfCommands;
        public uint Subtick;

        #endregion Public Fields

        #region Public Constructors

        public ExecuteCommandsMessage(Client client, BinaryReader br) : base(client, br)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public override void Decode()
        {
            using (var br = new BinaryReader(new MemoryStream(GetData())))
            {
                Subtick = br.ReadUInt32WithEndian();
                Checksum = br.ReadUInt32WithEndian();
                NumberOfCommands = br.ReadUInt32WithEndian();

                if (NumberOfCommands > 0 && NumberOfCommands < 20)
                {
                    NestedCommands = br.ReadBytes(GetLength());
                }
            }
        }

        public override void Process(Level level)
        {
            try
            {
                level.Tick();

                if (NumberOfCommands > 0 && NumberOfCommands < 20)
                    using (var br = new BinaryReader(new MemoryStream(NestedCommands)))
                        for (var i = 0; i < NumberOfCommands; i++)
                        {
                            var obj = CommandFactory.Read(br);
                            if (obj != null)
                            {
                                Debugger.WriteLine("\t Processing " + obj.GetType().Name);
                                ((Command)obj).Execute(level);
                            }
                            else
                                break;
                        }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Debugger.WriteLine("Exception occurred during command processing." + ex);
                Console.ResetColor();
            }
        }

        #endregion Public Methods
    }
}