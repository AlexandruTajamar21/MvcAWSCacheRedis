using Microsoft.AspNetCore.Mvc;
using MvcAWSCacheRedis.Models;
using MvcAWSCacheRedis.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAWSCacheRedis.Controllers
{
    public class ProductosController : Controller
    {
        private RepositoryProductos repo;

        public ProductosController(RepositoryProductos repo)
        {
            this.repo = repo;
        }

        public IActionResult IndexProductos()
        {
            List<Producto> productos = this.repo.GetProductos();
            return View(productos);
        }

        public IActionResult Details(int id)
        {
            Producto produc = this.repo.FindProducto(id);
            return View(produc);
        }
    }
}
