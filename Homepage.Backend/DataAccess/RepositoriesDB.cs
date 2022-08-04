using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Homepage.GlobalDefinitions.Collections;

namespace Homepage.Backend.DataAccess
{
    public class RepositoriesDB
    {
        /*  
         * Klasse holt Daten  aus der Datenbank ab , mit Hilfe der SQL Klasse, und fügt sie in eine Liste ein
         */

        public event OnErrorEventHandler OnError;
        public delegate void OnErrorEventHandler(string message);

        private SQL _sqlManager;

        public RepositoriesDB()
        {
            _sqlManager = new SQL();
        }

        /** Methode gibt Skills Tabelle zurück als Liste **/
        public List<ListSkills> GetSkills()
        {
            return getSkills();
        }

        /** Methode gibt Skills Projects zurück als Liste **/
        public List<ListProjects> GetProjects()
        {
            return getProjects();
        }

        /** Methode gibt Skills Tabelle zurück als Liste **/
        private List<ListSkills> getSkills()
        {
            List<ListSkills> listSkills = new List<ListSkills>();

            DataTable currentSkillsTable = new DataTable();

            string sqlQuery = "";

            try
            {
                sqlQuery = "GetSkills";

                /** SQL Select Statement gibt die Tabelle "Skills" zurück **/
                currentSkillsTable = _sqlManager.ExecuteSelect(sqlQuery, 
                    new string[] { }, 
                    new object[] { });

                /** Tabelle wird in einer Schleife durchlaufen und ins Modell geladen
                 * und anschließend zur Liste "listSkills" hinzugefügt **/
                if (currentSkillsTable.Rows.Count > 0)
                {
                    for(int i = 0; i< currentSkillsTable.Rows.Count; i++)
                    {
                        ListSkills currentSkill = new ListSkills();

                        if (!Convert.IsDBNull(currentSkillsTable.Rows[i]["ID"]))
                        {
                            currentSkill.Id = Convert.ToInt64(currentSkillsTable.Rows[i]["ID"]);
                        }
                        if (!Convert.IsDBNull(currentSkillsTable.Rows[i]["Name"]))
                        {
                            currentSkill.Name = currentSkillsTable.Rows[i]["Name"].ToString();
                        }
                        if (!Convert.IsDBNull(currentSkillsTable.Rows[i]["CodeSnippet"]))
                        {
                            currentSkill.CodeSnippet = currentSkillsTable.Rows[i]["CodeSnippet"].ToString();
                        }
                        if (!Convert.IsDBNull(currentSkillsTable.Rows[i]["Value"]))
                        {
                            currentSkill.Value = Convert.ToInt32(currentSkillsTable.Rows[i]["Value"]);
                        }
                        if (!Convert.IsDBNull(currentSkillsTable.Rows[i]["Active"]))
                        {
                            currentSkill.Active = (bool)currentSkillsTable.Rows[i]["Active"];
                        }
                        if (!Convert.IsDBNull(currentSkillsTable.Rows[i]["CreationDate"]))
                        {
                            currentSkill.CreationDate = (DateTime)currentSkillsTable.Rows[i]["CreationDate"];
                        }
                       
                        listSkills.Add(currentSkill);
                    }
                }
            }
            catch(Exception ex)
            {
                OnError("[GETSKILLS-ERROR] " + ex.Message);
            }
            
            return listSkills;
        }

