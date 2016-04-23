﻿using System.IO;
using UCS.Helpers;
using UCS.Logic;

namespace UCS.PacketProcessing
{
    //Commande 0x1FA
    internal class CollectResourcesCommand : Command
    {
        #region Public Constructors

        public CollectResourcesCommand(BinaryReader br)
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
            var go = level.GameObjectManager.GetGameObjectByID(BuildingId);

            if (go != null)
            {
                if (go.ClassId == 0 || go.ClassId == 4)
                {
                    var constructionItem = (ConstructionItem)go;
                    constructionItem.GetResourceProductionComponent().CollectResources();
                }
            }
        }

        #endregion Public Methods
    }
}