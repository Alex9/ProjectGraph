/*
 * Copyright (c) 2011 Achim 'ahzf' Friedland <achim@ahzf.de>
 * This file is part of ProjectGraph <http://www.github.com/ahzf/ProjectGraph>
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
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Balder;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Hermod.Datastructures;

#endregion

namespace de.ahzf.ProjectGraph.REST
{

    /// <summary>
    /// A tcp/http based GraphServer.
    /// </summary>
    public class ProjectServer : HTTPServer<IProjectService>, IProjectServer
    {

        #region Data

        private readonly IDictionary<String, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object,
                                                                    UInt64, Int64, String, String, Object>> VertexLookup;

        #endregion
        
        #region Properties

        #region PropertyGraph

        public IPropertyGraph PropertyGraph { get; private set; }

        #endregion

        #region DefaultServerName

        /// <summary>
        /// The default server name.
        /// </summary>
        public override String DefaultServerName
        {
            get
            {
                return "www.graph-database.org v0.1";
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        #region GraphServer(PropertyGraph)

        /// <summary>
        /// Initialize the GraphServer using IPAddress.Any, http port 8182 and start the server.
        /// </summary>
        public ProjectServer(IPropertyGraph PropertyGraph)
            : base(IPv4Address.Any, new IPPort(80), Autostart: true)
        {

            #region Initial Checks

            if (PropertyGraph == null)
                throw new ArgumentNullException("PropertyGraph", "The given PropertyGraph must not be null!");

            #endregion

            this.PropertyGraph = PropertyGraph;
            this.ServerName    = DefaultServerName;
            this.VertexLookup  = new Dictionary<String, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object>>();

            base.OnNewHTTPService += CentralService => { CentralService.GraphServer = this; };

        }

        #endregion

        #region GraphServer(PropertyGraph, Port, AutoStart = false)

        /// <summary>
        /// Initialize the GraphServer using IPAddress.Any and the given parameters.
        /// </summary>
        /// <param name="Port">The listening port</param>
        /// <param name="Autostart"></param>
        public ProjectServer(IPropertyGraph PropertyGraph, IPPort Port, Boolean Autostart = false)
            : base(IPv4Address.Any, Port, Autostart: true)
        {

            #region Initial Checks

            if (PropertyGraph == null)
                throw new ArgumentNullException("PropertyGraph", "The given PropertyGraph must not be null!");

            #endregion

            this.PropertyGraph = PropertyGraph;
            this.ServerName    = DefaultServerName;
            this.VertexLookup = new Dictionary<String, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object>>();

            base.OnNewHTTPService += CentralService => { CentralService.GraphServer = this; };

        }

        #endregion

        #region GraphServer(PropertyGraph, IIPAddress, Port, AutoStart = false)

        /// <summary>
        /// Initialize the GraphServer using the given parameters.
        /// </summary>
        /// <param name="IIPAddress">The listening IP address(es)</param>
        /// <param name="Port">The listening port</param>
        /// <param name="Autostart"></param>
        public ProjectServer(IPropertyGraph PropertyGraph, IIPAddress IIPAddress, IPPort Port, Boolean Autostart = false)
            : base(IIPAddress, Port, Autostart: true)
        {

            #region Initial Checks

            if (PropertyGraph == null)
                throw new ArgumentNullException("PropertyGraph", "The given PropertyGraph must not be null!");

            #endregion

            this.PropertyGraph = PropertyGraph;
            this.ServerName    = DefaultServerName;
            this.VertexLookup = new Dictionary<String, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object>>();

            base.OnNewHTTPService += CentralService => { CentralService.GraphServer = this; };

        }

        #endregion

        #region GraphServer(PropertyGraph, IPSocket, Autostart = false)

        /// <summary>
        /// Initialize the GraphServer using the given parameters.
        /// </summary>
        /// <param name="IPSocket">The listening IPSocket.</param>
        /// <param name="Autostart"></param>
        public ProjectServer(IPropertyGraph PropertyGraph, IPSocket IPSocket, Boolean Autostart = false)
            : base(IPSocket.IPAddress, IPSocket.Port, Autostart: true)
        {

            #region Initial Checks

            if (PropertyGraph == null)
                throw new ArgumentNullException("PropertyGraph", "The given PropertyGraph must not be null!");

            #endregion

            this.PropertyGraph = PropertyGraph;
            this.ServerName    = DefaultServerName;
            this.VertexLookup = new Dictionary<String, IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object,
                                                                        UInt64, Int64, String, String, Object>>();

            base.OnNewHTTPService += CentralService => { CentralService.GraphServer = this; };

        }

        #endregion

        #endregion




        public void AddToIndex(IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object> Vertex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IGenericPropertyVertex<ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object, ulong, long, string, string, object>> Vertices()
        {
            throw new NotImplementedException();
        }

    }

}
