using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using de.ahzf.Blueprints.PropertyGraphs;

namespace de.ahzf.ProjectGraph
{

    public class Objective
    {

        #region Properties

        /// <summary>
        /// The Id of the objective.
        /// </summary>
        public String Id { get; private set; }

        /// <summary>
        /// The name of the objective.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// The property graph of the objective.
        /// </summary>
        public IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object> Graph { get; private set; }

        /// <summary>
        /// The property vertex of the objective.
        /// </summary>
        public IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object> Vertex { get; private set; }

        #endregion

        #region Constructor(s)

        public Objective(String Id,
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
                                                 SetProperty("Type",  "Objective").
                                                 SetProperty("class", this));

        }

        #endregion

    }

}
