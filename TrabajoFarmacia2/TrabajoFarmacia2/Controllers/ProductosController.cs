using System;
using System.Data;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using TrabajoFarmacia2.Models;

namespace TrabajoFarmacia2.Controllers
{
    public class ProductosController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public ActionResult Index()
        {
            List<Producto> productos = new List<Producto>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM ProductosFarmacia";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        productos.Add(new Producto
                        {
                            Id = reader.GetInt32("Id"),
                            Nombre = reader.GetString("Nombre"),
                            Precio = reader.GetDecimal("Precio"),
                            Stock = reader.GetInt32("Stock")
                        });
                    }
                }
            }

            return View(productos);

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Producto producto)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO ProductosFarmacia (Nombre, Precio, Stock) VALUES (@Nombre, @Precio, @Stock)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");


        }

        // GET: Editar producto
        public ActionResult Edit(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM ProductosFarmacia WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Producto producto = new Producto
                    {
                        Id = reader.GetInt32("Id"),
                        Nombre = reader.GetString("Nombre"),
                        Precio = reader.GetDecimal("Precio"),
                        Stock = reader.GetInt32("Stock")
                    };
                    return View(producto);
                }
                return RedirectToAction("Index");
            }
        }

        // POST: Editar producto
        [HttpPost]
        public ActionResult Edit(Producto producto)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE ProductosFarmacia SET Nombre = @Nombre, Precio = @Precio, Stock = @Stock WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", producto.Nombre);
                cmd.Parameters.AddWithValue("@Precio", producto.Precio);
                cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                cmd.Parameters.AddWithValue("@Id", producto.Id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: Eliminar producto
        public ActionResult Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM ProductosFarmacia WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

    }
}