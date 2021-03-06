using Microsoft.AspNetCore.Mvc;
using MvcAWSCacheRedis.Models;
using MvcAWSCacheRedis.Repositories;
using MvcAWSCacheRedis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcAWSCacheRedis.Controllers
{
    public class ProductosController : Controller
    {
        private RepositoryProductos repo;
        ServiceCacheAWS service;

        public ProductosController(RepositoryProductos repo, ServiceCacheAWS service)
        {
            this.repo = repo;
            this.service = service;
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

        public IActionResult SeleccionarFavorito(int id)
        {
            Producto producto = this.repo.FindProducto(id);
            this.service.AddProductoCache(producto);
            TempData["MENSAJE"] = "Producto "+ producto.Nombre + " almacenado como Favorito";
            return RedirectToAction("IndexProductos");
        }

        public IActionResult Favoritos()
        {
            List<Producto> productos = this.service.GetProductosCache();
            return View(productos);
        }

        public IActionResult EliminarFavoritos(int id)
        {
            this.service.EliminarProductoCache(id);
            return RedirectToAction("Favoritos");
        }
    }
}
