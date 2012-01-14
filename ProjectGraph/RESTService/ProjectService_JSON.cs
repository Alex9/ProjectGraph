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
using System.Reflection;

using de.ahzf.Hermod;
using de.ahzf.Hermod.HTTP;

using Newtonsoft.Json.Linq;
using de.ahzf.Blueprints.PropertyGraphs;
using System.Collections.Generic;

#endregion

namespace de.ahzf.ProjectGraph.REST
{

    /// <summary>
    /// JSON content representation.
    /// </summary>
    public class GraphService_JSON : AProjectService
    {

        #region Constructor(s)

        #region GraphService_JSON()

        /// <summary>
        /// Creates a new CIMS service.
        /// </summary>
        public GraphService_JSON()
            : base(HTTPContentType.JSON_UTF8)
        { }

        #endregion

        #region GraphService_JSON(IHTTPConnection)

        /// <summary>
        /// Creates a new CIMS service.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        public GraphService_JSON(IHTTPConnection IHTTPConnection)
            : base(IHTTPConnection, HTTPContentType.JSON_UTF8)
        {
            this.CallingAssembly = Assembly.GetExecutingAssembly();
        }

        #endregion

        #endregion


        //#region AllGraphDBs()

        //public override HTTPResponse AllGraphDBs()
        //{

        //    var _Content = new JObject(
        //                           new JProperty("AllGraphDBs",
        //                               new JObject(
        //                                   from t in GraphServer.AllGraphDBs() select new JProperty(t.Name, t.Uri)
        //                               )
        //                           )
        //                       ).ToString();

        //    return new HTTPResponseBuilder()
        //    {
        //        HTTPStatusCode = HTTPStatusCode.OK,
        //        ContentType    = HTTPContentType.JSON_UTF8,
        //        Content        = _Content.ToUTF8Bytes()
        //    };

        //}

        //#endregion


        #region (protected) VertexSerialization(...)

        /// <summary>
        /// Serialize a single vertex.
        /// </summary>
        /// <param name="Vertex">A single vertex.</param>
        /// <returns>The serialized vertex.</returns>
        protected override Byte[] VertexSerialization(IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object,
                                                                      UInt64, Int64, String, String, Object> Vertex)
        {

            return new JObject(
                       new JProperty("PropertyVertex",
                           new JObject(
                               from   KeyValuePair
                               in     Vertex
                               select new JProperty(KeyValuePair.Key, KeyValuePair.Value)
                           )
                       )
                     ).ToString().
                       ToUTF8Bytes();

        }

        #endregion

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

            return new JArray(  ( from Vertex
                                  in   Vertices
                                  select
                                      new JObject(
                                          new JProperty("PropertyVertex",
                                              new JObject(
                                                  from   KeyValuePair
                                                  in     Vertex
                                                  select new JProperty(KeyValuePair.Key, KeyValuePair.Value)
                                              )
                                          )
                                      )
                                  ).ToArray()
                              ).ToString().
                                ToUTF8Bytes();

        }

        #endregion


    }

}
