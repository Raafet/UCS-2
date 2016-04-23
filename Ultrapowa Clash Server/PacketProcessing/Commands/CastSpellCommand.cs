﻿using System.IO;
using UCS.GameFiles;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 604 = 0x25C
    internal class CastSpellCommand : Command
    {
        #region Public Constructors

        public CastSpellCommand(BinaryReader br)
        {
            X = br.ReadInt32WithEndian();
            Y = br.ReadInt32WithEndian();
            Spell = (SpellData)br.ReadDataReference();
            Unknown1 = br.ReadUInt32WithEndian();
        }

        #endregion Public Constructors

        #region Public Properties

        public SpellData Spell { get; set; }
        public uint Unknown1 { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void Execute(Level level)
        {
            var components = level.GetComponentManager().GetComponents(0);
            for (var i = 0; i < components.Count; i++)
            {
                var c = (UnitStorageComponent)components[i];
                if (c.GetUnitTypeIndex(Spell) != -1)
                {
                    var storageCount = c.GetUnitCountByData(Spell);
                    if (storageCount >= 1)
                    {
                        c.RemoveUnits(Spell, 1);
                        break;
                    }
                }
            }
        }

        #endregion Public Methods
    }
}