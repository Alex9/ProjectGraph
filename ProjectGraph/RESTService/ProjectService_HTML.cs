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
using System.Text;
using System.Reflection;

using de.ahzf.Hermod;
using de.ahzf.Hermod.HTTP;
using de.ahzf.Blueprints.PropertyGraphs;
using System.Collections.Generic;

#endregion

namespace de.ahzf.ProjectGraph.REST
{

    /// <summary>
    /// HTML content representation.
    /// </summary>
    public class GraphService_HTML : AProjectService
    {

        #region Constructor(s)

        #region GraphService_HTML()

        /// <summary>
        /// Creates a new central system.
        /// </summary>
        public GraphService_HTML()
            : base(HTTPContentType.HTML_UTF8)
        { }

        #endregion

        #region GraphService_HTML(IHTTPConnection)

        /// <summary>
        /// Creates a new central system.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        public GraphService_HTML(IHTTPConnection IHTTPConnection)
            : base(IHTTPConnection, HTTPContentType.HTML_UTF8)
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
        }

        #endregion

        #endregion


        #region (private) HTMLBuilder(Headline, StringBuilderFunc)

        private String HTMLBuilder(String Headline, Action<StringBuilder> StringBuilderFunc)
        {

            var _StringBuilder = new StringBuilder();

            _StringBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            _StringBuilder.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\" \"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
            _StringBuilder.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            _StringBuilder.AppendLine("<head>");
            _StringBuilder.AppendLine("<title>Hermod HTTP Server</title>");
            _StringBuilder.AppendLine("</head>");
            _StringBuilder.AppendLine("<body>");
            _StringBuilder.Append("<h2>").Append(Headline).AppendLine("</h2>");
            _StringBuilder.AppendLine("<table>");
            _StringBuilder.AppendLine("<tr>");
            _StringBuilder.AppendLine("<td style=\"width: 100px\">&nbsp;</td>");
            _StringBuilder.AppendLine("<td>");

            StringBuilderFunc(_StringBuilder);

            _StringBuilder.AppendLine("</td>");
            _StringBuilder.AppendLine("</tr>");
            _StringBuilder.AppendLine("</table>");
            _StringBuilder.AppendLine("</body>").AppendLine("</html>").AppendLine();

            return _StringBuilder.ToString();

        }

        #endregion


        //#region GetRoot()

        //public override HTTPResponse GetRoot()
        //{

        //    var GraphDBs = GraphServer.AllGraphDBs().
        //                               Select(t => "<a href=\"" + t.Uri + "\">" + t.Name + "</a>").
        //                               Aggregate((a, b) =>  a + "<br>" + b);

        //    return new HTTPResponseBuilder() {
        //        HTTPStatusCode = HTTPStatusCode.OK,
        //        ContentType    = HTTPContentType.HTML_UTF8,
        //        Content        = HTMLBuilder("www.graph-database.org v0.1", b => b.AppendLine("Hello world!<br>").AppendLine(GraphDBs)).ToUTF8Bytes()
        //    };

        //}

        //#endregion

        //#region AllGraphDBs()

        //public override HTTPResponse AllGraphDBs()
        //{

        //    var GraphDBs = GraphServer.AllGraphDBs().
        //                               Select(t => "<a href=\"" + t.Uri + "\">" + t.Name + "</a>").
        //                               Aggregate((a, b) => a + "<br>" + b);

        //    return new HTTPResponseBuilder()
        //    {
        //        HTTPStatusCode = HTTPStatusCode.OK,
        //        ContentType    = HTTPContentType.HTML_UTF8,
        //        Content        = HTMLBuilder("www.graph-database.org v0.1", b => b.AppendLine("Hello world!<br>").AppendLine(GraphDBs)).ToUTF8Bytes()
        //    };

        //}

        //#endregion


        #region (protected) VerticesSerialization(...)

        /// <summary>
        /// Serialize an enumeration of vertices.
        /// </summary>
        /// <param name="Vertex">A single vertex.</param>
        /// <returns>The serialized vertex.</returns>
        protected override Byte[] VerticesSerialization(IEnumerable<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                                    UInt64, Int64, String, String, Object,
                                                                                    UInt64, Int64, String, String, Object,
                                                                                    UInt64, Int64, String, String, Object>> Vertices)
        {

            return HTMLBuilder("www.graph-database.org v0.1", b =>
            {

                b.AppendLine("Vertices<br>");

                foreach (var Vertex in Vertices)
                {

                    foreach (var KVP in Vertex)
                        b.Append(KVP.Key).
                          Append(" = ").
                          Append(KVP.Value.ToString()).
                          AppendLine("<br>");

                    b.AppendLine("<br>");

                }

            }).ToUTF8Bytes();

        }

        #endregion


    }

}
