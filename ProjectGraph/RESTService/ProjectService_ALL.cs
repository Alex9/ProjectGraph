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

using System.Reflection;

using de.ahzf.Hermod.HTTP;

#endregion

namespace de.ahzf.ProjectGraph.REST
{

    /// <summary>
    /// Any content representation.
    /// </summary>
    public class GraphService_ALL : AProjectService
    {

        #region Constructor(s)

        #region GraphService_ALL()

        /// <summary>
        /// Creates a new central system.
        /// </summary>
        public GraphService_ALL()
            : base(HTTPContentType.ALL)
        { }

        #endregion

        #region GraphService_ALL(IHTTPConnection)

        /// <summary>
        /// Creates a new central system.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        public GraphService_ALL(IHTTPConnection IHTTPConnection)
            : base(IHTTPConnection, HTTPContentType.ALL)
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
        }

        #endregion

        #endregion

    }

}
