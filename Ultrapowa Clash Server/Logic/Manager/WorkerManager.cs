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

namespace UCS.Logic.Manager
{
    internal class WorkerManager
    {
        #region Public Constructors

        public WorkerManager()
        {
            m_vGameObjectReferences = new List<GameObject>();
            m_vWorkerCount = 0;
        }

        #endregion Public Constructors

        #region Private Fields

        private readonly List<GameObject> m_vGameObjectReferences;
        private int m_vWorkerCount;

        #endregion Private Fields

        #region Public Methods

        public static int GetFinishTaskOfOneWorkerCost()
        {
            return 0;
        }

        public static void RemoveGameObjectReferences(GameObject go)
        {
        }

        public void AllocateWorker(GameObject go)
        {
            if (m_vGameObjectReferences.IndexOf(go) == -1)
            {
                m_vGameObjectReferences.Add(go);
            }
        }

        public void DeallocateWorker(GameObject go)
        {
            if (m_vGameObjectReferences.IndexOf(go) != -1)
            {
                m_vGameObjectReferences.Remove(go);
            }
        }

        public void DecreaseWorkerCount()
        {
            m_vWorkerCount--;
        }

        public void FinishTaskOfOneWorker()
        {
            var go = GetShortestTaskGO();
            if (go != null)
            {
                if (go.ClassId == 3)
                {
                    var o = (Obstacle) go;
                    if (o.IsClearingOnGoing())
                        o.SpeedUpClearing();
                }
                else
                {
                    var b = (ConstructionItem) go;
                    if (b.IsConstructing())
                        b.SpeedUpConstruction();
                    else
                    {
                        var hero = b.GetHeroBaseComponent();
                        if (hero != null)
                            hero.SpeedUpUpgrade();
                    }
                }
            }
        }

        public int GetFreeWorkers()
        {
            return m_vWorkerCount - m_vGameObjectReferences.Count;
        }

        public GameObject GetShortestTaskGO()
        {
            GameObject shortestTaskGO = null;
            var shortestGOTime = 0;
            int currentGOTime;

            foreach (var go in m_vGameObjectReferences)
            {
                currentGOTime = -1;
                if (go.ClassId == 3)
                {
                    var o = (Obstacle) go;
                    if (o.IsClearingOnGoing())
                        currentGOTime = o.GetRemainingClearingTime();
                }
                else
                {
                    var c = (ConstructionItem) go;
                    if (c.IsConstructing())
                    {
                        currentGOTime = c.GetRemainingConstructionTime();
                    }
                    else
                    {
                        var hero = c.GetHeroBaseComponent();
                        if (hero != null)
                        {
                            if (hero.IsUpgrading())
                            {
                                currentGOTime = hero.GetRemainingUpgradeSeconds();
                            }
                        }
                    }
                }
                if (shortestTaskGO == null)
                {
                    if (currentGOTime > -1)
                    {
                        shortestTaskGO = go;
                        shortestGOTime = currentGOTime;
                    }
                }
                else if (currentGOTime > -1)
                {
                    if (currentGOTime < shortestGOTime)
                    {
                        shortestGOTime = currentGOTime;
                        shortestTaskGO = go;
                    }
                }
            }
            return shortestTaskGO;
        }

        public int GetTotalWorkers()
        {
            return m_vWorkerCount;
        }

        public void IncreaseWorkerCount()
        {
            m_vWorkerCount++;
        }

        #endregion Public Methods
    }
}