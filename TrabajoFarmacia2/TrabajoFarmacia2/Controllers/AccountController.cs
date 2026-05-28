using System.Web.Mvc;
using MySql.Data.MySqlClient;
using TrabajoFarmacia2.Models;

namespace TrabajoFarmacia2.Controllers
{
    public class AccountController : Controller
    {
        private string connectionString = System.Configuration.ConfigurationManager
            .ConnectionStrings["DefaultConnection"].ConnectionString;

        // GET: Login
        public ActionResult Login()
        {
            // Si ya esta logueado, redirigir al Index
            if (Session["UsuarioLogueado"] != null)
                return RedirectToAction("Index", "Productos");

            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return View(usuario);

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Usuarios WHERE Username = @Username AND Password = @Password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", usuario.Username);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Login exitoso — guardar en sesion
                        Session["UsuarioLogueado"] = usuario.Username;
                        return RedirectToAction("Index", "Productos");
                    }
                }
            }

            // Login fallido
            ViewBag.Error = "Usuario o contrasena incorrectos";
            return View(usuario);
        }

        // POST: Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}