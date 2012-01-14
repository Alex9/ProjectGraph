/*
 * Copyright (c) 2012 Achim 'ahzf' Friedland <achim.friedland@aperis.com>
 * This file is part of ProjectGraph <http://www.github.com/Aperis/ProjectGraph>
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or
 * (at your option) any later version.
 * 
 * You may obtain a copy of the License at
 *     http://www.gnu.org/licenses/gpl.html
 *     
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * General Public License for more details.
 */

#region Usings

using System;

using de.ahzf.Blueprints.PropertyGraphs;

#endregion

namespace de.ahzf.ProjectGraph
{

    public class WorkTask
    {

        #region Properties

        /// <summary>
        /// The Id of the work task.
        /// </summary>
        public String Id { get; private set; }

        /// <summary>
        /// The name of the work task.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// The property graph of the work task.
        /// </summary>
        public IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object> Graph { get; private set; }

        /// <summary>
        /// The property vertex of the work task.
        /// </summary>
        public IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object> Vertex { get; private set; }

        #endregion

        #region Constructor(s)

        public WorkTask(String Id,
                        String Name,
                        IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object,
                                              UInt64, Int64, String, String, Object> Graph)
        {

            this.Id     = Id;
            this.Name   = Name;
            this.Graph  = Graph;

            this.Vertex = Graph.AddVertex(v => v.SetProperty("Id_",   Id).
                                                 SetProperty("Name",  Name).
                                                 SetProperty("Type",  "WorkTask").
                                                 SetProperty("class", this));

        }

        #endregion

    }

}
