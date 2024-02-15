using System.Data.SqlClient;
using System.Data;
using MvcNetCoreLinqToSql.Models;

namespace MvcNetCoreLinqToSql.Repository
{
    public class RepositoryEnfermo
    {
        private DataTable tablaEnfermos;


        public RepositoryEnfermo() 
        {
            string connectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            string sql = "select *from ENFERMO";
            SqlDataAdapter adEnf = new SqlDataAdapter(sql, connectionString);
            this.tablaEnfermos = new DataTable();
            adEnf.Fill(this.tablaEnfermos);
        }
        public List<Enfermo> GetEnfermo()
        {
            var consulta = from datos in this.tablaEnfermos.AsEnumerable() select datos;
            List<Enfermo> enfermos = new List<Enfermo>();
            foreach (var row in consulta)
            {
                Enfermo enf = new Enfermo();
                enf.Inscripcion = row.Field<string>("INSCRIPCION");
                enf.Apellido = row.Field<string>("APELLIDO");
                enf.Direccion = row.Field<string>("DIRECCION");
                enf.Fecha_NA = row.Field<string>("FECHA_NAC");
                enf.Sexo = row.Field<string>("S");
                enf.NSS = row.Field<string>("NSS");
                enfermos.Add(enf);
            }
            return enfermos;
        }
        public Enfermo FindEnfermo(string inscripcion)
        {

            var consulta = from datos in this.tablaEnfermos.AsEnumerable() where datos.Field<string>("INSCRIPCION") == inscripcion select datos;
            var row = consulta.First();
            Enfermo enf = new Enfermo();
            enf.Inscripcion = row.Field<string>("INSCRIPCION");
            enf.Apellido = row.Field<string>("APELLIDO");
            enf.Direccion = row.Field<string>("DIRECCION");
            enf.Fecha_NA = row.Field<string>("FECHA_NAC");
            enf.Sexo = row.Field<string>("S");
            enf.NSS = row.Field<string>("NSS");
            return enf;
        }

        public void DeleteEnfermo(int inscripcion)

        {

            

        }
    }
}
