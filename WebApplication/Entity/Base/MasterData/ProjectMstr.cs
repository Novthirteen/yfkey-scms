using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.Transportation;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public  class ProjectMstr : EntityBase
    {
        private string projectID;
        public string ProjectID { get { return projectID; } set {   projectID=value; } }
        private string projectDesc;
        public string ProjectDesc { get { return projectDesc; } set {   projectDesc=value; } }

		public override int GetHashCode()
        {
            if (ProjectID != null)
            {
                return ProjectID.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            ProjectMstr another = obj as ProjectMstr;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.ProjectID == another.ProjectID);
            }
        } 
    }
	
}
