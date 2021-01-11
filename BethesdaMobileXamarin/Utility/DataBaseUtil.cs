using BethesdaMobileXamarin.Registration.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace BethesdaMobileXamarin.Utility
{
    class DataBaseUtil
    {

        private SQLiteConnection conn;
        public SQLiteConnection GetConnection()
        {
            SQLiteConnection sqlConn;
            string dbName = "bethesdaMobiledb.db3";
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbName);
            sqlConn = new SQLiteConnection(dbPath);
            return sqlConn;
        }

        public DataBaseUtil()
        {
            conn = GetConnection();
            conn.CreateTable<Dokter>();
            conn.CreateTable<Klinik>();
        }
        public IEnumerable<Dokter> GetAllDokter()
        {
            var results = conn.Table<Dokter>().ToList();
            return results;

        }


        public IEnumerable<Klinik> GetAllKlinik()
        {
            var results = conn.Table<Klinik>().ToList();
            return results;

        }


        public bool InsertDokterAll(List<Dokter> dokters)
        {

            try
            {
                conn.InsertAll(dokters);
                return true;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public bool InsertKlinikAll(List<Klinik> kliniks)
        {

            try
            {
                conn.InsertAll(kliniks);
                return true;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }





        public bool DeleteAllDokter()
        {
            try
            {
                conn.DeleteAll<Dokter>();
                return true;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        public bool DeleteAllKlinik()
        {
            try
            {
                conn.DeleteAll<Klinik>();
                return true;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
