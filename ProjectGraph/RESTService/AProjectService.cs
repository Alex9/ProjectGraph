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
using System.Collections.Generic;

using de.ahzf.Hermod.HTTP;
using de.ahzf.Blueprints.PropertyGraphs;

#endregion

namespace de.ahzf.ProjectGraph.REST
{

    public abstract class AProjectService : AHTTPService, IProjectService
    {

        #region Properties

        public ProjectServer GraphServer { get; set; }

        #endregion

        #region Constructor(s)

        #region AProjectService()

        /// <summary>
        /// Creates a new abstract OCPP CIMS service.
        /// </summary>
        public AProjectService()
        { }

        #endregion

        #region AProjectService(HTTPContentType)

        /// <summary>
        /// Creates a new abstract OCPP CIMS service.
        /// </summary>
        /// <param name="HTTPContentType">A content type.</param>
        public AProjectService(HTTPContentType HTTPContentType)
            : base(HTTPContentType)
        { }

        #endregion

        #region AProjectService(HTTPContentTypes)

        /// <summary>
        /// Creates a new abstract OCPP CIMS service.
        /// </summary>
        /// <param name="HTTPContentTypes">A content type.</param>
        public AProjectService(IEnumerable<HTTPContentType> HTTPContentTypes)
            : base(HTTPContentTypes)
        { }

        #endregion

        #region AProjectService(IHTTPConnection, HTTPContentType)

        /// <summary>
        /// Creates a new abstract OCPP CIMS service.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        /// <param name="HTTPContentType">A content type.</param>
        /// <param name="ResourcePath">The path to internal resources.</param>
        public AProjectService(IHTTPConnection IHTTPConnection, HTTPContentType HTTPContentType)
            : base(IHTTPConnection, HTTPContentType, "graph_database.org.resources.")
        { }

        #endregion

        #region AProjectService(IHTTPConnection, HTTPContentTypes)

        /// <summary>
        /// Creates a new abstract OCPP CIMS service.
        /// </summary>
        /// <param name="IHTTPConnection">The http connection for this request.</param>
        /// <param name="HTTPContentTypes">An enumeration of content types.</param>
        /// <param name="ResourcePath">The path to internal resources.</param>
        public AProjectService(IHTTPConnection IHTTPConnection, IEnumerable<HTTPContentType> HTTPContentTypes)
            : base(IHTTPConnection, HTTPContentTypes, "graph_database.org.resources.")
        { }

        #endregion

        #endregion

        
        #region GetRoot()

        public virtual HTTPResponse GetRoot()
        {
            //return GetResources("landingpage.html");
            return Error406_NotAcceptable();
        }

        #endregion

        #region GetEvents()

        public virtual HTTPResponse GetEvents()
        {
            return Error406_NotAcceptable();
        }

        #endregion

        #region AllGraphDBs()

        public virtual HTTPResponse AllGraphDBs()
        {
            return Error406_NotAcceptable();
        }

        #endregion



        #region (protected) VertexSerialization(...)

        /// <summary>
        /// Serialize a single vertex.
        /// </summary>
        /// <param name="Vertex">A single vertex.</param>
        /// <returns>The serialized vertex.</returns>
        protected virtual Byte[] VertexSerialization(IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object,
                                                                     UInt64, Int64, String, String, Object> Vertex)
        {
            return null;
        }

        #endregion

        #region (protected) VerticesSerialization(...)

        /// <summary>
        /// Serialize an enumeration of vertices.
        /// </summary>
        /// <param name="Vertex">A single vertex.</param>
        /// <returns>The serialized vertex.</returns>
        protected virtual Byte[] VerticesSerialization(IEnumerable<IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object,
                                                                                   UInt64, Int64, String, String, Object>> Vertices)
        {
            return null;
        }

        #endregion



        #region VertexById(AccountId, RepositoryId, TransactionId, GraphId, VertexId)

