using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Balder;

namespace de.ahzf.ProjectGraph
{

    /// <summary>
    /// A project.
    /// </summary>
    public class Project

    {

        #region Properties

        /// <summary>
        /// The name of the project.
        /// </summary>
        public String Name  { get; private set; }

        /// <summary>
        /// The property graph of the project.
        /// </summary>
        public IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object,
                                     UInt64, Int64, String, String, Object> Graph { get; private set; }

        /// <summary>
        /// The property vertex of the project.
        /// </summary>
        public IGenericPropertyVertex<UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object,
                                      UInt64, Int64, String, String, Object> Vertex { get; private set; }

        public UInt64 NumberOfWorkPackages
        {
            get
            {
                return this.Vertex.OutDegree("HasWorkPackage");
            }
        }

        public UInt64 NumberOfWorkTasks
        {
            get
            {
                return (UInt64) this.Vertex.Out("HasWorkPackage").OutE("HasWorkTask").Count();
            }
        }

        public UInt64 NumberOfDeliverables
        {
            get
            {
                return (UInt64) this.Vertex.Out("HasWorkPackage").OutE("HasDeliverable").Count();
            }
        }

        public UInt64 NumberOfMilestones
        {
            get
            {
                return this.Vertex.OutDegree("HasMilestone");
            }
        }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// A project.
        /// </summary>
        /// <param name="Name">The name of the project.</param>
        /// <param name="Graph">The property graph of the project.</param>
        public Project(String Name,
                       IGenericPropertyGraph<UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object,
                                             UInt64, Int64, String, String, Object> Graph)
        {

            this.Name  = Name;
            this.Graph = Graph;

            this.Vertex = Graph.AddVertex(v => v.SetProperty("Name",  Name).
                                                 SetProperty("Type",  "Project").
                                                 SetProperty("class", this));

        }

        #endregion


        public Participant AddParticipant(String ShortName, String LongName)
        {

            var Participant = new Participant(this.Vertex.OutDegree("HasParticipant") + 1,
                                              Name,
                                              Graph);

            Graph.AddEdge(this.Vertex, "HasParticipant", Participant.Vertex);

            return Participant;

        }


        public WorkPackage AddWorkPackage(String Name, ActivityType ActivityType, UInt32 StartMonth, UInt32 EndMonth, Double Force)
        {

            var WorkPackage = new WorkPackage(this.Vertex.OutDegree("HasWorkPackage") + 1,
                                              Name,
                                              ActivityType,
                                              StartMonth,
                                              EndMonth,
                                              Force,
                                              Graph);

            Graph.AddEdge(this.Vertex, "HasWorkPackage", WorkPackage.Vertex);

            return WorkPackage;

        }


        public Milestone AddMilestone(String Name, UInt32 Month, String Verification, String Text, params WorkPackage[] WorkPackagesInvolved)
        {

            var Milestone = new Milestone(this.Vertex.OutDegree("HasMilestone") + 1,
                                          Name,
                                          Graph);

            Graph.AddEdge(this.Vertex, "HasMilestone", Milestone.Vertex);

            return Milestone;

        }

    }

}
