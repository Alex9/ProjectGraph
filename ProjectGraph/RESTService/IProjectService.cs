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

using de.ahzf.Hermod.HTTP;

#endregion

namespace de.ahzf.ProjectGraph.REST
{

    //[HTTPService(Host: "localhost:8080", ForceAuthentication: true)]
    [HTTPService(HostAuthentication: true)]
    public interface IProjectService : IHTTPBaseService
    {

        #region Properties

        ProjectServer GraphServer { get; set; }

        #endregion



        #region Events

        /// <summary>
        /// Get Events
        /// </summary>
        /// <returns>Endless text</returns>
        [HTTPEventMappingAttribute("GraphEvents", "/Events"), NoAuthentication]
        HTTPResponse GetEvents();

        #endregion


        #region AllGraphDBs()

        /// <summary>
        /// Get Landingpage
        /// </summary>
        /// <returns>Some HTML and JavaScript</returns>
        [HTTPMapping(HTTPMethods.GET, "/AllGraphDBs"), NoAuthentication]
        HTTPResponse AllGraphDBs();

        #endregion


        #region ProjectList(AccountId)

        /// <summary>
        /// Return a list of all projects within this account.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/ProjectList"), NoAuthentication]
        HTTPResponse ProjectList(String AccountId);

        #endregion

        #region WorkPackageList(AccountId, ProjectName)

        /// <summary>
        /// Return a list of all work packages within the given project.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        /// <param name="ProjectName">The name of the project.</param>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{ProjectName}/WorkPackages"), NoAuthentication]
        HTTPResponse WorkPackageList(String AccountId, String ProjectName);

        #endregion

        #region MilestoneList(AccountId, ProjectName)

        /// <summary>
        /// Return a list of all milestones within the given project.
        /// </summary>
        /// <param name="AccountId">The account identification.</param>
        /// <param name="ProjectName">The name of the project.</param>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{ProjectName}/Milestones"), NoAuthentication]
        HTTPResponse MilestoneList(String AccountId, String ProjectName);

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
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/VertexById/{VertexId}"), NoAuthentication]
        HTTPResponse VertexById(String AccountId, String RepositoryId, String TransactionId, String GraphId, String VertexId);

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
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/VerticesById"), NoAuthentication]
        HTTPResponse VerticesById(String AccountId, String RepositoryId, String TransactionId, String GraphId);

        #endregion

        #region VerticesByType(AccountId, RepositoryId, TransactionId, GraphId)

        /// <summary>
        /// Return an enumeration of all vertices having one of the
        /// given vertex types.
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="RepositoryId"></param>
        /// <param name="TransactionId"></param>
        /// <param name="GraphId"></param>
        /// <remarks>Include a JSON array having vertex types.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/VerticesByType"), NoAuthentication]
        HTTPResponse VerticesByType(String AccountId, String RepositoryId, String TransactionId, String GraphId);

        #endregion

        #region VerticesByType(AccountId, RepositoryId, TransactionId, GraphId, VertexType)

        /// <summary>
        /// Return an enumeration of all vertices having one of the
        /// given vertex types.
        /// </summary>
        /// <param name="AccountId"></param>
        /// <param name="RepositoryId"></param>
        /// <param name="TransactionId"></param>
        /// <param name="GraphId"></param>
        /// <param name="VertexType"></param>        
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/VerticesByType/{VertexType}"), NoAuthentication]
        HTTPResponse VerticesByType(String AccountId, String RepositoryId, String TransactionId, String GraphId, String VertexType);

        #endregion

        #region Vertices(AccountId, RepositoryId, TransactionId, GraphId)

        /// <summary>
        /// Get an enumeration of all vertices in the graph.
        /// An optional vertex filter may be applied for filtering.
        /// </summary>
        /// <remarks>Include $somescript for vertex filtering.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/Vertices"), NoAuthentication]
        HTTPResponse Vertices(String AccountId, String RepositoryId, String TransactionId, String GraphId);

        #endregion

        #region NumberOfVertices(AccountId, RepositoryId, TransactionId, GraphId, VertexIdList)

        /// <summary>
        /// Return the current number of vertices which match the given optional filter.
        /// When the filter is null, this method should implement an optimized
        /// way to get the currenty number of vertices.
        /// </summary>
        /// <remarks>Include $somescript for vertex filtering.</remarks>
        [HTTPMapping(HTTPMethods.GET, "/{AccountId}/{RepositoryId}/{TransactionId}/{GraphId}/NumberOfVertices"), NoAuthentication]
        HTTPResponse NumberOfVertices(String AccountId, String RepositoryId, String TransactionId, String GraphId);

        #endregion


     }

}
