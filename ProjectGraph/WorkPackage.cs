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
        /// The starting month of the work package.
        /// </summary>
        public UInt32 StartMonth { get; private set; }

        /// <summary>
        /// The ending month of the work package.
        /// </summary>
        public UInt32 EndMonth { get; private set; }

        /// <summary>
        /// The force of the work package.
        /// </summary>
        public Double Force { get; private set; }

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

        public WorkPackage(UInt64       Id,
                           String       Name,
                           ActivityType ActivityType,
                           UInt32       StartMonth,
                           UInt32       EndMonth,
                           Double       Force,
                           IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object,
                                                 UInt64, Int64, String, String, Object> Graph)
        {

            this.Id         = Id;
            this.Name       = Name;
            this.StartMonth = StartMonth;
            this.Graph      = Graph;

            this.Vertex     = Graph.AddVertex(v => v.SetProperty("Id_",          Id).
                                                     SetProperty("Name",         Name).
                                                     SetProperty("Type",         "WorkPackage").
                                                     SetProperty("ActivityType", ActivityType).
                                                     SetProperty("StartMonth",   StartMonth).
                                                     SetProperty("EndMonth",     EndMonth).
                                                     SetProperty("Force",        Force).
                                                     SetProperty("class",        this));

        }

        #endregion

        public Participant AddLeadingParticipant(Participant Participant, UInt32 PersonMonths)
        {

            Graph.AddEdge(this.Vertex, "HasParticipant", Participant.Vertex).
                  SetProperty("IsLeader", true).
                  SetProperty("PersonMonths", PersonMonths);

            return Participant;

        }

        public Participant AddParticipant(Participant Participant, UInt32 PersonMonths, Boolean WPLeader = false)
        {

            var edge = Graph.AddEdge(this.Vertex, "HasParticipant", Participant.Vertex).
                             SetProperty("PersonMonths", PersonMonths);

            if (WPLeader)
                edge.SetProperty("IsLeader", true);

            return Participant;

        }

        public Objective AddObjective(String Name)
        {

            var Objective = new Objective(String.Concat(Id, ".", this.Vertex.OutDegree("HasObjective") + 1),
                                        Name,
                                        Graph);

            Graph.AddEdge(this.Vertex, "HasObjective", Objective.Vertex);

            return Objective;

        }

        public WorkTask AddWorkTask(String Name)
        {

            var WorkTask = new WorkTask(String.Concat(Id, ".", this.Vertex.OutDegree("HasWorkTask") + 1),
                                        Name,
                                        Graph);

            Graph.AddEdge(this.Vertex, "HasWorkTask", WorkTask.Vertex);

            return WorkTask;

        }


        public Deliverable AddDeliverable(String Name)
        {

            var Deliverable = new Deliverable(String.Concat(Id, ".", this.Vertex.OutDegree("HasDeliverable") + 1),
                                              Name,
                                              Graph);

            Graph.AddEdge(this.Vertex, "HasDeliverable", Deliverable.Vertex);

            return Deliverable;

        }

    }

}
