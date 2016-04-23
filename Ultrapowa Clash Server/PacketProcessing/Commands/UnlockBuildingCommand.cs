﻿using System.IO;
using UCS.GameFiles;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x208
    internal class UnlockBuildingCommand : Command
    {
        #region Public Constructors

        public UnlockBuildingCommand(BinaryReader br)
        {
            BuildingId = br.ReadInt32WithEndian(); //buildingId - 0x1DCD6500;
            Unknown1 = br.ReadUInt32WithEndian();
        }

        #endregion Public Constructors

        #region Public Properties

        public int BuildingId { get; set; }
        public uint Unknown1 { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override void Execute(Level level)
        {
            var ca = level.GetPlayerAvatar();
            var go = level.GameObjectManager.GetGameObjectByID(BuildingId);

            var b = (ConstructionItem)go;

            var bd = (BuildingData)b.GetConstructionItemData();

            if (ca.HasEnoughResources(bd.GetBuildResource(b.GetUpgradeLevel()), bd.GetBuildCost(b.GetUpgradeLevel())))
            {
                var rd = bd.GetBuildResource(b.GetUpgradeLevel());
                ca.SetResourceCount(rd, ca.GetResourceCount(rd) - bd.GetBuildCost(b.GetUpgradeLevel()));
                b.Unlock();
            }
        }

        #endregion Public Methods
    }
}