        /** Methode gibt die Tabelle "Projects" zurück als Liste **/
        private List<ListProjects> getProjects()
        {
            List<ListProjects> listProjects = new List<ListProjects>();

            DataTable currentProjectsTable = new DataTable();
            DataTable currentProjectImagesTable = new DataTable();

            string sqlQuery = "";

            try
            {
                sqlQuery = "GetProjects";

                /** SQL Select Statement gibt die Tabelle "Projects" zurück **/
                currentProjectsTable = _sqlManager.ExecuteSelect(sqlQuery, 
                    new string[] { }, 
                    new object[] { });

                /** Tabelle wird in einer Schleife durchlaufen und ins Modell geladen
                * und anschließend zur Liste "listProjects" hinzugefügt **/
                if (currentProjectsTable.Rows.Count > 0)
                {
                    for (int i = 0; i < currentProjectsTable.Rows.Count; i++)
                    {
                        ListProjects currentProject = new ListProjects();

                        List<ListProjectImages> currentListProjectImages = new List<ListProjectImages>();

                        if (!Convert.IsDBNull(currentProjectsTable.Rows[i]["ID"]))
                        {
                            currentProject.Id = Convert.ToInt64(currentProjectsTable.Rows[i]["ID"]);
                        }
                        if (!Convert.IsDBNull(currentProjectsTable.Rows[i]["Name"]))
                        {
                            currentProject.Name = currentProjectsTable.Rows[i]["Name"].ToString();
                        }
                        if (!Convert.IsDBNull(currentProjectsTable.Rows[i]["Description"]))
                        {
                            currentProject.Description = currentProjectsTable.Rows[i]["Description"].ToString();
                        }
                        if (!Convert.IsDBNull(currentProjectsTable.Rows[i]["Live"]))
                        {
                            currentProject.Live = currentProjectsTable.Rows[i]["Live"].ToString();
                        }
                        if (!Convert.IsDBNull(currentProjectsTable.Rows[i]["Active"]))
                        {
                            currentProject.Active = (bool)currentProjectsTable.Rows[i]["Active"];
                        }
                        if (!Convert.IsDBNull(currentProjectsTable.Rows[i]["CreationDate"]))
                        {
                            currentProject.CreationDate = (DateTime)currentProjectsTable.Rows[i]["CreationDate"];
                        }

                        currentListProjectImages = getProjectsImages(currentProject.Id);

                        if(currentListProjectImages.Count > 0)
                        {
                            currentProject.ListProjectImages = currentListProjectImages;
                        }

                        listProjects.Add(currentProject);
                    }
                }
            }
            catch (Exception ex)
            {
                OnError("[GETPROJECTS-ERROR] " + ex.Message);
            }

            return listProjects;
        }

        /** Methode gibt die Tabelle "ProjectImages" zurück als Liste **/
        private List<ListProjectImages> getProjectsImages(long projectId)
        {
            List<ListProjectImages> listProjectsImages = new List<ListProjectImages>();

            DataTable currentProjectImagesTable = new DataTable();

            string sqlQuery = "";

            try
            {
                sqlQuery = "GetProjectImages";

                /** SQL Select Statement gibt die Tabelle "ProjectImages" zurück **/
                currentProjectImagesTable = _sqlManager.ExecuteSelect(sqlQuery, 
                    new string[] { "@ProjectID" }, 
                    new object[] { projectId });

                /** Tabelle wird in einer Schleife durchlaufen und ins Modell geladen
                * und anschließend zur Liste "listProjectsImages" hinzugefügt **/
                if (currentProjectImagesTable.Rows.Count > 0)
                {
                    for (int i = 0; i < currentProjectImagesTable.Rows.Count; i++)
                    {
                        ListProjectImages currentProjectImage = new ListProjectImages();

                        if (!Convert.IsDBNull(currentProjectImagesTable.Rows[i]["ID"]))
                        {
                            currentProjectImage.Id = Convert.ToInt64(currentProjectImagesTable.Rows[i]["ID"]);
                        }
                        if (!Convert.IsDBNull(currentProjectImagesTable.Rows[i]["ProjectID"]))
                        {
                            currentProjectImage.ProjectId = Convert.ToInt64(currentProjectImagesTable.Rows[i]["ProjectID"]);
                        }
                        if (!Convert.IsDBNull(currentProjectImagesTable.Rows[i]["Image"]))
                        {
                            currentProjectImage.Image = currentProjectImagesTable.Rows[i]["Image"].ToString();
                        }
                   
                        listProjectsImages.Add(currentProjectImage);
                    }
                }
            }
            catch (Exception ex)
            {
                OnError("[GETPROJECTIMAGES-ERROR] " + ex.Message);
            }

            return listProjectsImages;
        }
    }
}
