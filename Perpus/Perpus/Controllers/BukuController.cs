using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Perpus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BukuController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public BukuController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select id,nama_buku,tahun_terbit from buku";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BukuAppCon");
            MySqlDataReader myReader;
            using(MySqlConnection mycon=new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
