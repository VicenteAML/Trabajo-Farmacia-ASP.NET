using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using TrabajoFarmacia2.Models;

namespace TrabajoFarmacia2.Controllers
{
    public class ProductosController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager
            .ConnectionStrings["DefaultConnection"].ConnectionString;

        // ─────────────────────────────────────────
        // GET: Index — Listar medicamentos
        // ─────────────────────────────────────────
        public ActionResult Index()
        {
            if (Session["UsuarioLogueado"] == null)
                return RedirectToAction("Login", "Account");

            List<Medicamento> medicamentos = new List<Medicamento>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Medicamentos";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        medicamentos.Add(new Medicamento
                        {
                            Id = reader.GetInt32("Id"),
                            Nombre = reader.GetString("Nombre"),
                            Precio = reader.GetDecimal("Precio"),
                            Stock = reader.GetInt32("Stock"),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString("Descripcion"),
                            FechaVencimiento = reader.IsDBNull(reader.GetOrdinal("FechaVencimiento")) ? DateTime.MinValue : reader.GetDateTime("FechaVencimiento"),
                            Marca = reader.IsDBNull(reader.GetOrdinal("Marca")) ? null : reader.GetString("Marca"),
                            Laboratorio = reader.IsDBNull(reader.GetOrdinal("Laboratorio")) ? null : reader.GetString("Laboratorio")
                        });
                    }
                }
            }

            return View(medicamentos);
        }

        // ─────────────────────────────────────────
        // GET: Create — Mostrar formulario
        // ─────────────────────────────────────────
        public ActionResult Create()
        {
            if (Session["UsuarioLogueado"] == null)
                return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: Create — Guardar nuevo medicamento
        [HttpPost]
        public ActionResult Create(Medicamento medicamento)
        {
            if (!ModelState.IsValid)
                return View(medicamento);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Medicamentos 
                                 (Nombre, Precio, Stock, Descripcion, FechaVencimiento, Marca, Laboratorio) 
                                 VALUES 
                                 (@Nombre, @Precio, @Stock, @Descripcion, @FechaVencimiento, @Marca, @Laboratorio)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", medicamento.Nombre);
                cmd.Parameters.AddWithValue("@Precio", medicamento.Precio);
                cmd.Parameters.AddWithValue("@Stock", medicamento.Stock);
                cmd.Parameters.AddWithValue("@Descripcion", (object)medicamento.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaVencimiento", medicamento.FechaVencimiento);
                cmd.Parameters.AddWithValue("@Marca", (object)medicamento.Marca ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Laboratorio", (object)medicamento.Laboratorio ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // ─────────────────────────────────────────
        // GET: Edit — Cargar formulario con datos
        // ─────────────────────────────────────────
        public ActionResult Edit(int id)
        {
            if (Session["UsuarioLogueado"] == null)
                return RedirectToAction("Login", "Account");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Medicamentos WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Medicamento medicamento = new Medicamento
                        {
                            Id = reader.GetInt32("Id"),
                            Nombre = reader.GetString("Nombre"),
                            Precio = reader.GetDecimal("Precio"),
                            Stock = reader.GetInt32("Stock"),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString("Descripcion"),
                            FechaVencimiento = reader.IsDBNull(reader.GetOrdinal("FechaVencimiento")) ? DateTime.MinValue : reader.GetDateTime("FechaVencimiento"),
                            Marca = reader.IsDBNull(reader.GetOrdinal("Marca")) ? null : reader.GetString("Marca"),
                            Laboratorio = reader.IsDBNull(reader.GetOrdinal("Laboratorio")) ? null : reader.GetString("Laboratorio")
                        };
                        return View(medicamento);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        // POST: Edit — Guardar cambios
        [HttpPost]
        public ActionResult Edit(Medicamento medicamento)
        {
            if (Session["UsuarioLogueado"] == null)
                return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid)
                return View(medicamento);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Medicamentos SET 
                                 Nombre           = @Nombre, 
                                 Precio           = @Precio, 
                                 Stock            = @Stock, 
                                 Descripcion      = @Descripcion, 
                                 FechaVencimiento = @FechaVencimiento, 
                                 Marca            = @Marca, 
                                 Laboratorio      = @Laboratorio 
                                 WHERE Id         = @Id";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nombre", medicamento.Nombre);
                cmd.Parameters.AddWithValue("@Precio", medicamento.Precio);
                cmd.Parameters.AddWithValue("@Stock", medicamento.Stock);
                cmd.Parameters.AddWithValue("@Descripcion", (object)medicamento.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FechaVencimiento", medicamento.FechaVencimiento);
                cmd.Parameters.AddWithValue("@Marca", (object)medicamento.Marca ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Laboratorio", (object)medicamento.Laboratorio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", medicamento.Id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // ─────────────────────────────────────────
        // GET: Delete — Mostrar confirmación
        // ─────────────────────────────────────────
        public ActionResult Delete(int id)
        {
            if (Session["UsuarioLogueado"] == null)
                return RedirectToAction("Login", "Account");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Medicamentos WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Medicamento medicamento = new Medicamento
                        {
                            Id = reader.GetInt32("Id"),
                            Nombre = reader.GetString("Nombre"),
                            Precio = reader.GetDecimal("Precio"),
                            Stock = reader.GetInt32("Stock"),
                            Descripcion = reader.IsDBNull(reader.GetOrdinal("Descripcion")) ? null : reader.GetString("Descripcion"),
                            FechaVencimiento = reader.GetDateTime("FechaVencimiento"),
                            Marca = reader.IsDBNull(reader.GetOrdinal("Marca")) ? null : reader.GetString("Marca"),
                            Laboratorio = reader.IsDBNull(reader.GetOrdinal("Laboratorio")) ? null : reader.GetString("Laboratorio")
                        };
                        return View(medicamento);
                    }
                }
            }

            return RedirectToAction("Index");
        }

        // POST: Delete — Confirmar eliminación
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["UsuarioLogueado"] == null)
                return RedirectToAction("Login", "Account");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Medicamentos WHERE Id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}