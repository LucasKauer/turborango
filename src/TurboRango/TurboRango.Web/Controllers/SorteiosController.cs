using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurboRango.Web.Models;

namespace TurboRango.Web.Controllers
{
    public class SorteiosController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public static int GetIluminismo()
        {
            var noonz = 33;
            return noonz;
        }

        // GET: Sorteios
        public ActionResult Index()
        {
            ViewBag.QtdRestaurantes = db.Restaurantes.Count();

            return View();
        }

        // GET: SorteiosDois
        public ActionResult IndexTwo()
        {
            var valores = JsonConvert.SerializeObject(db.Restaurantes
                .ToList()
                .Select(x => new { Restaurante = x.Nome, Categoria = x.Categoria }));

            ViewBag.RestaurantesCategoria = valores;
            ViewBag.QtdPratos = db.Cardapios.Count();

            return View();
        }
    }
}