        /// <summary>
        /// Return the vertex referenced by the given vertex identifier.
        /// If no vertex is referenced by the identifier return null.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        /// <param name="RepositoryId">The repository identification.</param>
        /// <param name="TransactionId">The transaction identification.</param>
        /// <param name="GraphId">The graph identification.</param>
        /// <param name="VertexId">The vertex identification.</param>
        public virtual HTTPResponse VertexById(String AccountId, String RepositoryId, String TransactionId, String GraphId, String VertexId)
        {

            #region Process request

            UInt64 _VertexId = 0;

            if (!UInt64.TryParse(VertexId, out _VertexId))
                return Error400_BadRequest();

            #endregion

            var Vertex = GraphServer.PropertyGraph.VertexById(_VertexId);

            #region Process response

            if (Vertex == null)
                return Error404_NotFound();

            var Content = VertexSerialization(Vertex);
            if (Content == null)
                return Error400_BadRequest();

            return new HTTPResponseBuilder() {
                HTTPStatusCode = HTTPStatusCode.OK,
                ContentType    = this.HTTPContentTypes.First(),
                Content        = Content
            };

            #endregion

        }

        #endregion

        #region VerticesById(AccountId, RepositoryId, TransactionId, GraphId)

        /// <summary>
        /// Return the vertices referenced by the given array of vertex identifiers.
        /// If no vertex is referenced by a given identifier this value will be
        /// skipped.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        /// <param name="RepositoryId">The repository identification.</param>
        /// <param name="TransactionId">The transaction identification.</param>
        /// <param name="GraphId">The graph identification.</param>
        /// <remarks>Include a JSON array having vertex identifiers.</remarks>
        public virtual HTTPResponse VerticesById(String AccountId, String RepositoryId, String TransactionId, String GraphId)
        {

            #region Process request

            IEnumerable<UInt64> Ids = null;

            List<String> List = null;
            var QueryString = IHTTPConnection.InHTTPRequest.QueryString;

            if (QueryString != null)
            {
                if (QueryString.TryGetValue("Id", out List))
                    if (List != null && List.Count >= 1)
                        Ids = from s in List select UInt64.Parse(s);
            }

            #endregion

            var Vertices = GraphServer.PropertyGraph.VerticesById(Ids.ToArray());

            #region Process response

            if (Vertices == null || !Vertices.Any())
                return Error404_NotFound();

            var Content = VerticesSerialization(Vertices);
            if (Content == null)
                return Error400_BadRequest();

            return new HTTPResponseBuilder()
            {
                HTTPStatusCode = HTTPStatusCode.OK,
                ContentType    = this.HTTPContentTypes.First(),
                Content        = Content
            };

            #endregion

        }

        #endregion

        public virtual HTTPResponse VerticesByType(String AccountId, String RepositoryId, String TransactionId, String GraphId)
        {
            return Error406_NotAcceptable();
        }

        public virtual HTTPResponse VerticesByType(String AccountId, String RepositoryId, String TransactionId, String GraphId, String VertexType)
        {
            return Error406_NotAcceptable();
        }

        public virtual HTTPResponse Vertices(String AccountId, String RepositoryId, String TransactionId, String GraphId)
        {
            return Error406_NotAcceptable();
        }

        public virtual HTTPResponse NumberOfVertices(String AccountId, String RepositoryId, String TransactionId, String GraphId)
        {
            return Error406_NotAcceptable();
        }


        #region ProjectList(AccountId)

        /// <summary>
        /// Return a list of all projects within this account.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        public virtual HTTPResponse ProjectList(String AccountId)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region WorkPackageList(AccountId, ProjectName)

        /// <summary>
        /// Return a list of all work packages within the given project.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        /// <param name="ProjectName">The name of the project.</param>
        public virtual HTTPResponse WorkPackageList(String AccountId, String ProjectName)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region MilestoneList(AccountId, ProjectName)

        /// <summary>
        /// Return a list of all milestones within the given project.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        /// <param name="ProjectName">The name of the project.</param>
        public virtual HTTPResponse MilestoneList(String AccountId, String ProjectName)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
