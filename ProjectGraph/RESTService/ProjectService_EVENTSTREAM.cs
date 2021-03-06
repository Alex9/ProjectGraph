﻿/*
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
using System.Linq;
using System.Reflection;

using de.ahzf.Hermod;
using de.ahzf.Hermod.HTTP;

#endregion

namespace de.ahzf.ProjectGraph.REST
{

    /// <summary>
    /// EVENTSTREAM.
    /// </summary>
    public class GraphService_EVENTSTREAM : AProjectService
    {

        #region Constructor(s)

        #region GraphService_EVENTSTREAM()

        /// <summary>
        /// Creates a new CIMS service.
        /// </summary>
        public GraphService_EVENTSTREAM()
            : base(HTTPContentType.EVENTSTREAM)
        { }

        #endregion

        #region GraphService_EVENTSTREAM(IHTTPConnection)

        /// <summary>
        /// Creates a new CIMS service.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        public GraphService_EVENTSTREAM(IHTTPConnection IHTTPConnection)
            : base(IHTTPConnection, HTTPContentType.EVENTSTREAM)
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
        }

        #endregion

        #endregion


        #region GetEvents()

        public override HTTPResponse GetEvents()
        {

            var _RequestHeader      = IHTTPConnection.InHTTPRequest;
            var _LastEventId        = 0UL;
            var _Client_LastEventId = 0UL;
            var _EventSource        = IHTTPConnection.URLMapping.EventSource("GraphEvents");

            if (_RequestHeader.TryGet<UInt64>("Last-Event-Id", out _Client_LastEventId))
                _LastEventId = _Client_LastEventId + 1;

            var _Random = new Random();
            _EventSource.Submit("vertexadded", "{\"radius\": " + _Random.Next(5, 50) + ", \"x\": " + _Random.Next(50, 550) + ", \"y\": " + _Random.Next(50, 350) + "}");

            //var _ResourceContent = new StringBuilder();
            //_ResourceContent.AppendLine("event:vertexadded");
            //_ResourceContent.AppendLine("id: " + _LastEventId);
            //_ResourceContent.Append("data: ");
            //_ResourceContent.Append("{\"radius\": " + _Random.Next(5, 50));
            //_ResourceContent.Append(", \"x\": "     + _Random.Next(50, 550));
            //_ResourceContent.Append(", \"y\": "     + _Random.Next(50, 350) + "}");
            //_ResourceContent.AppendLine().AppendLine();

            var _ResourceContent = _EventSource.GetEvents(_Client_LastEventId);
            var _ResourceContent2 = _ResourceContent.Select(e => e.ToString()).Aggregate((a, b) => { return a + Environment.NewLine + b; });
            var _ResourceContent3 = _ResourceContent2.ToUTF8Bytes();

            return new HTTPResponseBuilder()
                        {
                            HTTPStatusCode = HTTPStatusCode.OK,
                            ContentType    = HTTPContentType.EVENTSTREAM,
                            ContentLength  = (UInt64) _ResourceContent3.Length,
                            CacheControl   = "no-cache",
                            Connection     = "keep-alive",
                            Content        = _ResourceContent3
                        };

        }

        #endregion

    }

}
