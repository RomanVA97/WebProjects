using System;
using System.Collections.Generic;
using System.Linq;
using Android.Database.Sqlite;
using Android.Util;
using SQLite;
using AppDrwer.Resources.models;

namespace AppDrwer.Resources.model
{
    class TrainContext
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        public bool CreateDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Trains5.db")))
                {
                    connection.CreateTable<Train>();
                    for (int i = 0; i < 20; i++)
                    {
                        Train train = new Train
                        {
                            Punkt = "punkt" + (i + 1),
                            StatitonId = (new Random()).Next(0, 2),
                            Number = (i + 1),
                            Date = new DateTime(2008, 2, 2, (i + 1), (50 - i), 0),
                            Ob = (i + 1),
                            Kupe = (i + 1),
                            Pl = (i + 1),
                            Lukc = (i + 1)
                        };
                        connection.Insert(train);
                    }
                    connection.CreateTable<Station>();//52.431184, 30.991729
                    connection.Insert(new Station { Id = 0, Lat = 52.431184, Long = 30.991729, Name = "Гомельский ЖД вокзал" });
                    //55.773601, 37.656744
                    connection.Insert(new Station { Id = 1, Lat = 55.773601, Long = 37.656744, Name = "Казанский ЖД вокзал" });
                }
                return true;
            }
            catch (Android.Database.Sqlite.SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }
        public List<Train> GetList()
        {
            try
            {
                List<Train> list = new List<Train>();
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Trains5.db")))
                {
                    return connection.Table<Train>().ToList();
                }

            }
            catch (SQLite.SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                CreateDataBase();
                return GetList();
            }
        }

        public List<Train> GetListId(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Trains5.db")))
                {
                    return connection.Table<Train>().Where(x => x.StatitonId == id).ToList();
                }

            }
            catch (SQLite.SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                CreateDataBase();
                return GetListId(id);
            }
        }


        public List<Station> GetStationList()
        {
            try
            {

                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Trains5.db")))
                {
                    return connection.Table<Station>().ToList();
                }

            }
            catch (SQLite.SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);
                CreateDataBase();
                return GetStationList();
            }
        }

    }
}