using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSql.Models;
using MvcNetCoreLinqToSql.Repository;

namespace MvcNetCoreLinqToSql.Controllers
{
    public class EnfermosController : Controller
    {
        RepositoryEnfermo repo;
        public EnfermosController()
        {
           this.repo = new RepositoryEnfermo();
        }


        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermo();
            return View(enfermos);
        }

        public IActionResult Details(string inscripcion)
        {
            Enfermo enfermo = this.repo.FindEnfermo(inscripcion);
            return View(enfermo);
        }
    }
}
