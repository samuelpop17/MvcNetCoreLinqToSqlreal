using MvcNetCoreLinqToSql.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcNetCoreLinqToSql.Repository
{
    public class RepositoryEmpleados
    {
        private DataTable tablaEmpleados;

        public RepositoryEmpleados()
        {
            string connectionString = "Data Source=LOCALHOST\\SQLEXPRESS;Initial Catalog=HOSPITAL;Persist Security Info=True;User ID=SA;Password=MCSD2023";
            string sql = "select *from EMP";
            SqlDataAdapter adEmp = new SqlDataAdapter(sql,connectionString);
            this.tablaEmpleados=new DataTable();
            adEmp.Fill(this.tablaEmpleados);
        }

        public List<Empleado> GetEmpleados()
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable() select datos;
            List<Empleado> empleados = new List<Empleado>();
            foreach (var row in consulta)
            {
                Empleado emp = new Empleado();
                emp.IdEmpleado = row.Field<int>("EMP_NO");
                emp.Apellido = row.Field<string>("APELLIDO");
                emp.Oficio = row.Field<string>("OFICIO");
                emp.Salario = row.Field<int>("SALARIO");
                emp.IdDepartamento = row.Field<int>("DEPT_NO");
                empleados.Add(emp);
            }
            return empleados;
        }
        public Empleado FindEmpleado(int idEmpleado)
        {

            var consulta = from datos in this.tablaEmpleados.AsEnumerable() where datos.Field<int>("EMP_NO") == idEmpleado select datos;
            var row = consulta.First();
            Empleado emp = new Empleado();
            emp.IdEmpleado = row.Field<int>("EMP_NO");
            emp.Apellido = row.Field<string>("APELLIDO");
            emp.Oficio = row.Field<string>("OFICIO");
            emp.Salario = row.Field<int>("SALARIO");
            emp.IdDepartamento = row.Field<int>("DEPT_NO");
            return emp;
        }

        public List<Empleado> GetEmpleadosOficioSalario(string oficio,int salario) 
        { 
        
            var consulta= from datos in this.tablaEmpleados.AsEnumerable()
                          where datos.Field<string>("OFICIO")==oficio
                          && datos.Field<int>("SALARIO") >= salario select datos;
            if (consulta.Count()==0)
            {
                return null;
            }
            else
            {
                List<Empleado> empleados = new List<Empleado>();
                foreach (var row in consulta)
                {
                    Empleado emp = new Empleado();
                    emp.IdEmpleado = row.Field<int>("EMP_NO");
                    emp.Apellido = row.Field<string>("APELLIDO");
                    emp.Oficio = row.Field<string>("OFICIO");
                    emp.Salario = row.Field<int>("SALARIO");
                    emp.IdDepartamento = row.Field<int>("DEPT_NO");
                    empleados.Add(emp);
                }
                return empleados;
            }
           

        }

        public ResumenEmpleados GetEmpladosOficio(string oficio)
        {
            var consulta = from datos in this.tablaEmpleados.AsEnumerable()
                          where datos.Field<string>("OFICIO")== oficio
                          select datos;
            consulta = consulta.OrderBy(x => x.Field<int>("SALARIO"));
            int personas = consulta.Count();
            int maximo = consulta.Max(z => z.Field<int>("SALARIO"));
            double media = consulta.Average(x => x.Field<int>("SALARIO"));
            List<Empleado> empleados = new List<Empleado>();

            foreach (var row in consulta)
            {
                Empleado emp = new Empleado
                {
                IdEmpleado = row.Field<int>("EMP_NO"),
                Apellido = row.Field<string>("APELLIDO"),
                Oficio = row.Field<string>("OFICIO"),
                Salario = row.Field<int>("SALARIO"),
                IdDepartamento = row.Field<int>("DEPT_NO")
            };
                empleados.Add(emp);
            }
            ResumenEmpleados resumen = new ResumenEmpleados
            {
                Personas = personas,
                MaximoSalario = maximo,
                MediaSalarial = media,
                Empleados = empleados
            };
            return resumen;
        }

        public List<string> GetOficios()
        {
            var consulta= (from datos in this.tablaEmpleados.AsEnumerable()
                          select datos.Field<string>("OFICIO")).Distinct();
            List<string> oficio = new List<string>();
            foreach (var ofi in consulta)
            {
                oficio.Add(ofi);
            }
            return oficio;
        }


    }
}
