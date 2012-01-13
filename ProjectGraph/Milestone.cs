using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using de.ahzf.Blueprints.PropertyGraphs;

namespace de.ahzf.ProjectGraph
{

    public class Milestone
    {

        #region Properties

        /// <summary>
        /// The Id of the milestone.
        /// </summary>
        public UInt64 Id { get; private set; }

        /// <summary>
        /// The name of the milestone.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// The property graph of the milestone.
        /// </summary>
        public IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object> Graph { get; private set; }

        /// <summary>
        /// The property vertex of the milestone.
        /// </summary>
        public IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object> Vertex { get; private set; }

        #endregion

        #region Constructor(s)

        public Milestone(UInt64 Id,
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
                                                 SetProperty("Type",  "Milestone").
                                                 SetProperty("class", this));

        }

        #endregion

    }

}
