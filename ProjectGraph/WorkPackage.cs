using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using de.ahzf.Blueprints.PropertyGraphs;

namespace de.ahzf.ProjectGraph
{

    public class WorkPackage
    {

        #region Properties

        /// <summary>
        /// The Id of the work package.
        /// </summary>
        public UInt64 Id   { get; private set; }

        /// <summary>
        /// The name of the work package.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// The property graph of the work package.
        /// </summary>
        public IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object> Graph { get; private set; }

        /// <summary>
        /// The property vertex of the work package.
        /// </summary>
        public IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object> Vertex { get; private set; }

        #endregion

        #region Constructor(s)

        public WorkPackage(UInt64 Id,
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
                                                 SetProperty("Type",  "WorkPackage").
                                                 SetProperty("class", this));

        }

        #endregion


        public WorkTask AddWorkTask(String Name)
        {

            var WorkTask = new WorkTask(String.Concat(Id, ".", this.Vertex.OutDegree("HasWorkTask") + 1),
                                        Name,
                                        Graph);

            Graph.AddEdge(this.Vertex, "HasWorkTask", WorkTask.Vertex);

            return WorkTask;

        }

    }

}
