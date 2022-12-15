using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Db_Aod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool v = true;
                string Conection_String = "Data Source=localhost\\SQLEXPRESS;Database=hospitales;integrated security=SSPI";
                SqlConnection connection = new SqlConnection(Conection_String);
                SqlCommand cm = new SqlCommand("select * from Medicamento",connection);

                connection.Open();

                SqlDataReader sdr = cm.ExecuteReader();

                while (sdr.Read())
                {
                    Console.WriteLine(sdr["nombre_medicamento"] + "," + sdr["indicaciones"]);
                }
                while (v) {
                    Console.WriteLine("quieres ingresar datos a la tabla paciente?" +
                        "\n 1.si" +
                        "\n 2.no");
                    int opciones = int.Parse(Console.ReadLine());
                    switch (opciones)
                    {
                        case 1:
                             Console.WriteLine("porfavor intrduce el nombre del paciente");
                             string nombre= Console.ReadLine();
                             Console.WriteLine("porfavor introduce la direcciond del paciente");
                             string direccion= Console.ReadLine();
                             Console.WriteLine("porfavro introduce el dni");
                             string dni =Console.ReadLine();
                             Console.WriteLine("porfavor introduce los dias ingresados: ");
                             string dias_ingresados=Console.ReadLine();
                             Console.WriteLine("porfavor introduce el diagnostico");
                             string diagnostico=Console.ReadLine();
                             Console.WriteLine("porfavor introduce el pronostico"); 
                             string proostico=Console.ReadLine();
                            SqlConnection connection2 = new SqlConnection(Conection_String);


                            // string sqlins = "Insert into Paciente (Dias_ingresados,Diagnostico,pronostico,nombre,dni,Direccion) values ('"+dias_ingresados+"','"+diagnostico+"','"+proostico+"','"+nombre+"','"+dni+"','"+direccion+"')";
                            string sqlins = $"Insert into dbo.Paciente values ({"@param1"},{ "@param2"},{ "@param3"},{ "@param4"},{ "@param5"},{ "@param6"})";
                            using (SqlCommand cmd2 = new SqlCommand(sqlins, connection2)) 
                            {

                                cmd2.Parameters.Add("@param1", System.Data.SqlDbType.VarChar, 50);
                                cmd2.Parameters.Add("@param2", System.Data.SqlDbType.VarChar, 50);
                                cmd2.Parameters.Add("@param3", System.Data.SqlDbType.VarChar, 50);
                                cmd2.Parameters.Add("@param4", System.Data.SqlDbType.NChar, 10);
                                cmd2.Parameters.Add("@param5", System.Data.SqlDbType.VarChar, 50);
                                cmd2.Parameters.Add("@param6", System.Data.SqlDbType.VarChar, 50);
                                cmd2.Parameters["@param1"].Value = dias_ingresados;
                                cmd2.Parameters["@param2"].Value = diagnostico;
                                cmd2.Parameters["@param3"].Value = proostico;
                                cmd2.Parameters["@param4"].Value = nombre;
                                cmd2.Parameters["@param5"].Value = dni;
                                cmd2.Parameters["@param6"].Value = direccion;

                                cmd2.CommandType = System.Data.CommandType.Text;
                                cmd2.ExecuteNonQuery();
                                Console.WriteLine("Datos inseratdos ");
                            }
                                //cmd2.CommandType = System.Data.CommandType.Text;
                                
                                
                            
                            break;
                        case 2:
                            Console.WriteLine("Adios");
                            v=false;
                            break;
                    }
                }
            }catch(Exception ex)
            {
                Console.WriteLine("Algo ha ido  mal ");
            }
        }
    }
}
