using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using HotelMVCTest2.Models;

namespace HotelMVCTest2.DBUtil
{
    public class ManageHotel : IManage<Hotel>
    {
        //Lokal database
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hotel02032020;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        

        private const string GET_All = "Select * from hotel";  // forklar koden her
        private const string GET_ONE = "Select * from hotel Where Hotel_No=@ID";
        private const string INSERT = "Insert into Hotel values (@ID, @Name, @Address)";
        private const string DELETE = "Delete from Hotel where Hotel_No = @ID";
        private const string UPDATE = "Update Hotel set Hotel_No = @HotelId, Name = @Name, Address= @Address where Hotel_No = @ID";

        public IEnumerable<Hotel> Get()
        {
            List<Hotel> liste = new List<Hotel>();

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(GET_All, conn);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Hotel hotel = readHotel(reader);
                liste.Add(hotel);
            }
            conn.Close();
           

            return liste;
        }


        /// <summary>
        /// Formålet
        /// </summary>
        /// <param name="reader">input</param>
        /// <returns>Output</returns>
        private Hotel readHotel(SqlDataReader reader)
        {
            Hotel hotel = new Hotel();
            hotel.Id = reader.GetInt32(0);
            hotel.Name = reader.GetString(1);  // specielt avanceret 
            hotel.Address = reader.GetString(2);
            return hotel;
        }

        public Hotel Get(int id)
        {
            Hotel hotel = null;
            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(GET_ONE, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    hotel = readHotel(reader);
                }
            }
            catch (Exception)
            {
                hotel = null;
            }
            finally
            {
                conn.Close();
            }
            return hotel;

        }


        public bool Post(Hotel hotel)
        {
            bool returnValue = false;

            SqlConnection conn = new SqlConnection(ConnectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(INSERT, conn);
                cmd.Parameters.AddWithValue("@ID", hotel.Id);
                cmd.Parameters.AddWithValue("@Name", hotel.Name);
                cmd.Parameters.AddWithValue("@Address", hotel.Address);

                int noRowsAffected = cmd.ExecuteNonQuery();

                returnValue = noRowsAffected == 1 ? true : false;
            }
            catch (Exception ex)
            {
                returnValue = false;
            }
            finally
            {
                conn.Close();
            }

            return returnValue;
        }


        public bool Put(int id, Hotel hotel)
        {
            bool returnValue = false;

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(UPDATE, conn);
            cmd.Parameters.AddWithValue("@HotelID", hotel.Id);
            cmd.Parameters.AddWithValue("@Name ", hotel.Name);
            cmd.Parameters.AddWithValue("@Address", hotel.Address);
            cmd.Parameters.AddWithValue("@Id", id);

            int noRowsAffected = cmd.ExecuteNonQuery();

            returnValue = noRowsAffected == 1 ? true : false;
            conn.Close();
            return returnValue;
        }


        public bool Delete(int id)
        {
            bool returnValue = false;

            //SqlConnection conn = new SqlConnection(ConnectionString);
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(DELETE, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                int noRowsAffected = cmd.ExecuteNonQuery();

                returnValue = noRowsAffected == 1 ? true : false;
            }

            //conn.Close();
            return returnValue;

        }


    }
}
