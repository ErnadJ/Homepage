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

        private SQL _sqlManager;
        private ListProjects _listProjects;
        private ListSkills _listSkills;
        private ListProjectImages _listProjectImages;

        public RepositoriesDB()
        {
            _sqlManager = new SQL();
            _listProjects = new ListProjects();
            _listSkills = new ListSkills();
            _listProjectImages = new ListProjectImages();
        }

        public ListProjects CurrentListProjects
        {
            get
            {
                return _listProjects;
            }
            set
            {
                _listProjects = value;
            }
        }

        public ListSkills CurrentListSkills
        {
            get
            {
                return _listSkills;
            }
            set
            {
                _listSkills = value;
            }
        }

        public ListProjectImages CurrentListProjectImages
        {
            get
            {
                return _listProjectImages;
            }
            set
            {
                _listProjectImages = value;
            }
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
                currentSkillsTable = _sqlManager.ExecuteSelect(sqlQuery, new string[] { }, new object[] { });

                /** Tabelle wird in einer Schleife durchlaufen und ins Modell geladen
                 * und anschließend zur Liste "listSkills" hinzugefügt **/
                if (currentSkillsTable.Rows.Count > 0)
                {
                    for(int i = 0; i< currentSkillsTable.Rows.Count; i++)
                    {
                        ListSkills currentSkill = new ListSkills();

                        currentSkill.Id = Convert.ToInt64(currentSkillsTable.Rows[i]["ID"]);
                        currentSkill.Name = currentSkillsTable.Rows[i]["Name"].ToString();
                        currentSkill.CodeSnippet = currentSkillsTable.Rows[i]["CodeSnippet"].ToString();
                        currentSkill.Value = Convert.ToInt32(currentSkillsTable.Rows[i]["Value"]);
                        currentSkill.Active = (bool)currentSkillsTable.Rows[i]["Active"];
                        currentSkill.CreationDate = (DateTime)currentSkillsTable.Rows[i]["CreationDate"];

                        listSkills.Add(currentSkill);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
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
                currentProjectsTable = _sqlManager.ExecuteSelect(sqlQuery, new string[] { }, new object[] { });

                /** Tabelle wird in einer Schleife durchlaufen und ins Modell geladen
                * und anschließend zur Liste "listProjects" hinzugefügt **/
                if (currentProjectsTable.Rows.Count > 0)
                {
                    for (int i = 0; i < currentProjectsTable.Rows.Count; i++)
                    {
                        ListProjects currentProject = new ListProjects();
                        List<ListProjectImages> currentListProjectImages = new List<ListProjectImages>();

                        currentProject.Id = Convert.ToInt64(currentProjectsTable.Rows[i]["ID"]);
                        currentProject.Name = currentProjectsTable.Rows[i]["Name"].ToString();
                        currentProject.Description = currentProjectsTable.Rows[i]["Description"].ToString();
                        currentProject.Live = currentProjectsTable.Rows[i]["Live"].ToString();
                        currentProject.Active = (bool)currentProjectsTable.Rows[i]["Active"];
                        currentProject.CreationDate = (DateTime)currentProjectsTable.Rows[i]["CreationDate"];

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
                throw ex;
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
                currentProjectImagesTable = _sqlManager.ExecuteSelect(sqlQuery, new string[] { "@ProjectID" }, new object[] { projectId });

                /** Tabelle wird in einer Schleife durchlaufen und ins Modell geladen
                * und anschließend zur Liste "listProjectsImages" hinzugefügt **/
                if (currentProjectImagesTable.Rows.Count > 0)
                {
                    for (int i = 0; i < currentProjectImagesTable.Rows.Count; i++)
                    {
                        ListProjectImages currentProjectImage = new ListProjectImages();

                        currentProjectImage.Id = Convert.ToInt64(currentProjectImagesTable.Rows[i]["ID"]);
                        currentProjectImage.ProjectId = Convert.ToInt64(currentProjectImagesTable.Rows[i]["ProjectID"]);
                        currentProjectImage.Image = currentProjectImagesTable.Rows[i]["Image"].ToString();

                        listProjectsImages.Add(currentProjectImage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listProjectsImages;
        }
    }
}
