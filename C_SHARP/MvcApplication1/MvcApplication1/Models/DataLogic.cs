using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace MvcApplication1.Models
{
    public class DataLogic
    {
        IDataReader reader = null;
        DbCommand Command;
        protected Microsoft.Practices.EnterpriseLibrary.Data.Database DataBase { get; set; }

        public List<Persona> listarPersonas()
        {

            string conexion = @"MIDATABASE";

            List<Persona> personas = new List<Persona>();
            this.DataBase = DatabaseFactory.CreateDatabase(conexion);
            Command = DataBase.GetSqlStringCommand("select * from [dbo].[Persona];");
            //Command = DataBase.GetStoredProcCommand("procedimiento");
            reader = DataBase.ExecuteReader(Command);

            Persona p = null;

            while (reader.Read())
            {
                p = new Persona 
                    { 
                        Nombre = reader["Nombre"].ToString(), 
                        ID = Convert.ToInt32( reader["ID"].ToString()),
                        Apellidos = reader["Apellidos"].ToString()
                    };

                personas.Add(p);
            }

            return personas;
        }
    }
}